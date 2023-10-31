

/* Unmerged change from project 'Assembly-CSharp.Player'
Before:
using System.Collections.Generic;

using UnityEngine;
After:
using System.Collections.Generic;
using TMPro;
using UnityEngine;
*/
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{

    [SerializeField] Enemy enemy;

    [SerializeField] Image enemyHealth;

    float enemyMaxHealth;
    float enemyCurrentHealth;

    // Update is called once per frame
    void Update()
    {
        enemyMaxHealth = enemy.Stats.MaxHealth;
        enemyCurrentHealth = enemy.EnemyHealthController.CurrentLife;

        UpdateHealth();
    }

    void UpdateHealth()
    {
        enemyHealth.fillAmount = enemyCurrentHealth / enemyMaxHealth;
    }

}
