using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Drone_Health drone_Health;

    [SerializeField]
    private float bulletSpeed = 10.0f;

    public GameObject target;

    private Vector3 dir;

    private Rigidbody rb;

    public float bulletLifeSpan = 3.0f;

    private float timer;

    private int damage;

    private float initialDamage = 30f;

    private Vector3 initialPosition;
    private Vector3 currentPosition;

    [SerializeField]
    private float damageFallOffFactor = 0.03f;

    private void Start()
    {
        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Drone");
        }

        if(drone_Health == null)
        {
            drone_Health = target.GetComponent<Drone_Health>();
        }

        timer = 0f;

        initialPosition = transform.position;

        if(rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        dir = (target.transform.position - transform.position).normalized;
        Debug.Log("Direction: " + dir);
    }


    private void Update()
    {
        rb.linearVelocity = dir * bulletSpeed * Time.deltaTime;

        timer += Time.deltaTime;
        if(timer >= bulletLifeSpan)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentPosition = transform.position;
        DamageToBeDealt();
        if (collision.gameObject.CompareTag("Drone"))
        {
            drone_Health.OnHit(damage);
        }
        Destroy(this.gameObject);
    }

    private void DamageToBeDealt()
    {
        float distanceTraversed = Vector3.Distance(initialPosition, currentPosition);

        damage = Mathf.RoundToInt(initialDamage - distanceTraversed * damageFallOffFactor);
    }
}
