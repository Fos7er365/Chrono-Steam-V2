﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplate : MonoBehaviour
{
    public GameObject[] topDoorRooms;
    public GameObject[] rightDoorRooms;
    public GameObject[] botomDoorRooms;
    public GameObject[] leftDoorRooms;
    public GameObject[] closeDoorRooms;

    public List<GameObject> spawnedRooms;

    public float waitTime;
    private bool spawnedBoss;
    public GameObject boss;

    private void Update()
    {
        if (spawnedBoss == false)
        {
            if (waitTime <= 0)
            {
                // limpio la lista de rooms para no spawnear el bossen una missreference o close doors
                for (int i = spawnedRooms.Count-1; i >=0; i--)
                {
                    if (spawnedRooms[i]==null || spawnedRooms[i].CompareTag("CloseDoors") || spawnedRooms[i].CompareTag("Parkour_Room"))
                    {
                        spawnedRooms.Remove(spawnedRooms[i]);
                    }
                    if (i==0)
                    {
                        GameObject bossGO = Instantiate(boss, spawnedRooms[spawnedRooms.Count - 1].transform.position, Quaternion.identity);
                        if (bossGO.TryGetComponent<ObstacleAvoidance>(out ObstacleAvoidance obstacle))
                            bossGO.GetComponent<ObstacleAvoidance>().waypointsContainer = spawnedRooms[spawnedRooms.Count - 1].GetComponent<RoomAdder>().RoomWaypoints;
                        else
                            Debug.Log("missing boss obstacleAvoidence component");
                        GameObject lastRoomElevator = spawnedRooms[spawnedRooms.Count - 1].GetComponent<RoomAdder>().RoomElevator;
                        if(lastRoomElevator!=null)
                            lastRoomElevator.SetActive(true);
                        spawnedBoss = true;
                    }
                }
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
