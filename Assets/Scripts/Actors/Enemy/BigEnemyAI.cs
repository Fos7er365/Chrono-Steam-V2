using System.Collections.Generic;
using UnityEngine;

public class BigEnemyAI : EnemyAI
{
    Vector3 previousPlayerPos;
    Vector3 direction;
    float playerDistance;
    [SerializeField] float chargeSpeed;
    [SerializeField] float rotSpeed;
    [SerializeField] float chargeRange;
    [SerializeField] float chargeCooldown;
    [SerializeField] float stunDuration;
    bool doingCharge;
    bool collisionWithPlayer;
    private bool bossCharge;
    private float _currentChargeCD;
    float _currentStunDuration;

    public bool BossCharge { get => bossCharge; set => bossCharge = value; }

    public override void Awake()
    {
        base.Awake();
    }

    //// Start is called before the first frame update
    //public void Start()
    //{
    //    _currentChargeCD = chargeCooldown * 0.25f;
    //}
    // Update is called once per frame
    public override void Update()
    {
        if (!player.Life_Controller.isDead)
        {
            
            if (!enemyModel.IsDead)
            {
                initialNode.Execute();
                playerDistance = Vector3.Distance(transform.position, enemyModel.Player.transform.position);

                ResetCollissionWithPlayer();
                HandleEnemyStun();
            }
            else
            {
                _seek.move = false;
                combat.IsAttacking = false;
                obstacleavoidance.move = false;
            }

        }
    }

    void ResetCollissionWithPlayer()
    {

        if (collisionWithPlayer) collisionWithPlayer = false;
    }
    void HandleEnemyStun()
    {

        if (_currentStunDuration > 0)
        {
            _currentStunDuration -= Time.deltaTime;
            enemyModel.Player.Stunned = true;
        }
        else enemyModel.Player.Stunned = false;

    }

    //protected override void CreateDecisionTree()
    //{
    //    ActionNode AttackPlayer = new ActionNode(AttackV2);
    //    ActionNode Patrol = new ActionNode(Patrolling);
    //    ActionNode SeekPlayer = new ActionNode(Seeking);
    //    ActionNode SeekChargeCD = new ActionNode(SeekingyChargeCD);
    //    ActionNode Charge = new ActionNode(Charging);
    //    ActionNode Die = new ActionNode(base.Die);

    //    QuestionNode isInAttackRange = new QuestionNode(() => (playerDistance < enemyModel.Stats.AttackRange), AttackPlayer, SeekPlayer);
    //    //QuestionNode isInSeekRange = new QuestionNode(() => (playerDistance < combat.SeekRange), isInAttackRange, SeekPlayer);
    //    QuestionNode isChargeInCooldown = new QuestionNode(() => (_currentChargeCD > 0) && !doingCharge, /*SeekPlayer*/SeekChargeCD, Charge);
    //    QuestionNode isTooCloseToCharge = new QuestionNode(() => (playerDistance > chargeRange) || (doingCharge), isChargeInCooldown, isInAttackRange);
    //    QuestionNode isPlayerInSight = new QuestionNode(() => (sight.targetInSight), isTooCloseToCharge/*isInSeekRange*/, Patrol);
    //    QuestionNode isPlayerAlive = new QuestionNode(() => !(enemyModel.Player.Life_Controller.isDead), isPlayerInSight, Patrol);
    //    QuestionNode isAlive = new QuestionNode(() => !(enemyModel.EnemyHealthController.isDead), isPlayerAlive, Die);

    //    //QuestionNode inAttackRange = new QuestionNode(() => (playerDistance < combat.AttackRange) && !doingCharge, AttackPlayer, SeekPlayer);


    //    //QuestionNode isPlayerInSight = new QuestionNode(() => (sight.targetInSight), isTooCloseToCharge, Patrol);

    //    //QuestionNode isPlayerAlive = new QuestionNode(() => !(enemyModel.Player.Life_Controller.isDead), isPlayerInSight, Patrol);

    //    //QuestionNode isAlive = new QuestionNode(() => !(enemyModel.Life_Controller.isDead), isPlayerAlive, Die);

    //    initialNode = isAlive;
    //}
    protected override void Attack()
    {
        doingCharge = false;
        base.Attack();
    }

    protected override void Patrolling()
    {
        Debug.Log("Enemy big patrol");
        if(obstacleavoidance.waypointsContainer != null)
        {
            doingCharge = false;
            _seek.move = false;
            combat.IsAttacking = false;
            obstacleavoidance.move = true;
            enemyModel.Animations.MovingAnimation(true);
        }
    }
    protected override void Seeking()
    {
        Debug.Log("Enemy big seek");
        if (!combat.IsAttacking && !doingCharge)
        {
            doingCharge = false;
            _seek.move = true;
            combat.IsAttacking = false;
            obstacleavoidance.move = false;
            enemyModel.Animations.MovingAnimation(true);
            _currentChargeCD = chargeCooldown;
        }
    }
    protected virtual void SeekingyChargeCD()
    {
        Debug.Log("Enemy big seek charge cd");
        if (!combat.IsAttacking && !doingCharge)
        {
            doingCharge = false;
            _seek.move = true;
            combat.IsAttacking = false;
            obstacleavoidance.move = false;
            enemyModel.Animations.MovingAnimation(true);
        }
        _currentChargeCD -= Time.deltaTime;
        if (enemyModel.Player != null)
        {
            previousPlayerPos = enemyModel.Player.transform.position;
        }
    }

    void Charging()
    {
        Debug.Log("Enemy big charge");
        doingCharge = true;
        _seek.move = false;
        combat.IsAttacking = false;
        obstacleavoidance.move = false;

        //Consigo el vector entre el objetivo y mi posición
        Vector3 deltaVector = (previousPlayerPos - transform.position).normalized;
        deltaVector.y = 0;
        //Me guardo la dirección unicamente.
        direction = deltaVector;

        //Roto mi objeto hacia la dirección obtenida
        transform.forward = Vector3.Lerp(transform.forward, direction, Time.deltaTime * rotSpeed);
        //Muevo mi objeto
        transform.position += transform.forward * chargeSpeed * Time.deltaTime;

        if (Vector3.Distance(previousPlayerPos, transform.position) <= 0.5f || collisionWithPlayer)
        {
            _currentChargeCD = chargeCooldown;
            doingCharge = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && doingCharge)
        {
            collisionWithPlayer = true;
            _currentStunDuration = stunDuration;
            doingCharge = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, chargeRange);
    }
}
