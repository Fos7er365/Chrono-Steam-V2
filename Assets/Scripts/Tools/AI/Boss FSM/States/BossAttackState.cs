using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Main.Scripts.FSM_SO_VERSION.States.BossStates
{
    [CreateAssetMenu(fileName = "Attack State", menuName = "ScriptableObject/FSM States/Boss FSM States/Attack State", order = 0)]
    public class BossAttackState : State
    {
        Dictionary<Enemy, BossAttackData> attackData = new Dictionary<Enemy, BossAttackData>();

        private class BossAttackData
        {
            public Enemy bossModel;
            public BossAI bossAI;
            public BossEnemyCombat bossCombatHandler;

            public BossAttackData(Enemy model)
            {
                bossModel = model;
                bossAI = model.gameObject.GetComponent<BossAI>();
                bossCombatHandler = model.gameObject.GetComponent<BossEnemyCombat>();
            }

        }

        public override void EnterState(Enemy model)
        {
            if (!attackData.ContainsKey(model)) attackData.Add(model, new BossAttackData(model));
            attackData[model].bossAI.BossSeekSB.move = false;
            attackData[model].bossAI.BossFleeSB.move = false;
        }

        public override void ExecuteState(Enemy model)
        {
            var dist = Vector3.Distance(attackData[model].bossModel.transform.position, GameManager.Instance.PlayerInstance.transform.position);
            if (dist < attackData[model].bossModel.Stats.AttackRange)
                attackData[model].bossCombatHandler.HandleAttacksRouletteWheel();
            else
            {
                attackData[model].bossAI.FsmConditionsStats.IsInSeekRange = true;
            }
        }

        public override void ExitState(Enemy model)
        {
            attackData[model].bossAI.FsmConditionsStats.IsInAttackRange = false;
            attackData.Remove(model);
        }
    }
}
