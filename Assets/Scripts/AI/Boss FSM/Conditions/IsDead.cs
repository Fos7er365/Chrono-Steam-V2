using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Main.Scripts.FSM_SO_VERSION.Conditions.BossConditions
{
    [CreateAssetMenu(fileName = "Is Dead?", menuName = "ScriptableObject/FSM Conditions/Boss Conditions/Is Dead?")]
    public class IsDead : StateCondition
    {
        public override bool CompleteCondition(Enemy model)
        {
            //TODO manejar este boolean desde afuera, con un triggerenter en el mapa.
            //BossEnemyModel bossModel = model as BossEnemyModel;
            //return model.GetFSMData().CanPatrol;
            return model.IsDead;

        }
    }
}
