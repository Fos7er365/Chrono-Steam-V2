using System.Collections;
using UnityEngine;

public class EnemyCombat : Combat
{
    protected EnemyAnimations enemyAnim;
    protected bool isAttacking = false;

    public bool IsAttacking { get => isAttacking; set => isAttacking = value; }

    //private Animator animator;
    public virtual void Awake()
    {
        enemyModel = GetComponent<Enemy>();
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
            enemyModel.Animations.AttackAnimation();
        }
    }

    public void SequencedAttack()
    {
        enemyModel.Animations.AttackAnimation();
        enemyModel.Animations.Attack2Animation();
        enemyModel.Animations.Attack3Animation();
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
                    Player.gameObject.GetComponent<Player_Controller>().Life_Controller.GetDamage(enemyModel.Stats.MeleeDamage);
            }
        }
    }
}
