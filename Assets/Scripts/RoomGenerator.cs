using UnityEngine;
using System.Collections.Generic;

public class RoomGenerator : MonoBehaviour 
{

	[SerializeField]
	List<GameObject> rooms = new List<GameObject>(); //List of predefined rooms for creation

	GameObject[] existingRooms = new GameObject[3]; // List of currently existing rooms
    bool existingRoomsSorted = false;

	void Update () 
	{
        if (!existingRoomsSorted) //sorting rooms from leftmost to rightmost (x) for proper room creation: 0 - left, 1 - middle, 2 - right
        {
            existingRooms = GameObject.FindGameObjectsWithTag("Room");
            for (int i = 0; i < existingRooms.Length; i++)
            {
                for (int j = 0; j < existingRooms.Length; j++)
                {
                    if (i != j)
                    {
                        if (existingRooms[i].GetComponent<Transform>().position.x > existingRooms[j].GetComponent<Transform>().position.x)
                        {
                            GameObject temp = existingRooms[i];
                            existingRooms[i] = existingRooms[j];
                            existingRooms[j] = temp;
                        }
                    }
                }
            }
            existingRoomsSorted = true;
        }
	}

	void OnTriggerEnter(Collider c) 
	{
		if (c.gameObject != existingRooms[1]) //if not in middle room
		{
			int roomNumber = Random.Range(0, rooms.Count); //randomize room to be created
            Vector3 roomPosition = new Vector3();

			if (c.gameObject == existingRooms[0]) //if in left room
			{
                roomPosition = SetRoomPosition(existingRooms[0], rooms[roomNumber], true); //true - exit through left
                Destroy(existingRooms[2]);
			}

			else if (c.gameObject == existingRooms[2]) //if in right room
			{
                roomPosition = SetRoomPosition(existingRooms[2], rooms[roomNumber], false); //false - exit through right
                Destroy(existingRooms[0]);
			}

            Instantiate(rooms[roomNumber], roomPosition, Quaternion.identity); //create room
            existingRoomsSorted = false;
		}
	}

    static public Vector3 SetRoomPosition(GameObject currentRoom, GameObject nextRoom, bool exit1or2)
    {
        Room curRoomProp = currentRoom.GetComponent<Room>();
        Room nextRoomProp = nextRoom.GetComponent<Room>();

        Vector3 curRoomPos = currentRoom.GetComponent<Transform>().position;

        Vector3 curRoomExit = new Vector3();
        Vector3 nextRoomExit = new Vector3();

        if (exit1or2)
        {
            curRoomExit = curRoomProp.exitPosition2;
            nextRoomExit = nextRoomProp.exitPosition1;
        }

        else
        {
            curRoomExit = curRoomProp.exitPosition1;
            nextRoomExit = nextRoomProp.exitPosition2;
        }

        return curRoomPos + curRoomExit - nextRoomExit;
    }
}
