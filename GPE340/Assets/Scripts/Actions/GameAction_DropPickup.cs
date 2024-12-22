using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAction_DropPickup : GameAction
{
    public GameObject HealthPickupPrefab;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public void DropItem()
    {
        Vector3 offset = new Vector3(0f, 1f, 0f);
        Instantiate(HealthPickupPrefab, transform.position + offset, transform.rotation);
    }
}
