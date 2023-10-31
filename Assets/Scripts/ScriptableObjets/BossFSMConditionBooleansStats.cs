using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/FSM Conditions/Conditions Stats/Boss FSM Conditions Stats")]
public class BossFSMConditionBooleansStats : ScriptableObject
{
    //FSM condition bool variables
    bool canPatrol;
    bool isInSight;
    bool isInSeekRange;
    bool isInAttackRange;
    bool isDamaged;
    bool isDead;

    public bool CanPatrol { get => canPatrol; set => canPatrol = value; }
    public bool IsInSight { get => isInSight; set => isInSight = value; }
    public bool IsInSeekRange { get => isInSeekRange; set => isInSeekRange = value; }
    public bool IsInAttackRange { get => isInAttackRange; set => isInAttackRange = value; }
    public bool IsDamaged { get => isDamaged; set => isDamaged = value; }
    public bool IsDead { get => isDead; set => isDead = value; }
}