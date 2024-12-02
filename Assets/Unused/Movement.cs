using UnityEngine;


public class Movement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float jumpPower = 5f;

    Rigidbody rb;

    private InputManager inputManager;

    public Transform playerBody;
    public Transform cam;

    bool isGrounded = true;

    private Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 playerVelocity = new Vector3(movement.x * walkSpeed, rb.velocity.y, movement.y * walkSpeed);
        rb.velocity = transform.TransformDirection(playerVelocity);


        if (inputManager.PlayerJumpThisFrame() && isGrounded == true)
        {
            Jump();
            Debug.Log("Jumped");
        }

        LockCursor();
        RotatePlayerCam();
    }

    void Jump()
    {
        rb.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false;
    }

    void RotatePlayerCam()
    {
        playerBody.transform.rotation = Quaternion.Euler(0, cam.eulerAngles.y, 0);
    }
}
