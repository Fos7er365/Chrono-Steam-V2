using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] Dialogue dialogue;
    [SerializeField] bool isAvailableToShowDialogue;
    public Dialogue Dialogue { get => dialogue; set => dialogue = value; }
    public bool IsAvailableToShowDialogue { get => isAvailableToShowDialogue; set => isAvailableToShowDialogue = value; }

    private void Start()
    {
        if(gameObject.tag == "Physical_Conversation_Trigger") TriggerDialogue();
    }
    public void TriggerDialogue()
    {
        if(isAvailableToShowDialogue)
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        //IsAvailableToShowDialogue = false;
    }
}
