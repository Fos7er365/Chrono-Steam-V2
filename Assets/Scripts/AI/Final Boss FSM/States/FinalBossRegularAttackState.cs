using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Main.Scripts.FSM_SO_VERSION.States.BossStates
{
    [CreateAssetMenu(fileName = "Regular Attack State", menuName = "ScriptableObject/FSM States/Final Boss FSM States/Regular Attack State", order = 0)]
    public class FinalBossRegularAttackState : State
    {
        FinalBossFSMStats stats;
        FinalBossEnemyCombat combat;
        BossAI ai;
        FinalBossEnemyAnimations anim;
        float timer = 0;

        public override void EnterState(Enemy model)
        {
            Debug.Log("Final Boss FSM REGULAR attack state ENTER");
            ai = model.gameObject.GetComponent<BossAI>();
            stats = ai.FsmConditionsStats as FinalBossFSMStats;
            combat = model.gameObject.GetComponent<FinalBossEnemyCombat>();
            anim = model.Animations as FinalBossEnemyAnimations;
        }

        public override void ExecuteState(Enemy model)
        {
            Debug.Log("Final Boss FSM REGULAR attack state EXECUTE");
            timer += Time.deltaTime;
            var dist = Vector3.Distance(model.gameObject.transform.position, GameManager.Instance.PlayerInstance.transform.position);

            combat.Attack();
            CheckTransitionToSummonAttack(model);
            CheckTransitionToSeek(dist, model);
            stats.IsBlocking = false;

            CheckTransitionToBlock(timer, model);
            //Debug.Log("reg attack timer");
            //if (timer >= 5)
            //{
            //    timer = 0;
            //    stats.IsBlocking = true;
            //}
        }

        public override void ExitState(Enemy model)
        {
            Debug.Log("Final Boss FSM REGULAR attack state EXIT");
        }

        void CheckTransitionToSummonAttack(Enemy model)
        {
            if(model.EnemyHealthController.CurrentLife < combat.RegularAttackThreshold)
            {
                stats.IsBelowAttackHealth = true;
            }
        }

        void CheckTransitionToSeek(float dist, Enemy model)
        {
            if (dist > model.Stats.AttackRange)
            {
                stats.IsInAttackRange = false;
            }
        }

        void CheckTransitionToBlock(float t, Enemy model)
        {
            if (t > 5) stats.IsBlocking = true;
            else stats.IsBlocking = false;
        }
    }
}
