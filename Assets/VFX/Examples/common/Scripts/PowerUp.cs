using System.Collections;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    [SerializeField] GameObject pickupEffect;
    [SerializeField] string powerUpType;
    [SerializeField] int propertyIncreaser;
    [SerializeField] float powerUpDuration;
    bool isHealthPowerUpActivated, isSpeedPowerUpActivated, isDurabilityPowerUpActivated, isDamagePowerUpActivated;
    Player_Controller playerController;
    [SerializeField] Rigidbody rb;

    private void Awake()
    {
        //GetComponent<Rigidbody>().useGravity = false;
        //GetComponent<Rigidbody>().isKinematic = false;
        //GetComponent<Collider>().isTrigger = true;
    }
    private void Start()
    {
        //rb.useGravity = false;
        //rb.useGravity = false;
        //rb.isKinematic = true;
        playerController = GameManager.Instance.PlayerInstance.GetComponent<Player_Controller>();
        //GetComponent<Collider>().isTrigger = true;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player speed power up pickup");
            Pickup();
        }
    }

    void Pickup()
    {
        //Instantiate(pickupEffect, transform.position, transform.rotation);
        HandlePowerUpEffectType();
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    void HandlePowerUpEffectType()
    {
        switch (powerUpType)
        {
            case "Health":
                IncreasePlayerHealth();
                break;
            case "Damage":
                IncreasePlayerWeaponDamage();
                break;
            case "Speed":
                IncreasePlayerSpeed();
                break;
            case "Durability":
                IncreasePlayerWeaponDurability();
                break;
        }
    }

    #region Power Up Handlers
    void IncreasePlayerHealth()
    {
        var playerHealth = playerController.Life_Controller.CurrentLife;
        playerHealth += propertyIncreaser;
    }
    void IncreasePlayerWeaponDamage()
    {
        if(!isDamagePowerUpActivated && playerController.PlayerStats.Weapon != null)
        {
            isDamagePowerUpActivated = true;
            playerController.TemporalPropertyIncrease(propertyIncreaser, powerUpDuration,
                playerController.PlayerStats.Weapon.GetComponent<WeaponStats>().AttDamage, isDamagePowerUpActivated);
        }
    }
    void IncreasePlayerSpeed()
    {
        if(!isSpeedPowerUpActivated)
        {
            isSpeedPowerUpActivated = true;
            playerController.TemporalPropertyIncrease(propertyIncreaser, powerUpDuration,
                playerController.PlayerStats.Speed, isSpeedPowerUpActivated);
        }
    }
    void IncreasePlayerWeaponDurability()
    {
        if(playerController.PlayerStats.Weapon != null)
        {
            var weaponDurability = playerController.PlayerStats.Weapon.GetComponent<Weapon>().WeaponStats.Durability;
            weaponDurability += propertyIncreaser;
        }

    }

    #endregion



}
