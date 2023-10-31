using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Enemy Base Stats")]
public class EnemyBaseStats : MonoBehaviour
{
    [SerializeField] float AttackRange = 5f;
    [SerializeField] float SeekRange = 5f;
}
