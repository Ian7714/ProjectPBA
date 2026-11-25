using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    /*
    Assets List:
    Bowling and pin: https://www.turbosquid.com/FullPreview/520062
    Ramp: https://assetstore.unity.com/packages/3d/environments/roadways/anima-proxima-low-poly-racing-track-starter-pack-389272
    */

    Rigidbody rb;
    public float speed = 5f;

    public float sensX;
    public float sensY;
    float rotationX, rotationY;

    public GameObject ballPrefab;
    public GameObject hand;

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
        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
    }

    Vector2 moveInput;
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        Shoot(ballPrefab);
    }

    void Shoot(GameObject prefab)
    {
        Vector3 handEuler = hand.transform.eulerAngles;
        Vector3 fixXRot = new Vector3(45f, handEuler.y, handEuler.z);

        Instantiate(prefab, hand.transform.position, Quaternion.Euler(fixXRot));
    }

}
