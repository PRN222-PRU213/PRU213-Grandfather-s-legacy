using UnityEngine;
using UnityEngine.InputSystem;

public class BoatController : MonoBehaviour
{
    public float acceleration = 10f;
    public float maxSpeed = 12f;
    public float turnStrength = 1.5f;

    public float waterDrag = 1.5f;
    public float idleDrag = 3f;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
    }

    void FixedUpdate()
    {
        Vector2 move = InputManager.Instance.GetMovement();
        float h = move.x;
        float v = move.y;

        // 🚤 Di chuyển
        if (v != 0)
            rb.AddForce(transform.forward * v * acceleration, ForceMode.Acceleration);

        // ⛔ Giới hạn tốc độ
        if (rb.linearVelocity.magnitude > maxSpeed)
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;

        // 🔄 Xoay
        if (rb.linearVelocity.magnitude > 0.5f)
            rb.AddTorque(Vector3.up * h * turnStrength, ForceMode.Acceleration);

        // 🧷 Chống trôi ngang
        Vector3 localVel = transform.InverseTransformDirection(rb.linearVelocity);
        localVel.x = Mathf.Lerp(localVel.x, 0, 0.2f);
        rb.linearVelocity = transform.TransformDirection(localVel);

        // 🌊 Drag
        rb.linearDamping = (Mathf.Abs(v) > 0.1f) ? waterDrag : idleDrag;

        // 🔒 Giới hạn tốc độ xoay
        rb.angularVelocity = Vector3.ClampMagnitude(rb.angularVelocity, 1.2f);

        // 🛑 Giảm quán tính xoay khi không bấm A/D
        if (h == 0)
        {
            rb.angularVelocity = Vector3.Lerp(
                rb.angularVelocity,
                Vector3.zero,
                0.12f
            );
        }
    }
}
