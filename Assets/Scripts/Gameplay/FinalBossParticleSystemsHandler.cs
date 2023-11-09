using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossParticleSystemsHandler : MonoBehaviour
{
    [SerializeField] GameObject leftHandPartSystemGO;
    [SerializeField] GameObject rightHandPartSystemGO;
    [SerializeField] GameObject damagePartSystemGO;
    [SerializeField] GameObject bodyCenterPartSystemGO;
    [SerializeField] GameObject frontPartSystemGO;
    [SerializeField] GameObject summonAttackPartSystemGO;
    [SerializeField] GameObject attack1ParticleSystem;
    [SerializeField] GameObject attack2ParticleSystem;
    [SerializeField] GameObject attack3ParticleSystem;
    [SerializeField] GameObject summonAttackParticleSystem;
    [SerializeField] GameObject death1ParticleSystem;
    [SerializeField] GameObject death2ParticleSystem;
    [SerializeField] GameObject death3ParticleSystem;

    [SerializeField] GameObject target;

    
    public void PlayFinalBossAttack1ParticleSystem()
    {
        if (attack1ParticleSystem != null)
        {
            var go = Instantiate(attack1ParticleSystem, leftHandPartSystemGO.transform.position, Quaternion.identity);
            go.GetComponent<ParticleSystem>().Play();
        }
        else
            Debug.Log("playParticles function particle not asigned");
    }
    public void PlayFinalBossAttack2ParticleSystem()
    {
        if (attack2ParticleSystem != null)
        {
            var go = Instantiate(attack2ParticleSystem, frontPartSystemGO.transform.position, Quaternion.identity);
            //var particles = go.GetComponentsInChildren<ParticleSystem>();
            //EnableParticles(particles);
        }
        else
            Debug.Log("playParticles function particle not asigned");
    }
    public void PlayFinalBossAttack3ParticleSystem()
    {
        var target = GameManager.Instance.PlayerInstance.transform;
        if (attack3ParticleSystem != null)
        {
            var go = Instantiate(attack3ParticleSystem, target.position, Quaternion.Euler(-90, 0, 0));
            FinalBossPlayAnidatedParticles(go);
        }
        else
            Debug.Log("playParticles function particle not asigned");
    }

    public void PlayFinalBossSummonAttackParticleSystem()
    {
        if (summonAttackParticleSystem != null)
        {
            var go = Instantiate(summonAttackParticleSystem, summonAttackPartSystemGO.transform.position, Quaternion.identity);
            FinalBossPlayAnidatedParticles(go);
        }
        else
            Debug.Log("playParticles function particle not asigned");
    }
    public void PlayFinalBossDeathPartSystem(GameObject go)
    {
        if (summonAttackParticleSystem != null)
        {
            var ps = Instantiate(go, bodyCenterPartSystemGO.transform.position, Quaternion.identity);
            FinalBossPlayAnidatedParticles(ps);
        }
        else
            Debug.Log("playParticles function particle not asigned");
    }
    public void FinalBossDestroyParticle(GameObject particle)
    {
        if (particle != null)
        {
            particle.GetComponent<ParticleSystem>().Stop();
            //Destroy(particle);
        }
    }
    public void FinalBossPlayAnidatedParticles(GameObject particle)
    {
        if (particle != null)
        {
            var go = particle.GetComponentsInChildren<ParticleSystem>();
            foreach (var p in go)
            {
                p.Play();
            }
            //Destroy(particle);
        }
    }
    public void FinalBossDestroyAnidatedParticles(GameObject particle)
    {
        if (particle != null)
        {
            var go = particle.GetComponentsInChildren<ParticleSystem>();
            foreach (var p in go)
            {
                p.Stop();
            }
            //Destroy(particle);
        }
    }
    void EnableParticles(ParticleSystem[] particles)
    {
        foreach (var sp in particles)
        {
            sp.Play();
        }
    }

}
