using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Main.Scripts.FSM_SO_VERSION.States.BossStates
{
    [CreateAssetMenu(fileName = "Block Attack State", menuName = "ScriptableObject/FSM States/Final Boss FSM States/Block Attack State", order = 0)]
    public class FinalBossBlockState : State
    {
        FinalBossFSMStats stats;
        FinalBossEnemyCombat combat;
        BossAI ai;
        FinalBossEnemyAnimations anim;
        float timer = 0;

        public override void EnterState(Enemy model)
        {
            Debug.Log("Final Boss FSM BLOCK state ENTER");
            ai = model.gameObject.GetComponent<BossAI>();
            stats = ai.FsmConditionsStats as FinalBossFSMStats;
            combat = model.gameObject.GetComponent<FinalBossEnemyCombat>();
            anim = model.Animations as FinalBossEnemyAnimations;
        }

        public override void ExecuteState(Enemy model)
        {
            Debug.Log("Final Boss FSM BLOCK state EXECUTE");
            var dist = Vector3.Distance(model.gameObject.transform.position, GameManager.Instance.PlayerInstance.transform.position);
            timer += Time.deltaTime;
            if (timer <= combat.BlockAttacksTimer)
            {
                Debug.Log("Estoy bloqueando ataques");
                combat.BlockAttacks();
            }
            else
            {
                Debug.Log("Puedo salir de block state");
                anim.BlockAttacksAnimation(false);
                stats.IsBlocking = false;
                CheckTransitionToSeek(dist, model);
                CheckTransitionToRegularAttack(model);
                CheckTransitionToSummonAttack(model);

            }
        }

        public override void ExitState(Enemy model)
        {
            Debug.Log("Final Boss FSM BLOCK state EXIT");
        }
        void CheckTransitionToRegularAttack(Enemy model)
        {
            if (model.EnemyHealthController.CurrentLife > combat.RegularAttackThreshold)
            {
                stats.IsBlocking = false;
                stats.IsBelowAttackHealth = false;
                timer = 0;
            }
        }

        void CheckTransitionToSummonAttack(Enemy model)
        {
            if (model.EnemyHealthController.CurrentLife < combat.RegularAttackThreshold)
            {
                stats.IsBlocking = false;
                stats.IsBelowAttackHealth = true;
                timer = 0;
            }
        }

        void CheckTransitionToSeek(float dist, Enemy model)
        {
            if (dist > model.Stats.AttackRange)
            {
                stats.IsInAttackRange = false;
                timer = 0;
            }
        }

    }
}
