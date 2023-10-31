﻿using UnityEngine;

public class SpearWeapon : BladeWeapon
{
    public override void EspecialExecute()

    /* Unmerged change from project 'Assembly-CSharp.Player'
    Before:
        {

            if (currentDurability > 0)
    After:
        {

            if (currentDurability > 0)
    */
    {

        if (currentDurability > 0)
        {
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

            Collider[] Enemys = Physics.OverlapCapsule(_player.transform.position, _player.transform.position + _player.transform.forward * _espAreaStats.MaxDistance, _espAreaStats.MaxAmplitude);
            for (int i = Enemys.Length - 1; i >= 0; i--)
            {
                if (Enemys[i].gameObject != null)
                {
                    if (Enemys[i].gameObject.CompareTag("Enemy"))
                    {
                        if (hitCounter != null && !Enemys[i].gameObject.GetComponent<Enemy>().EnemyHealthController.isDead)
                        {
                            hitCounter.AddHitCounter();
                            FindObjectOfType<AudioManager>().Play("PlayerSwordHit");
                        }

                        Enemys[i].gameObject.GetComponent<Enemy>().EnemyHealthController.GetDamage(_weaponStats.EspDamage);

                    }
                }
            }
            _currentEspExeCd = 0;
        }

    }
}
