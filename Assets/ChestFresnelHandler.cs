using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestFresnelHandler : MonoBehaviour
{

    ChestHandler chestParent;

    private void Awake()
    {
        chestParent = GetComponentInParent<ChestHandler>();
    }

    private void Update()
    {
        if (chestParent.IsPowerUpSpawn) TurnOffFresnel();
    }

    void TurnOffFresnel()
    {
        if (GetComponent<MeshRenderer>() != null)
        {
            Debug.Log("Disable UI chest fresnel");
            for (int i = 0; i < GetComponent<MeshRenderer>().materials.Length; i++)
            {
                var b = GetComponent<MeshRenderer>().materials[i];
                if (b.shader.name == "Fresnel") b.SetFloat("_FresnelScale", 0);
                else
                {
                    b.SetFloat("_FresnelRimPower", 0);
                    b.SetFloat("_FresnelSize", 0);
                }
            }
        }
    }
}
