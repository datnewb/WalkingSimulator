using UnityEngine;
using System.Collections.Generic;

public class GamePause : MonoBehaviour 
{
    [SerializeField]
    GUISkin skin;

    [SerializeField]
    Texture2D exitMessageImage;

    [SerializeField]
    Texture2D yesImage;

    [SerializeField]
    Texture2D noImage;

    [SerializeField]
    Texture2D exitImage;

    Texture2D background;

    bool paused = false;

    float scaleFactor = 0;

    KeyCode prevKey = KeyCode.None;

    MouseLook playerLookX;
    MouseLook playerLookY;

    bool isAudioStopped;

	void Start () 
    {
        paused = false;
        background = new Texture2D(1, 1);
        Color[] bgPix = background.GetPixels();
        for (int i = 0; i < bgPix.Length; i++)
            bgPix[i] = new Color(0, 0, 0, 0.7f);
        background.SetPixels(bgPix);
        background.Apply();

        isAudioStopped = false;
	}
	
	void Update () 
    {
        playerLookX = gameObject.GetComponent<MouseLook>();
        playerLookY = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>();

        if (Screen.width / 1920.0f < Screen.height / 1080.0f)
            scaleFactor = Screen.width / 1920.0f;

        else
            scaleFactor = Screen.height / 1080.0f;

        if (paused)
        {
            Screen.showCursor = true;
            Screen.lockCursor = false;

            playerLookX.enabled = false;
            playerLookY.enabled = false;

            Time.timeScale = 0;

            if (!isAudioStopped)
            {
                foreach (AudioSource audioSource in FindObjectsOfType<AudioSource>())
                {
                    audioSource.Pause();
                    isAudioStopped = true;
                }
            }
        }

        else
        {
            Screen.showCursor = false;
            Screen.lockCursor = true;

            playerLookX.enabled = true;
            playerLookY.enabled = true;

            Time.timeScale = 1;

            if (isAudioStopped)
            {
                foreach (AudioSource audioSource in FindObjectsOfType<AudioSource>())
                {
                    if (audioSource.time > 0)
                    {
                        audioSource.Play();
                    }
                }
                isAudioStopped = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && prevKey != KeyCode.Escape)
        {
            paused = !paused;
        }

        try
        {
            prevKey = Event.current.keyCode;
        }
        catch
        {
            prevKey = KeyCode.None;
        }
	}

    void OnGUI ()
    {
        GUI.skin = skin;

        if (paused)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background, ScaleMode.StretchToFill);

            GUI.DrawTexture(new Rect((Screen.width - exitMessageImage.width * scaleFactor) / 2, (Screen.height - exitMessageImage.height * scaleFactor * 3) / 2, exitMessageImage.width * scaleFactor, exitMessageImage.height * scaleFactor), exitMessageImage);

            if (GUI.Button(new Rect((Screen.width - noImage.width * scaleFactor - yesImage.width * scaleFactor * 3) / 2, (Screen.height - yesImage.height * scaleFactor) / 2, yesImage.width * scaleFactor, yesImage.height * scaleFactor), "", "YesButton"))
            {
                Application.LoadLevel("mainMenu");
            }

            if (GUI.Button(new Rect((Screen.width - noImage.width * scaleFactor) / 2, (Screen.height - noImage.height * scaleFactor) / 2, noImage.width * scaleFactor, noImage.height * scaleFactor), "", "NoButton"))
            {
                paused = false;
            }

            if (GUI.Button(new Rect((Screen.width - noImage.width * scaleFactor + exitImage.width * scaleFactor * 3) / 2, (Screen.height - exitImage.height * scaleFactor) / 2, exitImage.width * scaleFactor, exitImage.height * scaleFactor), "", "GameExitButton"))
            {
                Application.Quit();
            }
        }
    }
}
