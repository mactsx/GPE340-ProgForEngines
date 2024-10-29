using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class PickupWeapon : Pickup
{
    public Weapon weaponToEquip;
    public bool pickupSpin;
    public float spinSpeed;

    public void Update()
    {
        if (pickupSpin)
        {
            gameObject.transform.Rotate(0, 0 + (spinSpeed / 10), 0);
        }
    }

    // Add to the base onTrigger function
    public override void OnTriggerEnter(Collider other)
    {
        // Equip the weapon
        if (weaponToEquip != null)
        {
            Pawn otherPawn = other.GetComponent<Pawn>();
            if (otherPawn != null)
            {
                otherPawn.EquipWeapon(weaponToEquip);
            }
        }
        // Call base function to destroy self
        base.OnTriggerEnter(other);
    }
}
