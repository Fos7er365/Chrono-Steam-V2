﻿using UnityEngine;

public class FistWeapon : BladeWeapon
{
    public override void WeaponSpecialAttack()
    {
        if (currentDurability > 0)
        {
            currentDurability -= WeaponStats.DurabilityDecrease;
            if (specialAttackVFXGO != null)
            {
                if (specialAttackVFXGO.CompareTag("FloorCrack"))
                {
                    specialAttackVFXGO.GetComponent<BoxDamageArea>().Create(_weaponStats.EspDamage, Vector3.zero);
                }
                else
                    Debug.Log("objVFX tag error");
            }

            _currentEspExeCd = 0;
        }
    }
    public override void AreaAtack()
    {
        Collider[] enemiesColl = Physics.OverlapSphere(_player.transform.position + _player.transform.forward * _areaStats.MaxDistance, _areaStats.MaxAmplitude);
        for (int i = enemiesColl.Length - 1; i >= 0; i--)
        {
            if (enemiesColl[i].gameObject != null)
            {
                if (enemiesColl[i].gameObject.CompareTag("Enemy")  || enemiesColl[i].gameObject.CompareTag("Final_Boss"))
                {
                    if (hitCounter != null && !enemiesColl[i].gameObject.GetComponent<Enemy>().EnemyHealthController.isDead)
                    {
                        hitCounter.AddHitCounter();
                        FindObjectOfType<AudioManager>().Play("PlayerSwordHit");
                        PlayParticles();
                    }
                    else
                        return;

                    enemiesColl[i].gameObject.GetComponent<Enemy>().EnemyHealthController.GetDamage(_weaponStats.AttDamage);

                }
            }
        }
    }

    void PlayParticles()
    {
        //for (int i = 0; i < particleSystems.Count; i++)
        //{

        //    particleSystems[i].Play();

        //}
    }

    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            if (_player != null)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawLine(_player.transform.position, _player.transform.position + _player.transform.forward * _areaStats.MaxDistance);
                Gizmos.DrawWireSphere(_player.transform.position + _player.transform.forward * _areaStats.MaxDistance, _areaStats.MaxAmplitude);
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(_player.transform.position, _espAreaStats.MaxAmplitude);
                Gizmos.DrawWireSphere(_player.transform.position + _player.transform.forward * _espAreaStats.MaxDistance, _espAreaStats.MaxAmplitude);
            }
        }

    }
}
