using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Main.Scripts.FSM_SO_VERSION.States.BossStates
{
    [CreateAssetMenu(fileName = "Patrol State", menuName = "ScriptableObject/FSM States/Boss FSM States/Patrol State", order = 0)]
    public class BossPatrolState : State
    {
        Dictionary<Enemy, BossPatrolData> patrolData = new Dictionary<Enemy, BossPatrolData>();

        private class BossPatrolData
        {
            public Enemy bossModel;
            public BossAI bossAI;
            public Seek enemySeekSB;
            public Flee enemyFleeSB;
            public ObstacleAvoidance enemyObstacleAvoidanceSB;
            public EnemyAnimations enemyAnim;

            public BossPatrolData(Enemy model)
            {
                bossModel = model;
                bossAI = model.gameObject.GetComponent<BossAI>();
                enemySeekSB = model.gameObject.GetComponent<BossAI>().BossSeekSB;
                enemyFleeSB = model.gameObject.GetComponent<BossAI>().BossFleeSB;
                enemyObstacleAvoidanceSB = model.gameObject.GetComponent<BossAI>().BossObstaclAavoidanceSB;
                enemyAnim = model.gameObject.GetComponent<EnemyAnimations>();
            }

        }

        public override void EnterState(Enemy model)
        {
            if (!patrolData.ContainsKey(model)) patrolData.Add(model, new BossPatrolData(model));
        }

        public override void ExecuteState(Enemy model)
        {
            var dist = Vector3.Distance(patrolData[model].bossModel.transform.position, GameManager.Instance.PlayerInstance.transform.position);
            if (!patrolData[model].bossAI.BossLineOfSight.targetInSight && dist > patrolData[model].bossModel.Stats.SeekRange)
                Patrol(patrolData[model].bossModel);
            else
            {
                Debug.Log("Boss FSM paso a seek");
                patrolData[model].bossAI.FsmConditionsStats.IsInSight = true;
                patrolData[model].bossAI.FsmConditionsStats.IsInSeekRange = true;
            }

        }

        public override void ExitState(Enemy model)
        {
            patrolData[model].bossAI.FsmConditionsStats.CanPatrol = false;
            patrolData.Remove(model);
        }

        void Patrol(Enemy model)
        {
            patrolData[model].enemyFleeSB.move = false;
            patrolData[model].enemySeekSB.move = false;
            patrolData[model].enemyObstacleAvoidanceSB.move = true;
            patrolData[model].enemyAnim.MovingAnimation(true);
        }

    }
}
