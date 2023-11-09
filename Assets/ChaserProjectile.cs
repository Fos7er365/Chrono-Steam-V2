using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserProjectile : MonoBehaviour
{
    [SerializeField] float lifeTime;
    [SerializeField] float speed;
    [SerializeField] float rotSpeed;
    [SerializeField] float damage;
    GameObject target;
    ParticleSystem[] particles;
    Vector3 direction;
    float timer = 0;
    private void Start()
    {
        target = GameManager.Instance.PlayerInstance;
        particles = GetComponentsInChildren<ParticleSystem>();
        EnableParticles();
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= lifeTime)
        {
            DisableParticles();
            Destroy(gameObject);
        }
        if (target == null) return;
        else
        {
            var dir = target.transform.position - transform.position;
            if (target != null)
            {
                Vector3 deltaVector = (target.transform.position - transform.position).normalized;
                deltaVector.y = 0;
                direction = deltaVector;

                transform.position += Time.deltaTime * direction * speed;
                transform.forward = Vector3.Lerp(transform.forward, direction, Time.deltaTime * rotSpeed);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player_Controller>().Life_Controller.GetDamage(damage);
            DisableParticles();
            Destroy(this.gameObject);
        }
    }

    void EnableParticles()
    {
        foreach (var sp in particles)
        {
            sp.Play();
        }
    }
    void DisableParticles()
    {
        foreach (var sp in particles)
        {
            sp.Stop();
        }
    }
}
