using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Main.Scripts.FSM_SO_VERSION.States.BossStates
{
    [CreateAssetMenu(fileName = "Seek State", menuName = "ScriptableObject/FSM States/Final Boss FSM States/Seek State", order = 0)]
    public class FinalBossSeekState : State
    {
        FinalBossFSMStats stats;
        FinalBossEnemyCombat combat;
        BossAI ai;
        FinalBossEnemyAnimations anim;


        public override void EnterState(Enemy model)
        {
            Debug.Log("Final Boss FSM SEEK state ENTER");
            ai = model.gameObject.GetComponent<BossAI>();
            stats = ai.FsmConditionsStats as FinalBossFSMStats;
            combat = model.gameObject.GetComponent<FinalBossEnemyCombat>();
            anim = model.Animations as FinalBossEnemyAnimations;
        }

        public override void ExecuteState(Enemy model)
        {
            Debug.Log("Final Boss FSM SEEK state EXECUTE");
            ai.BossSeekSB.move = true;
            anim.MovingAnimation(true);
            var dist = Vector3.Distance(model.gameObject.transform.position, GameManager.Instance.PlayerInstance.transform.position);
            if (dist < model.Stats.AttackRange)
            {
                stats.IsInAttackRange = true;
                ai.BossSeekSB.move = false;
                anim.MovingAnimation(false);
                if (model.EnemyHealthController.CurrentLife <= combat.SummonAttackThreshold)
                    stats.IsBelowSummonAttackHealth = true;
                else if (model.EnemyHealthController.CurrentLife <= combat.RegularAttackThreshold && model.EnemyHealthController.CurrentLife > combat.SummonAttackThreshold)
                    stats.IsBelowAttackHealth = true;
                else if(model.EnemyHealthController.CurrentLife > combat.RegularAttackThreshold)
                    stats.IsBelowAttackHealth = false;

                //HandleTransitionToDesperateAttackState(model);
                //HandleTransitionToRegularAttackState(model);
                //HandleTransitionToSummonAttackState(model);
            }
        }

        public override void ExitState(Enemy model)
        {
            Debug.Log("Final Boss FSM SEEK state EXIT");
        }

        void HandleTransitionToRegularAttackState(Enemy model)
        {
            if (model.EnemyHealthController.CurrentLife > combat.RegularAttackThreshold)
                stats.IsBelowAttackHealth = false;
        }

        void HandleTransitionToSummonAttackState(Enemy model)
        {
            if (model.EnemyHealthController.CurrentLife <= combat.RegularAttackThreshold && model.EnemyHealthController.CurrentLife > combat.SummonAttackThreshold)
                stats.IsBelowAttackHealth = true;
        }
        void HandleTransitionToDesperateAttackState(Enemy model)
        {
            if (model.EnemyHealthController.CurrentLife < combat.SummonAttackThreshold)
                stats.IsBelowAttackHealth = true;
        }
    }
}
