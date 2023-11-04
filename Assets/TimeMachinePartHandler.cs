using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMachinePartHandler : MonoBehaviour
{
    Rigidbody _rb;
    CapsuleCollider _collider;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        CheckCollisionWGround(collision);
    }
    private void OnTriggerEnter(Collider other)
    {

        CheckCollisionWPlayer(other);
    }

    void CheckCollisionWGround(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _rb.useGravity = false;
            _collider.isTrigger = true;
        }
    }
    void CheckCollisionWPlayer(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.MachinePartsPickedUp++;
            Destroy(gameObject, 1f);
        }
    }

}
