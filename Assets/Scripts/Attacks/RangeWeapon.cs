using UnityEngine;

public class RangeWeapon : Weapon
{
    [SerializeField] private RangeWeaponStats _rangeStats;
    [SerializeField] private AreaStats _areaStats;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _bulletSpawner;
    //[SerializeField] private Transform _napalmSpawn;
    private float _currentReloadTime;
    private int bullets;
    public GameObject nRange;
    public RangeWeaponStats RangeStats { get => _rangeStats; }
    public GameObject BulletSpawner { get => _bulletSpawner; }
    public AreaStats AreaStats { get => _areaStats; }
    AudioManager audioMgr;

    //public Transform NapalmSpawn { get => _napalmSpawn;}

    public override void Start()
    {
        base.Start();
        bullets = _rangeStats.maxBullets;
        _currentReloadTime = _rangeStats.reloadTime;
        _player = GameObject.FindGameObjectWithTag("Player");
        audioMgr = FindObjectOfType<AudioManager>();
    }
    public override void Execute()
    {
        // Debug.Log("asdadasd");
        if (currentDurability > 0)
        {
            currentDurability -= _weaponStats.DurabilityDecrease;
            foreach (var item in ParticleSystems)
            {
                item.Play();

            }
            //Instantiate(_rangeStats.bulletPrefab, BulletSpawner.transform.position, BulletSpawner.transform.rotation);
            Collider[] enemiesColl = Physics.OverlapCapsule(_player.transform.position, _player.transform.position + _player.transform.forward * _areaStats.MaxDistance, _areaStats.MaxAmplitude);
            for (int i = enemiesColl.Length - 1; i >= 0; i--)
            {
                if (enemiesColl[i].gameObject != null)
                {
                    if (enemiesColl[i].gameObject.CompareTag("Enemy") || enemiesColl[i].gameObject.CompareTag("Final_Boss"))
                    {
                        if (/*hitCounter != null &&*/ !enemiesColl[i].gameObject.GetComponent<Enemy>().EnemyHealthController.isDead)
                        {
                            //hitCounter.AddHitCounter();
                            FindObjectOfType<AudioManager>().Play("PlayerSwordHit");
                        }

                        enemiesColl[i].gameObject.GetComponent<Enemy>().EnemyHealthController.GetDamage(_weaponStats.EspDamage);

                    }
                }
            }
            // Debug.Log($"Quedan {bullets} en el cargador");
            //Debug.Log($"Hice {_weaponStats.AttDamage} de daño con {name}");
        }
        _currentCD = _weaponStats.CoolDown;
    }

    public override void WeaponSpecialAttack()
    {
        #region debug comprobation
        //Debug.Log("Entered in Heavy Weapon SE");

        //for (int i = 0; i < EspParticleSystems.Count; i++)
        //{
        //    Debug.Log("Entered in SPS for");

        //    if (EspParticleSystems == null) Debug.Log("Special Particle System is null!");
        //    else Debug.Log("Special Particle System not null");

        //    EspParticleSystems[i].Play();
        //}
        #endregion
        if (currentDurability > 0)
        {
            currentDurability -= WeaponStats.DurabilityDecrease;

            for (int i = 0; i < espParticleSystems.Count; i++)
            {

                espParticleSystems[i].Play();
                if (!audioMgr.GetAudio("Gun_Weapon_Ultimate_B").Source.isPlaying) audioMgr.Play("Gun_Weapon_Ultimate_B");
            }

            nRange.GetComponent<SphereDamageArea>().Create(WeaponStats.EspDamage, _player.GetComponent<PlayerActions>().GunUIarea.transform.position);
            _player.GetComponent<Player_Controller>().IsSpecial = false;
            _player.GetComponent<LookAtMouse>().enabled = true;
            _currentEspExeCd = 0;

        }
    }

    public override void Update()
    {
        base.Update();
        if (bullets <= 0 || _currentReloadTime > 0)
        {
            //FindObjectOfType<AudioManager>().Stop("Gun_Weapon_Ultimate_B");
            // Debug.Log("Recargando");
            _currentReloadTime -= Time.deltaTime;
            bullets = _rangeStats.maxBullets;

            if (audioMgr.GetAudio("Gun_Weapon_Ultimate_B").Source.isPlaying) audioMgr.Stop("Gun_Weapon_Ultimate_B");
        }
    }
    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            if (_player != null)
            {
                Gizmos.DrawWireSphere(_player.transform.position, _areaStats.MaxAmplitude);
                Gizmos.DrawWireSphere(_player.transform.position + _player.transform.forward * _areaStats.MaxDistance, _areaStats.MaxAmplitude);
            }
        }
    }
}
