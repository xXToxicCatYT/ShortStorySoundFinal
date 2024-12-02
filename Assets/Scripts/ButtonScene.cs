using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScene : MonoBehaviour
{
    public Animator fadeOut;
    public Animator fadeIn;

    public Animator vagrant;
    public Animator massacre;

    private int counter;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        counter = 0;
    }

    public void Revenge()
    {
        if (counter == 0)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.buttonPress, this.transform.position);
            StartCoroutine(LoadLevelRevenge());
            counter++;
        }
    }   
    public void Vagrant()
    {
        if (counter == 0)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.buttonPress, this.transform.position);
            StartCoroutine(LoadLevelVagrant());
            counter++;
        }
    }    
    public void Massacre()
    {
        if (counter == 0)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.buttonPress, this.transform.position);
            StartCoroutine(LoadLevelMassacre());
            counter++;
        }
    }

    IEnumerator LoadLevelRevenge()
    {
        //Play animation
        fadeIn.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        fadeOut.SetTrigger("Out");
        //Wait
        yield return new WaitForSeconds(5);

        //Load scene
        SceneManager.LoadScene(5);
    }

    IEnumerator LoadLevelVagrant()
    {
        //Play animation
        vagrant.SetTrigger("VagIn");
        yield return new WaitForSeconds(1);
        fadeOut.SetTrigger("Out");
        //Wait
        yield return new WaitForSeconds(5);

        //Load scene
        SceneManager.LoadScene(6);
    }

    IEnumerator LoadLevelMassacre()
    {
        //Play animation
        massacre.SetTrigger("MasIn");
        yield return new WaitForSeconds(1);
        fadeOut.SetTrigger("Out");
        //Wait
        yield return new WaitForSeconds(5);

        //Load scene
        SceneManager.LoadScene(7);
    }
}
