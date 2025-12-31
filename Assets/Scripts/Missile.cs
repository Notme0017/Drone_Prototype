using UnityEngine;
using System.Collections.Generic;

public class Missile : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private float movementSpeed = 25.0f;

    [SerializeField]
    private GameObject explosionParticleEffects;

    [SerializeField]
    private float missileLifeTime = 3.0f;

    [SerializeField]
    private float turnRate = 5000.0f;

    [SerializeField]
    private float radius = 1.0f;

    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    private void Start()
    {
        Destroy(this.gameObject, missileLifeTime);
    }

    private void FixedUpdate()
    {
        Vector3 desiredVel = -transform.forward * movementSpeed;
        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, desiredVel, Time.fixedDeltaTime * turnRate);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("target"))
        {
            Destroy(collision.gameObject);
        }
            Explode();
            Destroy(this.gameObject);
    }

    private void Explode()
    {
        GameObject.Instantiate(explosionParticleEffects, transform.position, Quaternion.identity);
        Vector3 explosionPostiton = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPostiton, radius);

        foreach(Collider hit in colliders)
        {
            if(hit.gameObject.tag == "target")
            {
                Destroy(hit.gameObject);
            }
        }

    }
}
