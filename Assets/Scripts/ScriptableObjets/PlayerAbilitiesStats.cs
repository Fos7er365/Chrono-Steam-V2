using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Player/Player Abilities Stats")]
public class PlayerAbilitiesStats : ScriptableObject
{
    [SerializeField] float _punchDistance;
    [SerializeField] float _punchDuration;
    [SerializeField] float _punchCoolDown;
    [SerializeField] float _punchDamage;
    [SerializeField] float _punchArea;

    public float PunchDistance => _punchDistance;
    public float PunchDuration => _punchDuration;
    public float PunchCoolDown => _punchCoolDown;
    public float PunchDamage => _punchDamage;
    public float PunchArea => _punchArea;
}
