using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoatBuoyancy : MonoBehaviour
{
    [Header("Water")]
    public float waterHeight = 0f;

    [Header("Buoyancy")]
    public float floatForce = 20f;
    public float damping = 5f;
    public float offset = 0f;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.linearDamping = 1f;
        rb.angularDamping = 2f;
    }

    void FixedUpdate()
    {
        float boatY = transform.position.y + offset;
        float depth = waterHeight - boatY;

        if (depth > 0f)
        {
            float upwardForce = depth * floatForce;
            float dampingForce = -rb.linearVelocity.y * damping;

            rb.AddForce(Vector3.up * (upwardForce + dampingForce), ForceMode.Force);
        }
    }
}
