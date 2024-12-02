using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuPlay : MonoBehaviour
{
    public void Play()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.buttonPress, this.transform.position);
        SceneManager.LoadScene(1);
    }
}
