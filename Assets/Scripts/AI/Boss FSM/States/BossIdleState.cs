using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Main.Scripts.FSM_SO_VERSION.States.BossStates
{
    [CreateAssetMenu(fileName = "Idle State", menuName = "ScriptableObject/FSM States/Boss FSM States/Idle State", order = 0)]
    public class BossIdleState : State
    {
        Dictionary<Enemy, BossIdleData> idleData = new Dictionary<Enemy, BossIdleData>();

        private class BossIdleData
        {
            public Enemy bossModel;
            public BossAI bossAI;
            public BossIdleData(Enemy model)
            {
                bossModel = model;
                bossAI = model.gameObject.GetComponent<BossAI>();
            }

        }

        public override void EnterState(Enemy model)
        {
            if (!idleData.ContainsKey(model)) idleData.Add(model, new BossIdleData(model));
            idleData[model].bossAI.FsmConditionsStats.CanPatrol = true;
            //bossModel = model as BossEnemyModel;
            //bossModel.EnemyView.PlayWalkAnimation(false);
            //bossModel.GetRigidbody().velocity = Vector3.zero;
        }

        public override void ExecuteState(Enemy model)
        {
        }

        public override void ExitState(Enemy model)
        {

        }
    }
}
