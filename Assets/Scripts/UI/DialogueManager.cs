using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Queue<string> sentences;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] Animator anim;
    bool isDialogueOver = false;

    public bool IsDialogueOver { get => isDialogueOver; set => isDialogueOver = value; }

    // Start is called before the first frame update
    void Awake()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue d)
    {
        sentences.Clear();
        anim.SetBool("isOpen", true);
        nameText.text = d.name;

        foreach (var s in d.sentences)
        {
            sentences.Enqueue(s);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string s = sentences.Dequeue();
        dialogueText.text = s;
        Debug.Log(s);
    }

    public void EndDialogue()
    {
        anim.SetBool("isOpen", false);
    }
}