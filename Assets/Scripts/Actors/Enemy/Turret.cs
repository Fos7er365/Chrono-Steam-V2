using UnityEngine;

public class Turret : MonoBehaviour
{
    private float rotTime;
    private float shootCd;
    ELineOfSight turretLineOfSight;
    HealthController enemyHealthController;
    [SerializeField] GameObject cannon;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject muzzleFlash;
    [SerializeField] int rotSpeed;
    [SerializeField] float rotTimeMax;
    [SerializeField] float shootCdMax;
    [SerializeField] bool inSight;
    [SerializeField] ActorStats turretStats;
    [SerializeField] int damage;
    [SerializeField] float distance;
    Player_Controller _player;

    public ActorStats TurretStats { get => turretStats; set => turretStats = value; }
    public HealthController EnemyHealthController { get => enemyHealthController; set => enemyHealthController = value; }

    private void Awake()
    {
        turretLineOfSight = GetComponentInChildren<ELineOfSight>();
    }
    private void Start()
    {
        enemyHealthController = new HealthController(turretStats.MaxHealth);
        //animator = GetComponent<Animator>();
        enemyHealthController.isDead = false;
        _player = GameManager.Instance.PlayerInstance.GetComponent<Player_Controller>();
        enemyHealthController.Dead.AddListener(Die);
        enemyHealthController.Damaged.AddListener(OnDamaged);
        //turretLineOfSight.VisionPoint.transform.position = cannon.transform.position;
        //turretLineOfSight.VisionPoint.transform.rotation = cannon.transform.rotation;
    }

    void OnDamaged()
    {

    }
    void Update()
    {
        IdleMovement();
        CheckForPlayer();
        Debug.Log("Turret curr health: " + enemyHealthController.CurrentLife);
        //Shoot();
    }

    private void IdleMovement()
    {
        transform.Rotate(0, 0, rotSpeed * -1);
        rotTime += Time.deltaTime;

        if (rotTime >= rotTimeMax)
        {
            rotSpeed *= -1;
            rotTime = 0;
        }
    }

    void CheckForPlayer()
    {
        if (turretLineOfSight.targetInSight) Shoot();
    }

    private void Shoot()
    {
        shootCd += Time.deltaTime;
        Quaternion cRot = cannon.transform.rotation;

        if (shootCd >= shootCdMax)
        {
            //muzzleFlash.SetActive(true);
            var muzzleFlashInstance = Instantiate(muzzleFlash, cannon.transform.position, cannon.transform.rotation);
            muzzleFlashInstance.GetComponent<ParticleSystem>().Play();
            var go = Instantiate(bullet, cannon.transform.position, cannon.transform.rotation);
            //bullet.GetComponent<Bullet>().Create(damage,distance);//Instantiate(bullet, cannon.transform.position, cannon.transform.rotation);
            shootCd = 0;
            Destroy(muzzleFlashInstance.gameObject, .5f);
        }
        //muzzleFlash.SetActive(false);
    }
    void Die()
    {
        //Debug.Log("Enemey died!");
        enemyHealthController.isDead = true;
        Destroy(gameObject, 1.5f);

    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("turret coll w " + other.collider.name);
        if (other.gameObject.CompareTag("FloorWeapon"))
        {
            enemyHealthController.GetDamage(other.gameObject.GetComponent<Weapon>().WeaponStats.AttDamage);
        }
    }
}