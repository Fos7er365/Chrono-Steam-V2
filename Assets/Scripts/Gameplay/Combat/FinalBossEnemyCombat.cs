using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossEnemyCombat : EnemyCombat
{
    [SerializeField] BossAI bossEnemyAIHandler;
    [SerializeField] float regularAttackThreshold, summonAttackThreshold, regularAttackTimer, enhancedAttackTimer, blockAttacksTimer;
    [SerializeField] float regularAttackCooldown, desperateAttackCooldown, blockAttacksCooldown;
    [SerializeField] float regenerationAmount;
    [SerializeField] Transform[] boidsSpawnPositions;
    [SerializeField] GameObject[] boidsPrefabs;
    [SerializeField]LayerMask wpContainerLayer;
    [SerializeField] float wpContainerSearchDist;
    int boidsCount;
    bool canSummon;
    GameObject wpContainerGO;

    float currentBlockTime = 0;
    FinalBossEnemyAnimations finalBossEnemyAnim;

    //Roulette Wheel variables
    Roulette _regularAttacksRoulette, _desperateAttacksRoulette;
    Dictionary<Node, int> _regularAttacksRouletteNodes = new Dictionary<Node, int>();
    Dictionary<Node, int> _desperateAttacksRouletteNodes = new Dictionary<Node, int>();
    //

    public float RegularAttackThreshold { get => regularAttackThreshold; set => regularAttackThreshold = value; }
    public float SummonAttackThreshold { get => summonAttackThreshold; set => summonAttackThreshold = value; }
    public float RegularAttackTimer { get => regularAttackTimer; set => regularAttackTimer = value; }
    public float EnhancedAttackTimer { get => enhancedAttackTimer; set => enhancedAttackTimer = value; }
    public float BlockAttacksTimer { get => blockAttacksTimer; set => blockAttacksTimer = value; }
    public bool CanSummon { get => canSummon; set => canSummon = value; }

    private void Start()
    {
        finalBossEnemyAnim = enemyAnim as FinalBossEnemyAnimations;
        RegularAttacksRouletteWheelSetUp();
        DesperateAttacksRouletteWheelSetUp();
    }
    private void Update()
    {
        Debug.Log("Final boss health is " + enemyModel.EnemyHealthController.CurrentLife);
    }
    public override void Attack()
    {
        bossEnemyAIHandler.BossObstaclAavoidanceSB.move = false;
        var dir = GameManager.Instance.PlayerInstance.transform.position - transform.position;
        transform.LookAt(dir);
        bossEnemyAIHandler.CurrentAttackTime += 1 * Time.deltaTime;
        if(bossEnemyAIHandler.CurrentAttackTime > regularAttackCooldown)
        {

            enemyAnim.AttackAnimation();
            bossEnemyAIHandler.CurrentAttackTime = 0;
        }

    }
    public void SummonAttack()
    {
        enemyModel.GetComponent<Rigidbody>().velocity = Vector3.zero;
        CheckWPContainersNearEnemy();
        //Collider[] coll = Physics.OverlapSphere(transform.position, wpContainerSearchDist, wpContainerLayer);
        if (wpContainerGO != null && canSummon)
        {
            //wpContainerGO = coll[0].gameObject;
            finalBossEnemyAnim.SummonAttackAnimation();
            foreach (var boid in boidsSpawnPositions)
            {
                if (boidsCount >= boidsSpawnPositions.Length - 1)
                {
                    Debug.Log("Enough boids spawned");
                    canSummon = false;
                    return;
                }
                Debug.Log("Boid instanced");
                var boidGO = Instantiate(boidsPrefabs[Random.Range(0, boidsPrefabs.Length - 1)], boid.position, Quaternion.identity);
                boidGO.gameObject.GetComponent<Enemy>().ObsAvoidance.waypointsContainer = wpContainerGO;
                
                boidsCount++;
            }
        }
    }
    void CheckWPContainersNearEnemy()
    {
        var go = GameObject.FindGameObjectsWithTag("WaipointContainer");
        if(go.Length > 0)
        {
            foreach (var wp in go)
            {
                var dist = Vector3.Distance(transform.position, wp.transform.position);
                if (dist < wpContainerSearchDist) wpContainerGO = wp.gameObject;
            }
        }
    }

    public void DesperateAttack()
    {
        bossEnemyAIHandler.BossObstaclAavoidanceSB.move = false;
        bossEnemyAIHandler.CurrentAttackTime += 1 * Time.deltaTime;

        if (bossEnemyAIHandler.CurrentAttackTime > desperateAttackCooldown)
        {
            DesperateRouletteAction();
            bossEnemyAIHandler.CurrentAttackTime = 0;
        }
    }

    public void BlockAttacks()
    {
        currentBlockTime += Time.deltaTime;
        finalBossEnemyAnim.BlockAttacksAnimation(true);
        enemyModel.EnemyHealthController.CurrentLife += regenerationAmount;
        if (enemyModel.EnemyHealthController.CurrentLife >= enemyModel.Stats.MaxHealth)
            enemyModel.EnemyHealthController.CurrentLife = enemyModel.Stats.MaxHealth * .6f;
        Debug.Log("curr health: " + enemyModel.EnemyHealthController.CurrentLife);
    }

    #region Regular Attack Roulette Wheel
    public void RegularAttacksRouletteWheelSetUp()
    {
        _regularAttacksRoulette = new Roulette();

        ActionNode attackAnim1 = new ActionNode(AttackAnimation1);
        ActionNode attackAnim2 = new ActionNode(AttackAnimation2);
        ActionNode attackAnim3 = new ActionNode(AttackAnimation3);

        _regularAttacksRouletteNodes.Add(attackAnim1, 35);
        _regularAttacksRouletteNodes.Add(attackAnim2, 20);
        _regularAttacksRouletteNodes.Add(attackAnim3, 10);

        ActionNode rouletteAction = new ActionNode(RegularRouletteAction);
    }

    public void RegularRouletteAction()
    {
        Node nodeRoulette = _regularAttacksRoulette.Run(_regularAttacksRouletteNodes);

        nodeRoulette.Execute();
    }

    void AttackAnimation1()
    {
        enemyAnim.AttackAnimation();
    }
    void AttackAnimation2()
    {
        enemyAnim.Attack2Animation();
    }
    void AttackAnimation3()
    {
        enemyAnim.Attack3Animation();
    }
    #endregion

    #region Desperate Attack Roulette Wheel
    public void DesperateAttacksRouletteWheelSetUp()
    {
        _desperateAttacksRoulette = new Roulette();

        ActionNode attackAnim1 = new ActionNode(DesperateAttackAnimation1);
        ActionNode attackAnim2 = new ActionNode(DesperateAttackAnimation2);
        ActionNode attackAnim3 = new ActionNode(DesperateAttackAnimation3);

        _desperateAttacksRouletteNodes.Add(attackAnim1, 20);
        _desperateAttacksRouletteNodes.Add(attackAnim2, 30);
        _desperateAttacksRouletteNodes.Add(attackAnim3, 18);

        ActionNode rouletteAction = new ActionNode(DesperateRouletteAction);
    }

    public void DesperateRouletteAction()
    {
        Node nodeRoulette = _desperateAttacksRoulette.Run(_desperateAttacksRouletteNodes);

        nodeRoulette.Execute();
    }

    void DesperateAttackAnimation1()
    {
        finalBossEnemyAnim.DesperateAttackAnimation1();
    }
    void DesperateAttackAnimation2()
    {
        finalBossEnemyAnim.DesperateAttackAnimation2();
    }
    void DesperateAttackAnimation3()
    {
        finalBossEnemyAnim.DesperateAttackAnimation3();
    }
    #endregion
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 10f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("FloorWeapon"))
        {
            var wp = collision.gameObject;
            if (wp != null)
            {
                var wpStats = collision.gameObject.GetComponent<Weapon>().WeaponStats;
                enemyModel.EnemyHealthController.GetDamage(wpStats.AttDamage);
            }
        }
    }
}
