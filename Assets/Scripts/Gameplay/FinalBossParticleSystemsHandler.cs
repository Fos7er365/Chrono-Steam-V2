using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossParticleSystemsHandler : MonoBehaviour
{
    [SerializeField] GameObject leftHandPartSystemGO;
    [SerializeField] GameObject rightHandPartSystemGO;
    [SerializeField] GameObject damagePartSystemGO;
    [SerializeField] GameObject bodyCenterPartSystemGO;
    [SerializeField] GameObject attack1ParticleSystem;
    [SerializeField] GameObject attack2ParticleSystem;
    [SerializeField] GameObject attack3ParticleSystem;
    [SerializeField] GameObject summonAttackParticleSystem;
    [SerializeField] GameObject damageParticleSystem;
    [SerializeField] GameObject deathParticleSystem;


    public void PlayFinalBossAttack1ParticleSystem()
    {
        if (attack1ParticleSystem != null)
        {
            var go = Instantiate(attack1ParticleSystem, leftHandPartSystemGO.transform.position, leftHandPartSystemGO.transform.rotation);
            go.GetComponent<ParticleSystem>().Play();
        }
        else
            Debug.Log("playParticles function particle not asigned");
    }

    //public void PlayParticle(GameObject particle, GameObject position)
    //{
    //    if (particle != null)
    //    {
    //        Instantiate(particle, transform.position, transform.rotation);
    //        particle.GetComponent<ParticleSystem>().Play();
    //    }
    //    else
    //        Debug.Log("playParticles function particle not asigned");
    //}
    public void FinalBossDestroyParticle(GameObject particle)
    {
        if (particle != null) particle.GetComponent<ParticleSystem>().Stop();
    }

}
