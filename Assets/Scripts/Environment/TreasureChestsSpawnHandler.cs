using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TreasureChestsSpawnHandler : MonoBehaviour
{

    [SerializeField] GameObject chestPrefab;
    [SerializeField] int maxChestPerLevel;
    GameObject[] chestContainers;
    LevelManager lvlMgr;
    bool isSpawned;

    public bool IsSpawned { get => isSpawned; set => isSpawned = value; }

    private void Awake()
    {
        lvlMgr = GetComponent<LevelManager>();
    }

    private void Start()
    {
        isSpawned = false;
    }

    private void Update()
    {
    }

    void SpawnChests()
    {
        if(maxChestPerLevel > 0)
        {
            for (int j = 0; j < chestContainers.Length - 1; j++)
            {
                if (!chestContainers[j].GetComponent<ChestContainer>().HasChestSpawned)
                {
                    Instantiate(chestPrefab, chestContainers[j].transform);
                    chestContainers[j].GetComponent<ChestContainer>().HasChestSpawned = true;
                }
                maxChestPerLevel--;
            }
        }
    }

    public void HandleChestSpawning()
    {
        if (GameObject.FindGameObjectsWithTag("Chest_Container") != null)
        {
            chestContainers = GameObject.FindGameObjectsWithTag("Chest_Container");
            SpawnChests();
            isSpawned = true;
        }
    }

}
