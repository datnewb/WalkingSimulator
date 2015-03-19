using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour 
{
    internal AudioSource doorAudio;

    void Start()
    {
        doorAudio = GetComponent<AudioSource>();
    }
}
