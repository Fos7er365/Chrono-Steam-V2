using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPartSystemHandler : MonoBehaviour
{
    [SerializeField] float damage;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player_Controller>().Life_Controller.GetDamage(damage);
        }
    }
}
