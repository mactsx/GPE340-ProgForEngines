using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAction_GrenadeL : WeaponAction
{
    public Transform firepoint;
 
    public GameObject grenadePrefab;
    private Grenade grenade;

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
        base.Update();
    }

    public void Shoot()
    {
        // See if the fire rate allows for another shot
        if (Time.time >= lastShotTime + secondsPerShot)
        {
            GameObject grenadeObject = Instantiate(grenadePrefab, firepoint.position, firepoint.rotation);
            grenadeObject.transform.Rotate(0, weapon.GetAccuracyRotationDegrees(), 0);    
            
     
            // Save the time of the latest shot
            lastShotTime = Time.time;
        }
    }
}
