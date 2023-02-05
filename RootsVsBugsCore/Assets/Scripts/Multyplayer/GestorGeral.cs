using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TMPro;
using Unity.Collections;
using Unity.Networking.Transport;
using Unity.VisualScripting;
using Unity.XR.Oculus.Input;
using UnityEngine;

public class GestorGeral : MonoBehaviour
{
    public static NetworkDriver m_Driver;
    private static NativeList<NetworkConnection> m_Connections;

    public GameObject Lane;

    public Camera camera;
    public Transform helthBarHolder;

    public EnemySettings enemySettings;

    public TMP_Text IP;


    public List<GameObject> posicoes;
    public List<string> ocupado;

    void Start()
    {

        IP.text ="IP->  "+ GetLocalIPv4();
        m_Driver = NetworkDriver.Create();
        var endpoint = NetworkEndPoint.AnyIpv4; // The local address to which the client will connect to is 127.0.0.1
        endpoint.Port = 9000;
        print(endpoint);

        


        if (m_Driver.Bind(endpoint) != 0)
            Debug.Log("Failed to bind to port 9000");
        else
            m_Driver.Listen();

        m_Connections = new NativeList<NetworkConnection>(16, Allocator.Persistent);
    }

    public void OnDestroy()
    {
        if (m_Driver.IsCreated)
        {
            m_Driver.Dispose();
            m_Connections.Dispose();
        }
    }

    void Update()
    {
        m_Driver.ScheduleUpdate().Complete();

        // CleanUpConnections
        for (int i = 0; i < m_Connections.Length; i++)
        {
            if (!m_Connections[i].IsCreated)
            {
                m_Connections.RemoveAtSwapBack(i);
                --i;
            }
        }
        // AcceptNewConnections
        NetworkConnection c;
        while ((c = m_Driver.Accept()) != default(NetworkConnection))
        {
            m_Connections.Add(c);
            GameObject newG = Instantiate(Lane);
            newG.SetActive(true);


            int pistDisponivel = -1;


            foreach (string item in ocupado)
            {
                if(item == "VAZIO")
                {
                    pistDisponivel = ocupado.IndexOf(item);
                   
                    break;
                }
            }

            //por na primeira disponivel
            newG.gameObject.transform.position = posicoes[pistDisponivel].transform.position;
            ocupado[pistDisponivel] = m_Connections[m_Connections.Length - 1].InternalId.ToString();

            newG.GetComponent<PlayerGestor>()._myID = m_Connections[m_Connections.Length-1].InternalId;
            EveryoneListen += newG.GetComponent<PlayerGestor>().LerETratarMsg;
            
            newG.GetComponent<Lane>().SetUiOptions(camera,helthBarHolder);
            newG.GetComponent<Lane>().SetEnemySettings(enemySettings);
            Debug.Log("Accepted a connection");
        }

        DataStreamReader stream;
        for (int i = 0; i < m_Connections.Length; i++)
        {
            NetworkEvent.Type cmd;
            while ((cmd = m_Driver.PopEventForConnection(m_Connections[i], out stream)) != NetworkEvent.Type.Empty)
            {
                if (cmd == NetworkEvent.Type.Data)
                {
                    string msg = stream.ReadFixedString32().ToString();

                    Debug.Log("Ele disse me " + msg + " ID="+ m_Connections[i].InternalId);

                    EveryoneListen?.Invoke( m_Connections[i].InternalId + "," + msg);
                    IWishToLogOut(m_Connections[i], msg);
                    // m_Driver.BeginSend(NetworkPipeline.Null, m_Connections[i], out var writer);
                    // writer.WriteUInt(number);
                    // m_Driver.EndSend(writer);
                }
                else if (cmd == NetworkEvent.Type.Disconnect)
                {
                    Debug.Log("Client disconnected from server");
                    m_Connections[i] = default(NetworkConnection);
                }
            }
        }
    }


    public static event Action<string> EveryoneListen;

    public static void SendResourceUpdateMessage(string id, string msg)
    {
        foreach(NetworkConnection connection in m_Connections)
        {
            if (connection.InternalId.ToString() != id) continue;
            m_Driver.BeginSend(NetworkPipeline.Null, connection, out var writer);
            writer.WriteFixedString32(msg);
            m_Driver.EndSend(writer);
            return;
        }

        Debug.Log("és burro");
    }

    public  void IWishToLogOut(NetworkConnection m, string mensagem)
    {
        print("Mensa"+ mensagem);
        if (mensagem.Substring(0, 1) == "L")
        {
            print("LoggerOUT");
            Debug.Log("Client disconnected from server");

            // m = default(NetworkConnection);

            int index = -1;

            foreach (string item in ocupado)
            {
                if (item == m.InternalId.ToString())
                {
                    index = ocupado.IndexOf(item);
                    Debug.Log("r");
                }
            }

            print("Valor?" + index);
            if (index != -1)
            {
                ocupado[index] = "VAZIO";
            }
        }
  
    }
    //GET IP
    public string GetLocalIPv4()
    {
        return Dns.GetHostEntry(Dns.GetHostName())
            .AddressList.First(
                f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            .ToString();
    }
}
