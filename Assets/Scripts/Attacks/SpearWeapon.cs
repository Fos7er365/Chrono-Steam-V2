using UnityEngine;

public class SpearWeapon : BladeWeapon
{
    public override void WeaponSpecialAttack()

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

            Collider[] enemiesColl = Physics.OverlapCapsule(_player.transform.position, _player.transform.position + _player.transform.forward * _espAreaStats.MaxDistance, _espAreaStats.MaxAmplitude);
            for (int i = enemiesColl.Length - 1; i >= 0; i--)
            {
                if (enemiesColl[i].gameObject != null)
                {
                    if (enemiesColl[i].gameObject.CompareTag("Enemy") || enemiesColl[i].gameObject.CompareTag("Final_Boss"))
                    {
                        if (hitCounter != null && !enemiesColl[i].gameObject.GetComponent<Enemy>().EnemyHealthController.isDead)
                        {
                            hitCounter.AddHitCounter();
                            FindObjectOfType<AudioManager>().Play("PlayerSwordHit");
                        }

                        enemiesColl[i].gameObject.GetComponent<Enemy>().EnemyHealthController.GetDamage(_weaponStats.EspDamage);

                    }
                }
            }
            _currentEspExeCd = 0;
        }

    }
}
