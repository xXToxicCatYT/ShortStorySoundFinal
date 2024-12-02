using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
            playerIsClose = false;
            dialoguePanel.SetActive(false);
            dialogueActive = false;
            StopAllCoroutines();
            textComponent.text = string.Empty;
        }
    }
}
