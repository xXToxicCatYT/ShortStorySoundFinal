using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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

    int dCounter = 0;

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
            if (SceneManager.GetActiveScene().name == "FirstScene")
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.S1D1, this.transform.position);
            }

            if (SceneManager.GetActiveScene().name == "ThirdScene")
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.S2D1, this.transform.position);
            }

            dialoguePanel.SetActive(true);
            StartDialogue();
            dialogueActive = true;
        }

        if (Input.GetMouseButtonDown(0) && dialogueActive == true)
        {
            if (textComponent.text == lines[index])
            {
                if (SceneManager.GetActiveScene().name == "FirstScene" && dCounter == 0)
                {
                    AudioManager.instance.PlayOneShot(FMODEvents.instance.S1D2, this.transform.position);
                }

                if (SceneManager.GetActiveScene().name == "FirstScene" && dCounter == 1)
                {
                    AudioManager.instance.PlayOneShot(FMODEvents.instance.S1D3, this.transform.position);
                }

                if (SceneManager.GetActiveScene().name == "ThirdScene" && dCounter == 0)
                {
                    AudioManager.instance.PlayOneShot(FMODEvents.instance.S2D2, this.transform.position);
                }

                if (SceneManager.GetActiveScene().name == "ThirdScene" && dCounter == 1)
                {
                    AudioManager.instance.PlayOneShot(FMODEvents.instance.S2D3, this.transform.position);
                }

                if (SceneManager.GetActiveScene().name == "ThirdScene" && dCounter == 2)
                {
                    AudioManager.instance.PlayOneShot(FMODEvents.instance.S2D4, this.transform.position);
                }

                if (dCounter == 3)
                {
                    dCounter = 0;
                }

                NextLine();
                dCounter++;
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
            dCounter = 0;
            playerIsClose = false;
            dialoguePanel.SetActive(false);
            dialogueActive = false;
            StopAllCoroutines();
            textComponent.text = string.Empty;
        }
    }
}
