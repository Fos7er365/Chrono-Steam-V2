using UnityEngine;

public class Clap_controler : MonoBehaviour
{
    [SerializeField]
    private float damage;
    [SerializeField]
    private float lifeTime;
    [SerializeField]
    private float speed;
    private Rigidbody rb;
    private Vector3 dir;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        dir = GameManager.Instance.PlayerInstance.transform.position - transform.position;
        dir.y = 0;
    }

    private void FixedUpdate()
    {
        rb.velocity = dir.normalized * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = GameManager.Instance.PlayerInstance.GetComponent<Player_Controller>();
            if (!player.IsDashing)
            {
                player.Life_Controller.GetDamage(damage);
                Destroy();
            }
        }
    }
    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}
