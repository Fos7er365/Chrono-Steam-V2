using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGlassesHandling : MonoBehaviour
{
    [SerializeField] GameObject bossHeadGO;

    void Update()
    {
        transform.position = bossHeadGO.transform.position;
        transform.rotation = bossHeadGO.transform.rotation;
    }
}
