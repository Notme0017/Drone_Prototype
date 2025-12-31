using UnityEngine;

public class Drone_Weapons : MonoBehaviour
{
    [SerializeField]
    private Drone_Input input;

    [SerializeField]
    private float fireRate = 0.5f;

    [SerializeField]
    private Transform missileFirePoint;

    public GameObject missilePrefab;

    private float nextFireTime;

    public Camera lookDirection;

    public AudioSource missileFire;

    private void Awake()
    {
        if(input == null)
        {
            input = GetComponent<Drone_Input>();
        }

        if(missileFire == null)
        {
            missileFire = GameObject.FindWithTag("MissileAudio").GetComponent<AudioSource>();
        }
    }

    private void Update()
    {
        nextFireTime += Time.deltaTime;
        nextFireTime = Mathf.Clamp(nextFireTime, 0f, fireRate + 0.1f);
        Debug.DrawRay(lookDirection.transform.position, lookDirection.transform.position, Color.red);
        if (input.fireMissile && nextFireTime >= fireRate)
        {
            FireMissile();
            nextFireTime = 0f;
        }
    }


    private void FireMissile()
    {
        if (missileFire != null)
        {
            missileFire.Play();
        }
        Ray ray = new Ray(lookDirection.transform.position, -lookDirection.transform.forward);

        Vector3 targetPosition;

        if (Physics.Raycast(ray, out RaycastHit hit, 500f))
            targetPosition = hit.point;
        else
            targetPosition = ray.origin + ray.direction * 200f;

        Vector3 dir = (targetPosition - missileFirePoint.position).normalized;
        Quaternion rot = Quaternion.LookRotation(dir);
        GameObject.Instantiate(missilePrefab, missileFirePoint.position, rot);
    }
}
