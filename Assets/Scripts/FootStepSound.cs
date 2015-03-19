using UnityEngine;
using System.Collections;

public class FootStepSound : MonoBehaviour 
{
    AudioSource footStep;

	void Start ()
    {
        footStep = gameObject.GetComponent<AudioSource>();
        footStep.volume = 0;
    }

    void Update () 
    {
        if (Input.GetAxis("Horizontal") == 1 || Input.GetAxis("Vertical") == 1 ||
            Input.GetAxis("Horizontal") == -1 || Input.GetAxis("Vertical") == -1)
            footStep.volume = 0.5f;

        else
            footStep.volume = 0;
	}
}
