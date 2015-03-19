using UnityEngine;
using System.Collections;

public class DoorSound : MonoBehaviour 
{
    void Start()
    {
        foreach (Door door in FindObjectsOfType<Door>())
        {
            int chance = Random.Range(0, 100);
            if (chance <= 10)
            {
                door.doorAudio.PlayOneShot(door.doorAudio.clip);
                break;
            }
        }
        Destroy(this);
    }
}
