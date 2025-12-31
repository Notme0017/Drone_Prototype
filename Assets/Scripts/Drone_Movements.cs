using UnityEngine;

public class Drone_Movements : MonoBehaviour
{
    [SerializeField]
    private Drone_Input input;

    [SerializeField]
    private float normalMoveSpeed = 10.0f;

    [SerializeField]
    private Rigidbody rb;

    public Transform cameraTransform;

    public float rotationSmoothTime = 0.1f;

    private float rotationSmoothVelocity;
    private float targetAngle;


    private void Awake()
    {
        if (input == null)
        {
            input = GetComponent<Drone_Input>();
        }

        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }


    private void FixedUpdate()
    {
        HorizontalMovement(input.strafe, input.throttle, normalMoveSpeed, rb);
        VerticalMovement(input.rise, normalMoveSpeed, rb);
    }

    private void HorizontalMovement(float strafe, float throttle, float moveSpeed, Rigidbody rb)
    {
        Vector3 dir = new Vector3(strafe, 0f, throttle);

        if (dir.magnitude > 0.001f)
        {
            dir.Normalize();
            targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
        }

        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSmoothVelocity, rotationSmoothTime);

        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        rb.linearVelocity = moveDirection.normalized * moveSpeed * dir.magnitude;
    }

    private void VerticalMovement(float rise, float moveSpeed, Rigidbody rb)
    {
        Vector3 verticalVeloctiy = new Vector3(0f, rise * moveSpeed, 0f);
        rb.linearVelocity += verticalVeloctiy;
    }
}
