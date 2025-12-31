using UnityEngine;

public class Drone_Input : MonoBehaviour
{
    public float throttle;
    public float strafe;
    public float rise;
    public bool fireMissile;
    private bool isRising;
    private float h;
    private float v;
    public DroneAudio droneAudio;

    private void Update()
    {
        throttle = Input.GetAxis("Vertical");
        strafe = Input.GetAxis("Horizontal");

        if(Input.GetKey(KeyCode.E))
        {
            rise = Mathf.Lerp(rise, -1.0f, Time.deltaTime * 5.0f);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            rise = Mathf.Lerp(rise, 1.0f, Time.deltaTime * 5.0f);
        }

        if(!Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.E))
        {
            rise = 0.0f;
        }

        //------------Audio Logic----------------
        float h = Mathf.Abs(throttle);
        float v = Mathf.Abs(strafe);
        bool isRising = Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E);

        if (h + v > 0.1f || isRising)
        {
            float intensity = Mathf.Clamp01(h + v + (isRising ? 0.5f : 0f));
            droneAudio.PlayThrottle(intensity);
        }
        else
        {
            droneAudio.PlayIdle();
        }

        //------------Missile Firing Logic----------------
        fireMissile = Input.GetKeyDown(KeyCode.Space);
    }
}
