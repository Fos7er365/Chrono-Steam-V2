using UnityEngine;

public class ChestContainer : MonoBehaviour
{
    // Start is called before the first frame update
    bool hasChestSpawned;
    [SerializeField] GameObject chestSpawnPivot;

    public bool HasChestSpawned { get => hasChestSpawned; set => hasChestSpawned = false; }
    public GameObject ChestSpawnPivot { get => chestSpawnPivot; set => chestSpawnPivot = value; }
}
