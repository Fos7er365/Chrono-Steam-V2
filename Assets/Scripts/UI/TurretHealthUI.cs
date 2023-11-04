using UnityEngine;
using UnityEngine.UI;

public class TurretHealthUI : MonoBehaviour
{

    [SerializeField] Turret enemy;
    [SerializeField] Image enemyHealth, placeHolder;

    float enemyMaxHealth;
    float enemyCurrentHealth;

    // Update is called once per frame
    void Update()
    {
        enemyMaxHealth = enemy.TurretStats.MaxHealth;
        enemyCurrentHealth = enemy.EnemyHealthController.CurrentLife;

        UpdateHealth();

        if (enemyCurrentHealth <= 0) placeHolder.gameObject.SetActive(false);
    }

    void UpdateHealth()
    {
        enemyHealth.fillAmount = enemyCurrentHealth / enemyMaxHealth;
    }

}
