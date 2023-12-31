﻿using UnityEngine;
using UnityEngine.Events;

public class Elevator_controler : MonoBehaviour
{
    // Start is called before the first frame update
    Animator Animator;
    GameObject MainCamera;
    bool goUp;
    void Start()
    {
        Animator = GetComponent<Animator>();
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (goUp)
        {
            GameManager.Instance.PlayerInstance.GetComponent<Player_Controller>().Isleaving = true;
        }
    }
    //se llama desde el animator
    void stopElevator()
    {
        Animator.SetBool("GoUp", false);
    }
    //se llama desde el animator
    void loadWinLevelEvent()
    {
        UnityEvent @event = GameManager.Instance.LvlManager.GetComponent<LevelManager>().WinRoom;
        GameManager.Instance.EventQueue.Add(@event);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.CompareTag("Player"))
            {
                MainCamera.GetComponent<CameraFollow>().enabled = false;
                other.gameObject.GetComponent<Player_Controller>().Rb.velocity = Vector3.zero;
                goUp = true;
                Animator.SetBool("GoUp", true);
            }
        }
    }
}
