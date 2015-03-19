using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour 
{
    [SerializeField]
    float lightIncreaseSpeed = 0;

    float actualLightIncreaseSpeed = 0;
    float lightTarget = 0;
    bool isMaxIntensity = false;
    Light playerLight;

	void Start () 
    {
        playerLight = gameObject.GetComponentInChildren<Light>();
        actualLightIncreaseSpeed = lightIncreaseSpeed;
        lightTarget = playerLight.intensity;
        playerLight.intensity = 0;
	}
	
	void Update () 
    {
        if (!isMaxIntensity)
        {
            if (playerLight.intensity + lightIncreaseSpeed * Time.deltaTime >= lightTarget)
            {
                actualLightIncreaseSpeed = lightTarget - playerLight.intensity;
                isMaxIntensity = true;
            }

            if (playerLight.intensity < lightTarget)
                playerLight.intensity += actualLightIncreaseSpeed * Time.deltaTime;

            else
                isMaxIntensity = true;
        }
	}
}
