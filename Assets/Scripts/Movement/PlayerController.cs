using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // Speed of the player movement
    public float rotationSpeed = 100f; // Speed of the player rotation
    private Transform cameraTransform; // Reference to the camera's transform
    [SerializeField]
    private Animator animator; // Reference to the Animator component

    // Use this for initialization
    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 moveDirection = (cameraTransform.forward * verticalInput + cameraTransform.right * horizontalInput).normalized;
        moveDirection.y = 0; // Ensure the player stays grounded

        if (moveDirection != Vector3.zero)
        {
            // Rotate the player to face the moving direction
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime * 100);

            // Set animator parameter to transition to "walk" animation
            animator.SetFloat("moveSpeed", moveDirection.magnitude);
        }
        else
        {
            // Idle state
            animator.SetFloat("moveSpeed", 0);
        }

        // Move the player
        transform.position += moveDirection * speed * Time.deltaTime;
    }
}
