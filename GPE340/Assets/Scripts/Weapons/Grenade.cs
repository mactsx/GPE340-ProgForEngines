using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float explosionRadius;
    public float explosionDelay;
    private float creationTime;
    public float damageDone;
    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        creationTime = Time.time;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = transform.TransformDirection(Vector3.forward * 20);
    }

    // Update is called once per frame
    void Update()
    {
        // Check when to explode
        if (Time.time >= creationTime + explosionDelay)
        {
            Explode();
        }
    }

    public void Explode()
    {
        // Create an explosion particle
        Instantiate(explosionParticle, transform.position, transform.rotation);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            Health health = hitCollider.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damageDone);
                Debug.Log(gameObject.name + " took " + damageDone + " damage from grenade.");
            }
        }

        Destroy(gameObject);
    }
}
