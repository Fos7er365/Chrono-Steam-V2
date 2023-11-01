using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Main.Scripts.FSM_SO_VERSION.States.BossStates
{
    [CreateAssetMenu(fileName = "Seek State", menuName = "ScriptableObject/FSM States/Boss FSM States/Seek State", order = 0)]
    public class BossSeekState : State
    {
        Dictionary<Enemy, BossSeekData> seekData = new Dictionary<Enemy, BossSeekData>();

        private class BossSeekData
        {
            public Enemy bossModel;
            public BossAI bossAI;
            public Seek enemySeekSB;

            public BossSeekData(Enemy model)
            {
                bossModel = model;
                bossAI = model.gameObject.GetComponent<BossAI>();
                enemySeekSB = model.gameObject.GetComponent<BossAI>().BossSeekSB;
            }

        }

        public override void EnterState(Enemy model)
        {
            if (!seekData.ContainsKey(model)) seekData.Add(model, new BossSeekData(model));
        }

        public override void ExecuteState(Enemy model)
        {
            var dist = Vector3.Distance(seekData[model].bossModel.transform.position, GameManager.Instance.PlayerInstance.transform.position);
            if(dist > seekData[model].bossModel.Stats.AttackRange) Seek(seekData[model].bossModel);
            else
            {
                Debug.Log("Boss FSM paso a attack");
                seekData[model].bossAI.FsmConditionsStats.IsInAttackRange = true;
                seekData[model].bossAI.FsmConditionsStats.IsInSight = false;
                seekData[model].bossAI.FsmConditionsStats.IsInSeekRange = false;
            }

        }

        public override void ExitState(Enemy model)
        {
            seekData[model].bossAI.FsmConditionsStats.CanPatrol = false;
            seekData[model].bossAI.BossSeekSB.move = true;
            seekData.Remove(model);
        }

        void Seek(Enemy model)
        {
            Debug.Log("Boss FSM Seek");
            seekData[model].bossAI.BossSeekSB.move = true;
        }

    }
}
