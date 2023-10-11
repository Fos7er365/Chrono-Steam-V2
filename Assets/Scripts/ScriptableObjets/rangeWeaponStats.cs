using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Weapon/RangedWeaponStats", order = 1)]

public class rangeWeaponStats : ScriptableObject
{
    public float reloadTime = 3f;
    public int maxBullets = 10;
    public GameObject bulletPrefab;
}
