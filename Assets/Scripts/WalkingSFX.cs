using UnityEngine;

public class WalkingSFX : MonoBehaviour
{
    public AudioSource walkingSound; // Assign in the Inspector
    private bool isWalking = false;

    void Update()
    {
        // Check if the W key is pressed
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (!isWalking)
            {
                isWalking = true;
                walkingSound.Play();
            }
        }
        else
        {
            isWalking = false;
            walkingSound.Stop();
        }
    }
}