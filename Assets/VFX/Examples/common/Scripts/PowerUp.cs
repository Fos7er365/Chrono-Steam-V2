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

    private void Start()
    {
        playerController = GameManager.Instance.PlayerInstance.GetComponent<Player_Controller>();
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
        GameManager.Instance.PowerUpText.text = $"Health +{propertyIncreaser}";
        //StartCoroutine(WaitToDisableUI(1f));

    }
    void IncreasePlayerWeaponDamage()
    {
        if(!isDamagePowerUpActivated && playerController.PlayerStats.Weapon != null)
        {
            isDamagePowerUpActivated = true;
            playerController.TemporalPropertyIncrease(propertyIncreaser, powerUpDuration,
                playerController.PlayerStats.Weapon.GetComponent<Weapon>().WeaponStats.AttDamage, isDamagePowerUpActivated);
            GameManager.Instance.PowerUpText.text= $"Weapon damage +{propertyIncreaser} for {powerUpDuration} seconds";
            //StartCoroutine(WaitToDisableUI(1f));
        }
    }
    void IncreasePlayerSpeed()
    {
        if(!isSpeedPowerUpActivated)
        {
            isSpeedPowerUpActivated = true;
            playerController.TemporalPropertyIncrease(propertyIncreaser, powerUpDuration,
                playerController.PlayerStats.Speed, isSpeedPowerUpActivated);
            GameManager.Instance.PowerUpText.text= $"Speed +{propertyIncreaser} for {powerUpDuration} seconds";
            //StartCoroutine(WaitToDisableUI(1f));
        }
    }
    void IncreasePlayerWeaponDurability()
    {
        if(playerController.PlayerStats.Weapon != null)
        {
            var weaponDurability = playerController.PlayerStats.Weapon.GetComponent<Weapon>().WeaponStats.Durability;
            weaponDurability += propertyIncreaser;
            GameManager.Instance.PowerUpText.text= $"Weapon damage +{propertyIncreaser}";
            //StartCoroutine(WaitToDisableUI(1f));
        }

    }

    #endregion

}
