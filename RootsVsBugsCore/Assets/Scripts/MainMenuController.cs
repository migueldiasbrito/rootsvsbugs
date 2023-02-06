using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public static int MainMenuState = 0;

    public GameObject MainMenu;
    public GameObject EndScreen;
    public TMP_Text endScreenText;

    public RectTransform exitCreditsButton;
    public RectTransform speedUpCreditsButton;
    public Animator creditsAnimator;
    private int exitCreditsCounter;

    public GameObject Credits;

    public AudioSource multiPurposeAudioSource;
    public AudioClip victory;
    public AudioClip defeat;

    private bool canExitCredits = false;

    private void Start()
    {
        switch (MainMenuState)
        {
            case 0:
                MainMenu.SetActive(true);
                EndScreen.SetActive(false);
                if (!EverPlayingMusic.instance.audioSource.isPlaying)
                    EverPlayingMusic.instance.audioSource.Play();
                break;
            case 1:
                endScreenText.text = "DEFEAT";
                MainMenu.SetActive(false);
                EndScreen.SetActive(true);
                EverPlayingMusic.instance.audioSource.Stop();
                multiPurposeAudioSource.PlayOneShot(defeat);
                break;
            case 2:
                endScreenText.text = "VICTORY";
                MainMenu.SetActive(false);
                EndScreen.SetActive(true);
                EverPlayingMusic.instance.audioSource.Stop();
                multiPurposeAudioSource.PlayOneShot(victory);
                break;
        }
    }

    public void PlayEasy()
    {
        GoToGameScene("EasyPlayerGameScene");
    }

    public void PlayNormal()
    {
        GoToGameScene("NormalPlayerGameScene");
    }

    public void PlayHard()
    {
        GoToGameScene("HardPlayerGameScene");
    }

    private void GoToGameScene(string sceneName)
    {
        MainMenuState = 0;
        if (!EverPlayingMusic.instance.audioSource.isPlaying)
            EverPlayingMusic.instance.audioSource.Play();
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        MainMenuState = 0;
        Start();
    }

    public void OpenCredits()
    {
        Credits.SetActive(true);
    }

    public void TryExitCredits()
    {
        if (canExitCredits)
        {
            canExitCredits = false;
            Credits.SetActive(false);

            exitCreditsCounter = 0;
            speedUpCreditsButton.gameObject.SetActive(false);
            creditsAnimator.speed = 1;
            EverPlayingMusic.instance.audioSource.pitch = 1;
        }
        else
        {
            exitCreditsButton.position = new Vector3(Random.Range(100f, 1820f), Random.Range(100f, 980f));
            exitCreditsCounter++;
            if (exitCreditsCounter >= 3 && creditsAnimator.speed == 1)
            {
                speedUpCreditsButton.gameObject.SetActive(true);
            }
        }
    }

    public void CanExitCredits()
    {
        canExitCredits = true;
    }

    public void SpeedUpCredits()
    {
        creditsAnimator.speed = 0.5f;
        EverPlayingMusic.instance.audioSource.pitch = 0.5f;
        speedUpCreditsButton.gameObject.SetActive(false);
    }


    public void goToMultyplayer()
    {
        SceneManager.LoadScene("Muyltyplayer", LoadSceneMode.Single);
    }
}
