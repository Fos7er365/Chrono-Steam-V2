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
    protected Enemy enemyModel;
    protected EnemyCombat combat;
    protected bool attackTarget;
    protected Player_Controller player;
    [SerializeField] float maxAttackCooldown = 1.25f;
    float attackCDTimer = 0f;

    public ObstacleAvoidance Obstacleavoidance { get => obstacleavoidance; set => obstacleavoidance = value; }

    //

    public virtual void Awake()
    {
        enemyModel = gameObject.GetComponent<Enemy>();
        sight = gameObject.GetComponent<ELineOfSight>();
        _seek = gameObject.GetComponent<Seek>();
        obstacleavoidance = gameObject.GetComponent<ObstacleAvoidance>();
        combat = gameObject.GetComponent<EnemyCombat>();
        //if(enemyModel.Stats.EnemyType != "Boss" )
            CreateDecisionTree();
    }
    private void Start()
    {
        player = GameManager.Instance.PlayerInstance.gameObject.GetComponent<Player_Controller>();
    }

    public virtual void Update()
    {
        if (player != null)
        {
            if (!enemyModel.IsDead && !player.Life_Controller.isDead)
                initialNode.Execute();
        }
    }
    protected virtual void CreateDecisionTree()
    {
        ActionNode AttackPlayer = new ActionNode(Attack);
        ActionNode Patrol = new ActionNode(Patrolling);
        ActionNode SeekPlayer = new ActionNode(Seeking);
        ActionNode Dead = new ActionNode(Die);

        QuestionNode inAttackRange = new QuestionNode(() => (Vector3.Distance(transform.position, GameManager.Instance.PlayerInstance.transform.position) < enemyModel.Stats.AttackRange), AttackPlayer, SeekPlayer);
        Debug.Log("stats por que tira error aaa " + sight + "sight tgt " +sight.Target + "Att range" + enemyModel.Stats.AttackRange);
        QuestionNode doIHaveTarget = new QuestionNode(() => (sight.targetInSight) || (enemyModel.IsHurt), inAttackRange, Patrol);

        QuestionNode playerAlive = new QuestionNode(() => !(enemyModel.Player.Life_Controller.isDead) && obstacleavoidance.waypointsContainer != null, doIHaveTarget, Patrol);

        QuestionNode doIHaveHealth = new QuestionNode(() => !(enemyModel.EnemyHealthController.isDead), playerAlive, Dead);

        initialNode = doIHaveHealth;
    }

    protected void AttackV2()
    {

        Debug.Log("Enemy big attack");
        _seek.move = false;
        obstacleavoidance.move = false;
        enemyModel.Animations.MovingAnimation(false);
        if (enemyModel.EnemyHealthController.CurrentLife > 0)
        {
            obstacleavoidance.move = false;
            _seek.move = false;
            Debug.Log("BTree Hitting player timer: " + attackCDTimer);
            if (sight.Target != null)
            {
                attackCDTimer += Time.deltaTime;
                if(attackCDTimer >= maxAttackCooldown)
                {
                    combat.IsAttacking = true;
                    //enemyModel.Animations.AttackAnimation();
                    //combat.RegularAttacksRouletteAction();
                    attackCDTimer = 0;
                }
            }
        }
        else combat.IsAttacking = false;
    }

    protected virtual void Attack()
    {
        Debug.Log("Enemy small attack");
        _seek.move = false;
        obstacleavoidance.move = false;
        enemyModel.Animations.MovingAnimation(false);
        if (enemyModel.EnemyHealthController.CurrentLife > 0)
        {
            obstacleavoidance.move = false;
            _seek.move = false;
            if (sight.Target != null)
            {
                combat.IsAttacking = true;
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

        Debug.Log("Enemy small patrol");
        _seek.move = false;
        combat.IsAttacking = false;
        obstacleavoidance.move = true;
        enemyModel.Animations.MovingAnimation(true);
    }
    protected virtual void Seeking()
    {
        Debug.Log("Enemy small seek");
        if (!combat.IsAttacking)
        {
            _seek.move = true;
            combat.IsAttacking = false;
            obstacleavoidance.move = false;
            enemyModel.Animations.MovingAnimation(true);
        }
    }
    protected virtual void Die()
    {
        Debug.Log("Enemy big die");
        Debug.Log("Enemy small die");
        _seek.move = false;
        combat.IsAttacking = false;
        obstacleavoidance.move = false;
    }
    //se llama desde el animator 
    public void AttackOver()
    {
        combat.IsAttacking = false;
    }
}