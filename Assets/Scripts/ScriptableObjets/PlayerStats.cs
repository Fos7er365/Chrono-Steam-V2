﻿using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Player/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [SerializeField]
    private float _speed = 10;
    [SerializeField]
    private float _dashDistance;
    [SerializeField]
    private float _dashDuration;
    [SerializeField]
    private float _dashCoolDown;
    [SerializeField]
    private float _maxLife;
    [SerializeField]
    private PlayerAbilitiesStats _abilitiStats;
    [SerializeField]
    private Transform _spawnPoint;
    [SerializeField]
    private GameObject _weapon;

    public float Speed { get => _speed; set => _speed = value; }

    public float DashDistance => _dashDistance;
    public float DashDuration => _dashDuration;
    public float DashCoolDown => _dashCoolDown;
    public float MaxLife => _maxLife;
    public Transform SpawnPoint1 => _spawnPoint;
    public GameObject Weapon { get => _weapon; set => _weapon = value; }
    public PlayerAbilitiesStats AbilitiStats => _abilitiStats;
}
