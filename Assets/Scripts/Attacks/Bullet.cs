using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float LifeTime;
    float _damage;
    float _distance;
    void Update()
    {
        var realSpeed = speed * Time.deltaTime;
        transform.Translate(0, 0, realSpeed);
        Destroy(gameObject, LifeTime);
    }
    public void Create(Vector3 pos, int damage, float distance)
    {
        _damage = damage;
        _distance = distance;
        var _player = GameManager.Instance.PlayerInstance;
        Instantiate(gameObject, pos, _player.gameObject.transform.rotation);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Final_Boss"))
        {
            float weapondamage = GameManager.Instance.PlayerInstance.GetComponent<Player_Controller>()
                                    .PlayerStats.Weapon.GetComponent<Weapon>().WeaponStats.EspDamage;
            other.gameObject.GetComponent<Enemy>().EnemyHealthController.GetDamage(weapondamage);
        }
        //if(!other.gameObject.CompareTag("Player") || !other.gameObject.CompareTag("FloorWeapon"))
        //    Destroy(this.gameObject);
    }
}
