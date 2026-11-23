using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody rb;

    public float power = 1f;
    float multiplier = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * (power * multiplier), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
