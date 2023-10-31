using System.Collections;
using UnityEngine;

public class EnemyCombat : Combat
{
    Enemy _enemy;
    EnemyAnimations enemyAnim;
    protected bool isAttacking = false;

    public bool IsAttacking { get => isAttacking; set => isAttacking = value; }

    //private Animator animator;
    private void Start()
    {
        _enemy = gameObject.GetComponent<Enemy>();
        enemyAnim = GetComponent<EnemyAnimations>();
        //hitEnemies = new List<Collider>();
        //hitEnemies.Add( GameManager.Instance.PlayerInstance.GetComponent<Collider>() );
        //animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isAttacking) Attack();
    }

    public virtual void Attack()
    {
        if (gameObject.TryGetComponent<BossAI>(out var bossAI)) enemyAnim.AttackAnimation();
        else
        {
            Vector3.RotateTowards(transform.position, GameManager.Instance.PlayerInstance.transform.position, 2f, 1f);
            _enemy.Animations.AttackAnimation();
        }
    }

    public void SequencedAttack()
    {
        _enemy.Animations.AttackAnimation();
        _enemy.Animations.Attack2Animation();
        _enemy.Animations.Attack3Animation();
    }

    IEnumerator WaitToPlayNextAnim()
    {
        yield return new WaitForSeconds(.5f);
    }

    public virtual void OnAttack()
    {
        DoDamage();
    }

    public override void DoDamage()
    {
        base.DoDamage();

        // Damage them
        foreach (Collider Player in hitEnemies)
        {
            if (gameObject.TryGetComponent<BossAI>(out var bossAI))
            {
                enemyAnim.AttackAnimation();
                if (GameManager.Instance.PlayerInstance != null)
                    Player.gameObject.GetComponent<Player_Controller>().Life_Controller.GetDamage(bossAI.EnemyModel.Stats.MeleeDamage);
            }
            else
            {
                if (GameManager.Instance.PlayerInstance != null)
                    Player.gameObject.GetComponent<Player_Controller>().Life_Controller.GetDamage(_enemy.Stats.MeleeDamage);
            }
        }
    }
}
