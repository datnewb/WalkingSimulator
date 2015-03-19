using UnityEngine;
using System.Collections;

public class HeartBeat : MonoBehaviour 
{
    [SerializeField]
    AudioSource heartbeatSource;

    float heartCheck = 10;
    float currentHeartCheck = 0;

    void Update()
    {
        if (!heartbeatSource.isPlaying)
        {
            currentHeartCheck += Time.deltaTime;
            if (currentHeartCheck >= heartCheck)
            {
                currentHeartCheck = 0;
                int chance = Random.Range(0, 100);
                Debug.Log(chance);
                if (chance <= 5)
                    heartbeatSource.Play();
            }
        }
    }
}
