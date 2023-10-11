using UnityEngine;

public abstract class Spawner : IFactory
{

    public GameObject Create(GameObject prefab)
    {
        return GameObject.Instantiate(prefab);
    }

}
