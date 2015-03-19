using UnityEngine;
using System.Collections;

public class MainMenuGUI : MonoBehaviour 
{
    [SerializeField]
    GUISkin skin;

    [SerializeField]
    Texture2D titleImage;

    [SerializeField]
    Texture2D startImage;

    [SerializeField]
    Texture2D exitImage;

    [SerializeField]
    Texture2D loadingImage;

    Texture2D transitionTex;
    float transitionAlpha = 0;
    bool transitioning = false;

    [SerializeField]
    float transitionSpeed = 0;

    float scaleFactor = 0;

    void Start ()
    {
        Time.timeScale = 1;

        transitioning = false;
        transitionAlpha = 0;

        transitionTex = new Texture2D(1, 1);
        Color[] texPix = transitionTex.GetPixels();
        for (int i = 0; i < texPix.Length; i++)
            texPix[i] = new Color(0, 0, 0, 0);

        transitionTex.SetPixels(texPix);
        transitionTex.Apply();
    }

    void Update ()
    {
        if (Screen.width / 1920.0f < Screen.height / 1080.0f)
            scaleFactor = Screen.width / 1920.0f;

        else
            scaleFactor = Screen.height / 1080.0f;

        if (transitioning && transitionAlpha != 1.0f)
        {
            transitionAlpha += transitionSpeed * Time.deltaTime;

            Color[] transitionTexPix = transitionTex.GetPixels();
            for (int i = 0; i < transitionTexPix.Length; i++)
                transitionTexPix[i] = new Color(0, 0, 0, transitionAlpha);

            transitionTex.SetPixels(transitionTexPix);
            transitionTex.Apply();
        }

        if (transitionAlpha >= 1.0f)
            transitionAlpha = 1.0f;

        if (transitionAlpha == 1.0f && transitioning)
        {
            Application.LoadLevel("game");
        }
    }

    void OnGUI ()
    {
        GUI.skin = skin;

        GUI.DrawTexture(new Rect((Screen.width - titleImage.width * scaleFactor) / 2, Screen.height / 20, titleImage.width * scaleFactor, titleImage.height * scaleFactor), titleImage);

        if (GUI.Button(new Rect((Screen.width - startImage.width * scaleFactor) / 2, (Screen.height - startImage.height * scaleFactor) / 2, startImage.width * scaleFactor, startImage.height * scaleFactor), "", "StartButton")
            && !transitioning)
        {
            transitioning = true;
        }

        if (GUI.Button(new Rect((Screen.width - exitImage.width * scaleFactor) / 2, (Screen.height - startImage.height * scaleFactor) / 2 + exitImage.height * scaleFactor, exitImage.width * scaleFactor, exitImage.height * scaleFactor), "", "ExitButton")
            && !transitioning)
        {
            Application.Quit();
        }

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), transitionTex, ScaleMode.StretchToFill);

        if (transitionAlpha == 1.0f)
        {
            GUI.DrawTexture(new Rect(Screen.width - loadingImage.width * scaleFactor - 20 * scaleFactor, Screen.height - loadingImage.height * scaleFactor - 20 * scaleFactor, loadingImage.width * scaleFactor, loadingImage.height * scaleFactor), loadingImage);
        }
    }
}
