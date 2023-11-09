using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletv2 : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float LifeTime;
    [SerializeField] float _damage;
    float _distance;
    Vector3 dir;

    private void Start()
    {
        dir = GameManager.Instance.PlayerInstance.transform.position - transform.position;
    }

    void Update()
    {
        var realSpeed = speed * Time.deltaTime;
        //transform.Translate(dir * realSpeed);
        transform.Translate(dir * realSpeed);
        Destroy(gameObject, LifeTime);
    }
    public void Create(int damage, float distance)
    {
        _damage = damage;
        _distance = distance;
        var _player = GameManager.Instance.PlayerInstance;
        Instantiate(gameObject, _player.transform.position, _player.transform.rotation);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player_Controller>().Life_Controller.GetDamage(_damage);
            Destroy(gameObject);
        }
        else if(other.gameObject.layer == 1 << LayerMask.NameToLayer("Wall")) Destroy(gameObject);
        //else if(!other.gameObject.CompareTag("Final_Boss")) 
    }
}