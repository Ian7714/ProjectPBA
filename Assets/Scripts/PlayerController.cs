using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 5f;

    public float sensX;
    public float sensY;
    float rotationX, rotationY;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * moveInput.y + right * moveInput.x) * speed;
        rb.linearVelocity = new Vector3(moveDirection.x, rb.linearVelocity.y, moveDirection.z);

        Vector2 mouseMovement = Mouse.current.delta.ReadValue();

        float mouseX = mouseMovement.x * sensX * Time.deltaTime;
        float mouseY = mouseMovement.y * sensY * Time.deltaTime;

        rotationY += mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        Camera.main.transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }

    Vector2 moveInput;
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
