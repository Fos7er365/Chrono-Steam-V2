using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ELineOfSight), typeof(Seek))]
[RequireComponent(typeof(ObstacleAvoidance), typeof(Enemy), typeof(EnemyCombat))]
public class EnemyAI : MonoBehaviour
{
    protected Node initialNode;
    protected ELineOfSight sight;
    protected Seek _seek;
    protected ObstacleAvoidance obstacleavoidance;
    protected Enemy enemy;
    protected EnemyCombat combat;
    protected bool attackTarget;
    [SerializeField] float maxAttackCooldown = 1.25f;
    float attackCDTimer = 0f;

    public virtual void Awake()
    {
        enemy = gameObject.GetComponent<Enemy>();
        sight = gameObject.GetComponent<ELineOfSight>();
        _seek = gameObject.GetComponent<Seek>();
        obstacleavoidance = gameObject.GetComponent<ObstacleAvoidance>();
        combat = gameObject.GetComponent<EnemyCombat>();
        CreateDecisionTree();
    }

    public virtual void Update()
    {
        if (!enemy.Dead)
        {
            initialNode.Execute();
        }
    }
    protected virtual void CreateDecisionTree()
    {
        ActionNode AttackPlayer = new ActionNode(Attack);
        ActionNode Patrol = new ActionNode(Patrolling);
        ActionNode SeekPlayer = new ActionNode(Seeking);
        ActionNode Dead = new ActionNode(Die);

        QuestionNode inAttackRange = new QuestionNode(() => (Vector3.Distance(transform.position, sight.Target.position)) <= combat.AttackRange, AttackPlayer, SeekPlayer);

        QuestionNode doIHaveTarget = new QuestionNode(() => (sight.targetInSight) || (enemy.Hurt), inAttackRange, Patrol);

        QuestionNode playerAlive = new QuestionNode(() => !(enemy.Player.Life_Controller.isDead), doIHaveTarget, Patrol);

        QuestionNode doIHaveHealth = new QuestionNode(() => !(enemy.Life_Controller.isDead), playerAlive, Dead);

        initialNode = doIHaveHealth;
    }

    protected void AttackV2()
    {
        Debug.Log("BT Attack method");
        _seek.move = false;
        obstacleavoidance.move = false;
        enemy.Animations.MovingAnimation(false);
        if (enemy.Life_Controller.CurrentLife > 0)
        {
            obstacleavoidance.move = false;
            _seek.move = false;
            Debug.Log("BTree Hitting player timer: " + attackCDTimer);
            if (sight.Target != null)
            {
                attackCDTimer += Time.deltaTime;
                if(attackCDTimer >= maxAttackCooldown)
                {
                    combat.attack = true;
                    //enemy.Animations.AttackAnimation();
                    //combat.RegularAttacksRouletteAction();
                    attackCDTimer = 0;
                }
            }
        }
        else combat.attack = false;
    }

    protected virtual void Attack()
    {
        Debug.Log("BT Attack method");
        _seek.move = false;
        obstacleavoidance.move = false;
        enemy.Animations.MovingAnimation(false);
        if (enemy.Life_Controller.CurrentLife > 0)
        {
            obstacleavoidance.move = false;
            _seek.move = false;
            Debug.Log("BTree Hitting player");
            if (sight.Target != null)
            {
                combat.attack = true;
                //StartCoroutine(HandleAttackCooldown());
            }
        }
        else AttackOver();
    }

    IEnumerator HandleAttackCooldown()
    {
        yield return new WaitForSeconds(1f);
    }
    protected virtual void Patrolling()
    {
        _seek.move = false;
        combat.attack = false;
        obstacleavoidance.move = true;
        enemy.Animations.MovingAnimation(true);
    }
    protected virtual void Seeking()
    {
        if (!combat.attack)
        {
            _seek.move = true;
            combat.attack = false;
            obstacleavoidance.move = false;
            enemy.Animations.MovingAnimation(true);
        }
    }
    protected virtual void Die()
    {
        _seek.move = false;
        combat.attack = false;
        obstacleavoidance.move = false;
    }
    //se llama desde el animator 
    public void AttackOver()
    {
        combat.attack = false;
    }
}