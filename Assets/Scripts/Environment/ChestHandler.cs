using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestHandler : MonoBehaviour
{
    [SerializeField] Transform spawnPosition;
    [SerializeField] GameObject[] powerUps;
    Dictionary<ActionNode, int> _regularAttacksRouletteWheelNodes = new Dictionary<ActionNode, int>();
    bool isPowerUpSpawn;

    public bool IsPowerUpSpawn { get => isPowerUpSpawn; }

    private void OnCollisionEnter(Collision collision)
    {
        if(!isPowerUpSpawn && collision.gameObject.tag == "Player")
        {
            HandlePowerUpSpawn();
            //go.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    void HandlePowerUpSpawn()
    {
        isPowerUpSpawn = true;
        var randomIndex = Random.Range(0, powerUps.Length);
        var go = Instantiate(powerUps[randomIndex], spawnPosition.position, Quaternion.identity);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(spawnPosition.position, 1f);
    }

}
