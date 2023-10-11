using UnityEngine;

public class ParticleFX : MonoBehaviour
{
    [SerializeField]
    Transform castSpot;

    [SerializeField]
    GameObject vfxPrefab;

    public void InstantiateVFX()
    {

        var castPref = GameObject.Instantiate(vfxPrefab, castSpot.position, castSpot.rotation);
        Destroy(vfxPrefab, 3f);

    }

}
