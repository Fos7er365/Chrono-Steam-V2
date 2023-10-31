using System.Collections.Generic;
using UnityEngine;
using _Main.Scripts.FSM_SO_VERSION;

[RequireComponent(typeof(ELineOfSight), typeof(Seek), typeof(Flee))]
[RequireComponent(typeof(ObstacleAvoidance), typeof(Enemy), typeof(EnemyCombat))]
[RequireComponent(typeof(EnemyAnimations))]
public class BossAI : MonoBehaviour
{

    ELineOfSight bossLineOfSight;
    //Steering Behaviours variables
    Seek bossSeekSB;
    Flee bossFleeSB;
    ObstacleAvoidance bossObstaclAavoidanceSB;
    //

    //Base script variables
    [SerializeField] float defaultAttackTime = 10f;
    float currentAttackTime = 0;
    bool isAttackingTarget;
    Enemy enemyModel;
    //

    //FSM variables
    FsmScript bossFSM;
    [SerializeField] StateData fsmInitialState;
    [SerializeField] BossFSMConditionBooleansStats fsmConditionsStats;
    //

    public Enemy EnemyModel { get => enemyModel; set => enemyModel = value; }
    public ELineOfSight BossLineOfSight { get => bossLineOfSight; set => bossLineOfSight = value; }
    public Seek BossSeekSB { get => bossSeekSB; set => bossSeekSB = value; }
    public Flee BossFleeSB { get => bossFleeSB; set => bossFleeSB = value; }
    public ObstacleAvoidance BossObstaclAavoidanceSB { get => bossObstaclAavoidanceSB; set => bossObstaclAavoidanceSB = value; }
    public float DefaultAttackTime { get => defaultAttackTime; set => defaultAttackTime = value; }
    public float CurrentAttackTime { get => currentAttackTime; set => currentAttackTime = value; }
    public BossFSMConditionBooleansStats FsmConditionsStats { get => fsmConditionsStats; set => fsmConditionsStats = value; }

    void Awake()
    {
        bossLineOfSight = gameObject.GetComponent<ELineOfSight>();
        bossSeekSB = gameObject.GetComponent<Seek>();
        bossFleeSB = gameObject.GetComponent<Flee>();
        bossObstaclAavoidanceSB = gameObject.GetComponent<ObstacleAvoidance>();
        enemyModel = gameObject.GetComponent<Enemy>();
    }
    void Start()
    {
        GameManager.Instance.LvlManager.GetComponent<LevelManager>().BossInstance = enemyModel;
        bossFSM = new FsmScript(enemyModel, fsmInitialState);
    }

    void Update()
    {
        if (!enemyModel.IsDead)
        {
            bossFSM.UpdateState();
            currentAttackTime += 1 * Time.deltaTime;
        }
    }
}
