using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAction_Raygun : WeaponAction
{
    public float fireDistance;
    public Transform firepoint;

    private bool isAutofireActive;
    public GameObject beamPrefab;
    private LaserBeam beam;

    public ParticleSystem muzzleParticlePrefab;

    private float lastShotTime;
    private float secondsPerShot;

    public override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    public override void Start()
    {
        secondsPerShot = 1 / weapon.fireRate;
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (isAutofireActive)
        {
            Shoot();
        }

        base.Update();
    }
    public void Shoot()
    {
        // Create variable to hold raycast data
        RaycastHit hit;

        // See if the fire rate allows for another shot
        if (Time.time >= lastShotTime + secondsPerShot)
        {
            if (beam  == null)
            {
                GameObject beamObject = Instantiate(beamPrefab);
                beam = beamObject.GetComponent<LaserBeam>();

                // Do anything else that involves shooting for both primary and secondary fire
                weapon.OnShoot.Invoke();
            }
                
            beam.startPoint = firepoint.position;
            beam.endPoint = firepoint.position + transform.forward * fireDistance;
            
            
            // Cast the Ray and see if it hits something
            if (Physics.Raycast(firepoint.position, firepoint.forward, out hit, fireDistance))
            {
                // Try to get the health component of the target
                Health otherHealth = hit.collider.gameObject.GetComponent<Health>();
                // If it does have a health component
                if (otherHealth != null)
                {
                    otherHealth.TakeDamage(weapon.damageDone);
                }
            }
            // Save the time of the latest shot
            lastShotTime = Time.time;

            

        }
        
    }

    public void CreateParticle()
    {
        if (muzzleParticlePrefab != null)
        {
            Instantiate(muzzleParticlePrefab, firepoint.position, firepoint.rotation);
        }
    }

    public void AutofireBegin()
    {
        isAutofireActive = true;
    }
    public void AutofireEnd() 
    {
        isAutofireActive = false;
    }


}
