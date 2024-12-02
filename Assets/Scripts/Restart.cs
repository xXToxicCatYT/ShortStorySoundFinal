using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(Restarting()); 
    }
    IEnumerator Restarting()
    {
        yield return new WaitForSeconds(60);
        SceneManager.LoadScene(0);
    }
}
