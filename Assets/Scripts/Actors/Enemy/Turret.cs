using UnityEngine;

public class Turret : MonoBehaviour
{
    private float rotTime;
    private float shootCd;
    ELineOfSight turretLineOfSight;
    [SerializeField] GameObject cannon;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject muzzleFlash;
    [SerializeField] int rotSpeed;
    [SerializeField] float rotTimeMax;
    [SerializeField] float shootCdMax;
    [SerializeField] bool inSight;

    private void Awake()
    {
        turretLineOfSight = GetComponentInChildren<ELineOfSight>();
    }
    private void Start()
    {
        //turretLineOfSight.VisionPoint.transform.position = cannon.transform.position;
        //turretLineOfSight.VisionPoint.transform.rotation = cannon.transform.rotation;
    }

    void Update()
    {
        IdleMovement();
        CheckForPlayer();
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
        if (inSight) Shoot();
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
            Instantiate(bullet, cannon.transform.position, cannon.transform.rotation);
            shootCd = 0;
            Destroy(muzzleFlashInstance.gameObject, .5f);
        }
        //muzzleFlash.SetActive(false);
    }
}