using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;

    private bool playerIsClose;
    private bool dialogueActive;
    private bool dialogueDone;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        dialoguePanel.SetActive(false);
        dialogueActive = false;
        dialogueDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose && dialogueActive == false)
        {
            dialoguePanel.SetActive(true);
            StartDialogue();
            dialogueActive = true;
        }

        if (Input.GetMouseButtonDown(0) && dialogueActive == true)
        {
            if (textComponent.text == lines[index])
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
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            dialoguePanel.SetActive(false);
            dialogueActive = false;
            textComponent.text = string.Empty;
            dialogueDone = true;
            DialogueDone();
        }
    }

    public bool DialogueDone()
    {
        return dialogueDone;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsClose = false;
            dialoguePanel.SetActive(false);
            dialogueActive = false;
            StopAllCoroutines();
            textComponent.text = string.Empty;
        }
    }
}
