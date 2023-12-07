using UnityEngine;

public class HeavyWeapon : BladeWeapon

/* Unmerged change from project 'Assembly-CSharp.Player'
Before:
{
    
    public override void Execute()
After:
{

    public override void Execute()
*/
{

    public override void Execute()
    {
        base.Execute();
        #region debugcomprobation
        //for (int i = 0; i < EspParticleSystems.Count; i++)
        //{
        //    Debug.Log("Entered in SPS for");

        //    if (EspParticleSystems == null) Debug.Log("Special Particle System is null!");
        //    else Debug.Log("Special Particle System not null");

        //    EspParticleSystems[i].Play();
        //}
        #endregion
    }
    public override void WeaponSpecialAttack()
    {

        //Debug.Log("Entered in Heavy Weapon SE");
        if (currentDurability > 0)
        {
            currentDurability -= WeaponStats.DurabilityDecrease;

            if (_currentEspExeCd >= WeaponStats.EspExeCd)
            {
                for (int i = 0; i < espParticleSystems.Count; i++)
                {
                    #region debugcomprobation
                    //  Debug.Log("Entered in SPS for");

                    /*if (EspParticleSystems == null) Debug.Log("Special Particle System is null!");
                    else Debug.Log("Special Particle System not null");*/
                    #endregion
                    espParticleSystems[i].Play();
                }
                if (specialAttackVFXGO != null)
                    Instantiate(specialAttackVFXGO, _player.transform.position + _player.transform.forward * _espAreaStats.MaxDistance, Quaternion.identity);
                else
                    Debug.Log("objVFX = NULL");

                Collider[] enemiesColl = Physics.OverlapSphere(_player.transform.position + _player.transform.forward * _espAreaStats.MaxDistance, _espAreaStats.MaxAmplitude);
                for (int i = enemiesColl.Length - 1; i >= 0; i--)
                {
                    if (enemiesColl[i].gameObject != null)
                    {
                        if (enemiesColl[i].gameObject.CompareTag("Enemy") || enemiesColl[i].gameObject.CompareTag("Final_Boss"))
                        {
                            enemiesColl[i].gameObject.GetComponent<Enemy>().EnemyHealthController.GetDamage(WeaponStats.EspDamage);
                        }
                    }
                }

                _currentEspExeCd = 0;
            }
        }
    }
    public override void Update()
    {
        for (int i = 0; i < ParticleSystems.Count - 1; i++)
        {
            if (i <= 1)
            {
                ParticleSystems[i].Play();
            }
        }
        base.Update();
    }
    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            if (_player != null)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(_player.transform.position, _player.transform.position + _player.transform.forward * _areaStats.MaxDistance);
                Gizmos.DrawWireSphere(_player.transform.position, _areaStats.MaxAmplitude);
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireSphere(_player.transform.position + _player.transform.forward * _espAreaStats.MaxDistance, _espAreaStats.MaxAmplitude);

            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Final_Boss"))
        {
            collision.gameObject.GetComponent<Enemy>().EnemyHealthController.GetDamage(WeaponStats.AttDamage);
        }
    }


}
