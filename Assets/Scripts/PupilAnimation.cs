using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PupilAnimation : MonoBehaviour
{
    public Animator animator;

    public Dialogue dialogue;
    public Dialogue2 dialogue2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && dialogue.DialogueDone() == true)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.armorJump, this.transform.position);
            animator.SetTrigger("Escape");
            StartCoroutine(DelaySceneChange());
        }
    }

    IEnumerator DelaySceneChange()
    {
        yield return new WaitForSeconds(2f);
        dialogue2.LoadNextLevel();
    }
}
