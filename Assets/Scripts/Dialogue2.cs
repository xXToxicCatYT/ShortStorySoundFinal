using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using System.Diagnostics.Tracing;

public class Dialogue2 : MonoBehaviour
{
    public Dialogue dialogue;

    public GameObject dialoguePanel;
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    public Animator transition;
    public float transitionTime = 5f;

    private int index;

    private bool playerIsClose;
    private bool dialogueActive;

    int dCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        dialoguePanel.SetActive(false);
        dialogueActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose && dialogueActive == false && dialogue.DialogueDone() == true)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.G1D1, this.transform.position);
            dialoguePanel.SetActive(true);
            StartDialogue();
            dialogueActive = true;
        }

        if (Input.GetMouseButtonDown(0) && dialogueActive == true)
        {
            if (textComponent.text == lines[index])
            {
                if (dCounter == 0)
                {
                    AudioManager.instance.PlayOneShot(FMODEvents.instance.G1D2, this.transform.position);
                }

                if (dCounter == 1)
                {
                    AudioManager.instance.PlayOneShot(FMODEvents.instance.G1D3, this.transform.position);
                }

                if (dCounter == 2)
                {
                    dCounter = 0;
                }

                NextLine();
                dCounter++;
            }
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //Play animation
        transition.SetTrigger("Start");

        //Wait
        yield return new WaitForSeconds(transitionTime);

        //Load scene
        SceneManager.LoadScene(levelIndex);
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

            AudioManager.instance.PlayOneShot(FMODEvents.instance.ambience, this.transform.position);
            LoadNextLevel();
        }
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
