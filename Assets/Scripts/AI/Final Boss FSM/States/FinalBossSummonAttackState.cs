using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Main.Scripts.FSM_SO_VERSION.States.BossStates
{
    [CreateAssetMenu(fileName = "Summon Attack State", menuName = "ScriptableObject/FSM States/Final Boss FSM States/Summon Attack State", order = 0)]
    public class FinalBossSummonAttackState : State
    {
        FinalBossFSMStats stats;
        FinalBossEnemyCombat combat;
        BossAI ai;
        FinalBossEnemyAnimations anim;
        public override void EnterState(Enemy model)
        {
            Debug.Log("Final Boss FSM SUMMON attack state ENTER");
            ai = model.gameObject.GetComponent<BossAI>();
            stats = ai.FsmConditionsStats as FinalBossFSMStats;
            combat = model.gameObject.GetComponent<FinalBossEnemyCombat>();
            anim = model.Animations as FinalBossEnemyAnimations;

            combat.SummonAttack();
            var dist = Vector3.Distance(model.gameObject.transform.position, GameManager.Instance.PlayerInstance.transform.position);
            CheckTransitionToSeek(dist, model);
            HandleTransitionToDesperateAttackState(model);
            stats.IsBlocking = true;
        }

        public override void ExecuteState(Enemy model)
        {
            Debug.Log("Final Boss FSM SUMMON attack state EXECUTE");
        }

        public override void ExitState(Enemy model)
        {
            Debug.Log("Final Boss FSM SUMMON attack state EXIT");
        }
        void CheckTransitionToSeek(float dist, Enemy model)
        {
            if (dist > model.Stats.AttackRange)
            {
                stats.IsInAttackRange = false;
            }
        }
        void HandleTransitionToDesperateAttackState(Enemy model)
        {
            if (model.EnemyHealthController.CurrentLife > combat.SummonAttackThreshold)
                stats.IsBelowAttackHealth = true;
        }

    }
}
