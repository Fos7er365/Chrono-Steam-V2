using UnityEngine;

public class EnemyTorretBullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float LifeTime;
    [SerializeField] float _damage;
    float _distance;
    void Update()
    {
        var realSpeed = speed * Time.deltaTime;
        transform.Translate(0, 0, realSpeed);
        Destroy(gameObject, LifeTime);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player_Controller>().Life_Controller.GetDamage(_damage);
            Destroy(gameObject);
        }
    }
}
