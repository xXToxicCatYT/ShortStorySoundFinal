using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;
using FMOD.Studio;

public class ButtonScene : MonoBehaviour
{
    [Header("UI Animators")]
    public Animator fadeOut;
    public Animator fadeIn;
    public Animator vagrant;
    public Animator massacre;

    [Header("FMOD Settings")]
    private EventInstance buttonPressInstance;

    private int counter = 0; // Ensures only one button action can be triggered at a time

    private void Awake()
    {
        // Ensure the cursor is visible and unlocked
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Start()
    {
        // Create the FMOD sound instance for the button press
        try
        {
            buttonPressInstance = RuntimeManager.CreateInstance(FMODEvents.instance.buttonPress);

            // Optional: Set 3D positional attributes
            buttonPressInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to initialize button press sound: {ex.Message}");
        }
    }

    public void Revenge()
    {
        if (counter == 0)
        {
            PlayButtonSound();
            StartCoroutine(LoadSceneWithTransition(5, fadeIn, fadeOut, null));
            counter++;
        }
    }

    public void Vagrant()
    {
        if (counter == 0)
        {
            PlayButtonSound();
            StartCoroutine(LoadSceneWithTransition(6, vagrant, fadeOut, "VagIn"));
            counter++;
        }
    }

    public void Massacre()
    {
        if (counter == 0)
        {
            PlayButtonSound();
            StartCoroutine(LoadSceneWithTransition(7, massacre, fadeOut, "MasIn"));
            counter++;
        }
    }

    private void PlayButtonSound()
    {
        if (buttonPressInstance.isValid())
        {
            buttonPressInstance.start();
        }
        else
        {
            Debug.LogWarning("Button press sound instance is invalid!");
        }
    }

    private void OnDestroy()
    {
        // Properly release FMOD instance to free resources
        if (buttonPressInstance.isValid())
        {
            buttonPressInstance.release();
        }
    }

    /// <summary>
    /// Loads a scene with optional transition animations.
    /// </summary>
    /// <param name="sceneIndex">Scene index to load</param>
    /// <param name="entryAnimator">Animator for entry transition</param>
    /// <param name="exitAnimator">Animator for exit transition</param>
    /// <param name="entryTrigger">Optional trigger for entry animation</param>
    private IEnumerator LoadSceneWithTransition(int sceneIndex, Animator entryAnimator, Animator exitAnimator, string entryTrigger)
    {
        if (entryAnimator != null && !string.IsNullOrEmpty(entryTrigger))
        {
            entryAnimator.SetTrigger(entryTrigger);
        }

        yield return new WaitForSeconds(0.5f);

        if (exitAnimator != null)
        {
            exitAnimator.SetTrigger("Out");
        }

        yield return new WaitForSeconds(5);

        SceneManager.LoadScene(sceneIndex);
    }
}
