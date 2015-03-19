using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour 
{
    [SerializeField]
    internal Vector3 exitPosition1;

    [SerializeField]
    internal Vector3 exitPosition2;

	void Start () 
    {
	    if (exitPosition1.x > exitPosition2.x) //sort exits from left to right for proper room creation
        {
            Vector3 temp = exitPosition1;
            exitPosition1 = exitPosition2;
            exitPosition2 = temp;
        }
	}
}
