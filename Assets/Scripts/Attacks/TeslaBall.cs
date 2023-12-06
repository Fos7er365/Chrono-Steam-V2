using UnityEngine;

public class TeslaBall : MonoBehaviour
{
    [SerializeField] float rotSpeed;
    [SerializeField] float speed;
    [SerializeField] float damage;
    Player_Controller player;
    [SerializeField] ParticleSystem[] particles;

    Vector3 playerPos, direction;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Player_Controller>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        PlayParticles();

        playerPos = player.transform.position;

        Vector3 deltaVector = (playerPos - transform.position).normalized;
        deltaVector.y = 0;

        direction = (playerPos - transform.position).normalized;//deltaVector;

        transform.forward = Vector3.Lerp(transform.forward, direction, Time.deltaTime * rotSpeed);

        transform.position += transform.forward * speed * Time.deltaTime;

        Destroy(gameObject, 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.Life_Controller.GetDamage(damage);
            Destroy(gameObject);
        }
        else Destroy(gameObject);
    }

    void PlayParticles()
    {
        foreach (ParticleSystem ps in particles)
        {
            Debug.Log("Tesla Ball Rendered");
            ps.Play();
        }
    }
}
