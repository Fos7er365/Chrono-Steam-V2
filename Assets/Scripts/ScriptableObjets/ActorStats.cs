using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ActorStats")]
public class ActorStats : ScriptableObject
{
    [SerializeField] float speed = 5f;
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float meleeDamage = 40f;
    [SerializeField] float attackRange = 40f;
    [SerializeField] float seekRange = 40f;
    [SerializeField] List<int> lifeRange = new List<int>(3);
    [SerializeField] string enemyType;

    public float Speed { get => speed; set => speed = value; }
    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float MeleeDamage { get => meleeDamage; set => meleeDamage = value; }
    public List<int> LifeRange { get => lifeRange; }
    public float AttackRange { get => attackRange; set => attackRange = value; }
    public float SeekRange { get => seekRange; set => seekRange = value; }
    public string EnemyType { get => enemyType; set => enemyType = value; }
}
