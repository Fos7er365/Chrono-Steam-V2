using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogueHandler : MonoBehaviour
{
    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
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

