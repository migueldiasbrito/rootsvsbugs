using UnityEngine;

public class MultiPurposeAudioSource : MonoBehaviour
{
    public static MultiPurposeAudioSource instance;
    public AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
