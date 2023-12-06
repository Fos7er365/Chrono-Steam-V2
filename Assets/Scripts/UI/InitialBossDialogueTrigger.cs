using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialBossDialogueTrigger : MonoBehaviour
{
    LevelManager lvlMgr;

    private void Start()
    {
        lvlMgr = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (SceneManager.GetActiveScene().buildIndex == 2 && lvlMgr.BossDead)
            CheckEnabling(other);
    }

    void CheckEnabling(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Enabling dialogue post boss death");
            var go = gameObject.GetComponent<DialogueTrigger>();
            if (go.IsAvailableToShowDialogue) return;
            go.IsAvailableToShowDialogue = true;
            go.TriggerDialogue();

        }
    }
}
