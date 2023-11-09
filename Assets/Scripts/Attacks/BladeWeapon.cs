using
/* Unmerged change from project 'Assembly-CSharp.Player'
Before:
using UnityEngine;
using TMPro;
After:
using TMPro;
using UnityEngine;
*/
UnityEngine;

public class BladeWeapon : Weapon, IAreaAttack
{
    [SerializeField] protected AreaStats _areaStats;
    [SerializeField] protected AreaStats _espAreaStats;

    [SerializeField] protected GameObject _player;
    [SerializeField] protected GameObject specialAttackVFXGO;
    [SerializeField] GameObject position;
    public AreaStats AreaStats { get => _areaStats; set => _areaStats = value; }

    public override void Start()
    {
        hitCounter = GameObject.FindGameObjectWithTag("hitCounter").GetComponent<HitCounter>();
        _currentCD = 0;
        _currentEspExeCd = WeaponStats.EspExeCd;
        currentDurability = _weaponStats.Durability;
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    public override void Execute()
    {
        AreaAtack();
        currentDurability -= _weaponStats.DurabilityDecrease;
        _currentCD = _weaponStats.CoolDown;
        //Debug.Log($"Hice {_weaponStats.AttDamage} de daño con {name} a rango melee de distancia");
    }
    public override void WeaponSpecialAttack()
    {

        //if (currentDurability > 0)
        //{
        //    currentDurability -= WeaponStats.DurabilityDecrease;
        //    for (int i = 0; i < espParticleSystems.Count; i++)
        //    {
        //        #region debugcomprobation
        //        // Debug.Log("Entered in SPS for");

        //        /*if (EspParticleSystems == null) Debug.Log("Special Particle System is null!");
        //        else Debug.Log("Special Particle System not null");*/
        //        #endregion
        //        espParticleSystems[i].Play();
        //    }

        //    Collider[] Enemys = Physics.OverlapCapsule(_player.transform.position, _player.transform.position + _player.transform.forward * _espAreaStats.MaxDistance, _espAreaStats.MaxAmplitude);
        //    for (int i = Enemys.Length - 1; i >= 0; i--)
        //    {
        //        if (Enemys[i].gameObject != null)
        //        {
        //            if (Enemys[i].gameObject.CompareTag("Enemy"))
        //            {
        //                if (hitCounter != null && !Enemys[i].gameObject.GetComponent<Enemy>().EnemyHealthController.isDead)
        //                {
        //                    hitCounter.AddHitCounter();
        //                    FindObjectOfType<AudioManager>().Play("PlayerSwordHit");
        //                }

        //                Enemys[i].gameObject.GetComponent<Enemy>().EnemyHealthController.GetDamage(_weaponStats.EspDamage);

        //            }
        //        }
        //    }
        //    _currentEspExeCd = 0;
        //}
        Debug.Log("Entered in Heavy Weapon SE");
        if (currentDurability > 0)
        {
            var spVFXGOChilds = GetComponentsInChildren<ParticleSystem>();
            currentDurability -= WeaponStats.DurabilityDecrease;
            for (int i = 0; i < espParticleSystems.Count; i++)
            {
                #region debugcomprobation
                // Debug.Log("Entered in SPS for");

                /*if (EspParticleSystems == null) Debug.Log("Special Particle System is null!");
                else Debug.Log("Special Particle System not null");*/
                #endregion
                espParticleSystems[i].Play();
            }

            if (specialAttackVFXGO != null)
            {
                if (specialAttackVFXGO.CompareTag("Bullet"))
                {
                    var go =Instantiate(specialAttackVFXGO, position.transform.position, Quaternion.identity);
                    go.GetComponent<ParticleSystem>().Play();
                    //specialAttackVFXGO.GetComponent<Bullet>().Create(_weaponStats.EspDamage, _espAreaStats.MaxDistance);
                }
                else
                    Debug.Log("objVFX tag error");
            }

            else
                Debug.Log("objVFX = NULL");
            _currentEspExeCd = 0;
        }
    }

    //public void PlayFinalBossSummonAttackParticleSystem()
    //{
    //    if (summonAttackParticleSystem != null)
    //    {
    //        var go = Instantiate(summonAttackParticleSystem, summonAttackPartSystemGO.transform.position, Quaternion.identity);
    //        FinalBossPlayAnidatedParticles(go);
    //    }
    //    else
    //        Debug.Log("playParticles function particle not asigned");
    //}

    public virtual void AreaAtack()
    {
        Collider[] enemies = Physics.OverlapCapsule(_player.transform.position, _player.transform.forward * _areaStats.MaxDistance, _areaStats.MaxAmplitude);

        /* Unmerged change from project 'Assembly-CSharp.Player'
        Before:
                for (int i = Enemys.Length-1; i >=0 ; i--)
        After:
                for (int i = Enemys.Length-1; i >= 0; i--)
        */
        for (int i = enemies.Length - 1; i >= 0; i--)
        {
            if (enemies[i].gameObject != null)
            {
                var baseEnemyTag = enemies[i].gameObject.CompareTag("Enemy");
                var turretEnemyTag = enemies[i].gameObject.CompareTag("Turret");
                var finalBossEnemyTag = enemies[i].gameObject.CompareTag("Final_Boss");
                //if ( baseEnemyTag || turretEnemyTag || finalBossEnemyTag)
                //{
                    CheckHit("Enemy", enemies[i]);
                    CheckHit("Turret", enemies[i]);
                    CheckHit("Final_Boss", enemies[i]);
                    //    if (hitCounter != null && !enemies[i].gameObject.GetComponent<Enemy>().EnemyHealthController.isDead)
                    //    {
                    //        hitCounter.AddHitCounter();
                    //        FindObjectOfType<AudioManager>().Play("PlayerSwordHit");
                    //    }
                    //    else
                    //        return;

                    //    enemies[i].gameObject.GetComponent<Enemy>().EnemyHealthController.GetDamage(_weaponStats.AttDamage);

                //}
            
             
            }
        }
    }

    void CheckHit(string enemyTypeTag, Collider enemyColl)
    {
        HealthController hpController = new HealthController(0);
        if (enemyTypeTag == "Turret" && enemyColl.gameObject.TryGetComponent<Turret>(out Turret coll))
        {
            hpController = coll.EnemyHealthController;
            hpController.GetDamage(_weaponStats.AttDamage);
            FindObjectOfType<AudioManager>().Play("PlayerSwordHit");
        }
        else if (enemyTypeTag == "Enemy" && enemyColl.gameObject.TryGetComponent<Enemy>(out Enemy turretColl))
        {
            hpController = turretColl.EnemyHealthController;
            hpController.GetDamage(_weaponStats.AttDamage);
            FindObjectOfType<AudioManager>().Play("PlayerSwordHit");
        }
        else if (enemyTypeTag == "Final_Boss" && enemyColl.gameObject.TryGetComponent<Enemy>(out Enemy finalBossColl))
        {
            hpController = finalBossColl.EnemyHealthController;
            hpController.GetDamage(_weaponStats.AttDamage);
            FindObjectOfType<AudioManager>().Play("PlayerSwordHit");
        }
        //if (/*hitCounter != null &&*/ !hpController.isDead)
        //{
        //    //hitCounter.AddHitCounter();
        //}
        else
            return;
    }

    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            if (_player != null)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawLine(_player.transform.position, _player.transform.position + _player.transform.forward * _areaStats.MaxDistance);
                Gizmos.DrawWireSphere(_player.transform.position, _areaStats.MaxAmplitude);
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(_player.transform.position, _espAreaStats.MaxAmplitude);
                Gizmos.DrawWireSphere(_player.transform.position + _player.transform.forward * _espAreaStats.MaxDistance, _espAreaStats.MaxAmplitude);
            }
        }

    }
}
