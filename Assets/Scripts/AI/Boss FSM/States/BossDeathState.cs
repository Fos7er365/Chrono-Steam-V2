﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Main.Scripts.FSM_SO_VERSION.States.BossStates
{
    [CreateAssetMenu(fileName = "Death State", menuName = "ScriptableObject/FSM States/Boss FSM States/Death State", order = 0)]
    public class BossDeathState : State
    {
        public override void EnterState(Enemy model)
        {
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
