using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewDialogueManager : MonoBehaviour
{

    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float speed;
    public Animator anim;
    int index;

    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("IsOpen", true);
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (var c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(speed);
        }
    }

    void NextLine()
    {
        if(index <lines.Length -1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            anim.SetBool("IsOpen", false);

            //gameObject.SetActive(false);
        }
    }

}
