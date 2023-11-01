using UnityEngine;

public class RangeEnemyCombat : EnemyCombat
{
    [SerializeField] GameObject _enemyBulletPrefab;
    [SerializeField] float attPerSec = 3;
    float coolDown = 0;

    public GameObject EnemyBulletPrefab { get => _enemyBulletPrefab; }
    public override void OnAttack()
    {
        if (isAttacking && coolDown <= 0)
        {
            Instantiate(EnemyBulletPrefab, attackPoint.transform.position, attackPoint.transform.rotation);
            coolDown = attPerSec;
        }
        else coolDown -= Time.deltaTime;
    }
}