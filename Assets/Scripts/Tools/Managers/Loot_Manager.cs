﻿using System.Collections.Generic;
using UnityEngine;

public class Loot_Manager : MonoBehaviour
{
    private Dictionary<GameObject, int> _weaponDrops = new Dictionary<GameObject, int>();
    private Dictionary<GameObject, int> _currentDrops = new Dictionary<GameObject, int>();
    [SerializeField] List<GameObject> drops;
    [SerializeField] List<int> rates;

    public Dictionary<GameObject, int> CurrentDrops => _currentDrops;

    public Dictionary<GameObject, int> WeaponDrops => _weaponDrops;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var d in drops)
        {
            foreach (var r in rates)
            {
                if(!_weaponDrops.ContainsKey(d)) _weaponDrops.Add(d, r);
                if (! _currentDrops.ContainsKey(d)) _currentDrops.Add(d, r);
            }
        }
        //for (int i = 0; i < drops.Count - 1; i++)
        //{
        //    _weaponDrops.Add(drops[i], rates[i]);
        //}
        //for (int i = 0; i < drops.Count - 1; i++)
        //{
        //    _currentDrops.Add(drops[i], rates[i]);
        //}
        GameManager.Instance.LootManager = this;
    }

    public void AddWeaponToLoot()
    {
        if (_currentDrops.Count < _weaponDrops.Count)
        {
            _currentDrops.Add(drops[_currentDrops.Count + 1], rates[_currentDrops.Count + 1]);
        }
    }
}
