using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomAdder : MonoBehaviour
{
    private RoomTemplate templates;
    [SerializeField]
    private GameObject _roomElevator;
    [SerializeField]
    private GameObject _roomWaypoints;
    public GameObject RoomElevator => _roomElevator;

    public GameObject RoomWaypoints => _roomWaypoints;

    void Start()
    {
        templates = GameObject.FindWithTag("RoomTamplates").GetComponent<RoomTemplate>();
        
        templates.spawnedRooms.Add(this.gameObject);
    }
}
