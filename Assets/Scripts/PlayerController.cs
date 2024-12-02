using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float jumpPower = 3.0f;

    private InputManager inputManager;
    private Vector3 playerVelocity;
    private bool isGrounded = true;
    private Transform cameraTransform;
    public Transform playerBody;
    private Rigidbody rb;

    private EventInstance playerFootsteps;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;

        // Initialize footsteps event instance
        playerFootsteps = AudioManager.instance.CreateEventInstance(FMODEvents.instance.footsteps);
    }

    private void Update()
    {
        RotatePlayerCam();
        Movement();
        Jump();
        UpdateSound();
    }

    private void Movement()
    {
        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x * playerSpeed, rb.velocity.y, movement.y * playerSpeed);
        rb.velocity = transform.TransformDirection(move);
    }

    private void Jump()
    {
        if (inputManager.PlayerJumpThisFrame() && isGrounded)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.jump, this.transform.position);
            rb.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void RotatePlayerCam()
    {
        playerBody.rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void UpdateSound()
    {
        // Ensure 3D attributes are set
        playerFootsteps.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));

        // Check if player is moving on the ground
        if (isGrounded && (Mathf.Abs(rb.velocity.x) > 0.1f || Mathf.Abs(rb.velocity.z) > 0.1f))
        {
            // Get the playback state of the event
            PLAYBACK_STATE playbackState;
            playerFootsteps.getPlaybackState(out playbackState);

            if (playbackState == PLAYBACK_STATE.STOPPED)
            {
                playerFootsteps.start();
            }
        }
        else
        {
            // Stop the footsteps sound when the player is idle or not grounded
            playerFootsteps.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    private void OnDestroy()
    {
        // Release FMOD event instance to free resources
        playerFootsteps.release();
    }
}
