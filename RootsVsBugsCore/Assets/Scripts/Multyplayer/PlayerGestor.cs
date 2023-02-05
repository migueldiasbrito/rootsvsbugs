using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerGestor : MonoBehaviour
{
    // Start is called before the first frame update

    public int ? _myID;
    public Lane myLane;
    public Player myPlayer;

    public List<Plant> myPlants;

    private void Start()
    {
        myPlayer.resourcesUpdated.AddListener((Resources resources) =>
        {
            print("ACONTECE");
            string tradtada = "R" + resources.Minerals + "/" + resources.Water + "/" + resources.Seeds;
            SendToPhone(_myID.ToString(), tradtada);
        });
    }

    public void LerETratarMsg(string msg)
    {

   
        string idM = msg.Split(",")[0];
        string mensagemRawRaw = msg.Split(",")[1];

        if (_myID.ToString() == idM)
        {
            //Plantar
            if (mensagemRawRaw.Substring(0, 1) == "P")
            {
                string mensagem = mensagemRawRaw.Split("P;")[1];


                print("A Mensagem  " + mensagem);

                string laneM = mensagem.Split(";")[1];
                string plantaM = mensagem.Split(";")[0];
                string suns = mensagem.Split(';')[2];

                print("foi eu " + idM + "      L->" + laneM + "     Planta-> " + plantaM);


                myPlayer.Resources.Minerals = int.Parse(suns);
                print(myPlayer.Resources.Minerals);
                myLane.AddPlantToSlot(myPlants[int.Parse(plantaM)], int.Parse(laneM));

            }
            //Enviar
            if (mensagemRawRaw.Substring(0, 1) == "E")
            {
                string mensagemh = mensagemRawRaw.Split("E;")[1];
                string IDAmigo = mensagemh.Split(";")[0];
                string RecursoEnv = mensagemh.Split(';')[1];

                print(" ID " + IDAmigo + "Recurso" + RecursoEnv);
            }

            if(mensagemRawRaw.Substring(0, 1) == "L")
            {
                if(myLane != null)
                {
                    myLane.GameOver();
                    myLane.gameObject.SetActive(false);
                    Destroy(myLane);

                }
          
            }

            if (mensagemRawRaw.Substring(0, 1) == "W")
            {
                string waterToAdd = mensagemRawRaw.Split(";")[1];
                int waterInt = int.Parse(waterToAdd);

                myPlayer.Resources.Minerals = waterInt;
            }


        }

    }

    public void SendToPhone(string id, string msg)
    {
        GestorGeral.SendResourceUpdateMessage(id, msg);
    }
}
