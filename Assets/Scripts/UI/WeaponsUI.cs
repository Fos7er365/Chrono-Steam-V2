﻿using UnityEngine;
using UnityEngine.UI;

public class WeaponsUI : MonoBehaviour
{
    [Header("Durability UI Properties")]
    [SerializeField]
    Image weaponDurabilityImage;
    float weaponDurability;
    float weaponMaxDurability;

    UIIconsManager uiIconsManager;

    public Image WeaponDurabilityImage { get => weaponDurabilityImage; set => weaponDurabilityImage = value; }
    public float WeaponDurability { get => weaponDurability; set => weaponDurability = value; }
    public float WeaponMaxDurability { get => weaponMaxDurability; set => weaponMaxDurability = value; }

    private void Start()
    {
    }

    private void Update()
    {
        if (GameManager.Instance.PlayerInstance.GetComponent<Player_Controller>().PlayerStats.Weapon != null)
        {
            Debug.Log("Weapon aaa", GameManager.Instance.PlayerInstance.GetComponent<Player_Controller>().PlayerStats.Weapon);
            weaponMaxDurability = GameManager.Instance.PlayerInstance.GetComponent<Player_Controller>().PlayerStats.Weapon.GetComponent<Weapon>().WeaponStats.Durability;
            weaponDurability = GameManager.Instance.PlayerInstance.GetComponent<Player_Controller>().PlayerStats.Weapon.GetComponent<Weapon>().currentDurability;
        }

    }

    public void DisplayDurability(Image durabilityUI, float value, float maxValue)
    {
        if (weaponDurability <= 0f)
            weaponDurability = 0f;

        durabilityUI.fillAmount = weaponDurability / weaponMaxDurability;
    }

}
