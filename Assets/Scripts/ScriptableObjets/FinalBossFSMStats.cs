using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Final Boss FSM Conditions Stats", menuName = "ScriptableObject/FSM Conditions/Conditions Stats/Final Boss FSM Conditions Stats", order =0)]
public class FinalBossFSMStats : BossFSMConditionBooleansStats
{
    //FSM condition bool variables
    bool canRunFSM;
    //bool isInSight;
    //bool isInAttackRange;
    //bool isDamaged;
    //bool isDead;
    bool isBelowAttackHealth;
    bool isBlocking;
    bool isBelowSummonAttackHealth;

    public bool CanRunFSM { get => canRunFSM; set => canRunFSM = value; }
    public bool IsBelowAttackHealth { get => isBelowAttackHealth; set => isBelowAttackHealth = value; }
    public bool IsBlocking { get => isBlocking; set => isBlocking = value; }
    public bool IsBelowSummonAttackHealth { get => isBelowSummonAttackHealth; set => isBelowSummonAttackHealth = value; }
    //public bool IsInSight { get => isInSight; set => isInSight = value; }
    //public bool IsInAttackRange { get => isInAttackRange; set => isInAttackRange = value; }
    //public bool IsDamaged { get => isDamaged; set => isDamaged = value; }
    //public bool IsDead { get => isDead; set => isDead = value; }
}
