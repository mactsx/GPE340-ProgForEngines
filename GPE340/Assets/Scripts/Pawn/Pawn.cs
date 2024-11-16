using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    public Controller controller;

    [Header("Movement")]
    public float maxMoveSpeed;
    public float maxSprintSpeed;

    [Header("Rotation")]
    [Tooltip("How fast the camera and player rotate")]
    [Range(0f, 150f)]
    public float maxRotationSpeed;

    public Weapon weapon;
    public Weapon[] startingWeaponOptions;

    public virtual void Start()
    {
        // If there are weapons to be chosen from
        if (startingWeaponOptions.Length > 0)
        {
            // Equip random weapon
            EquipWeapon(startingWeaponOptions[UnityEngine.Random.Range(0, startingWeaponOptions.Length)]);
        }
        else
        {
            Debug.Log("No starting weapons to chose from");
        }
    }


    /// <summary>
    /// All variables that all pawn characters will have
    /// Every pawn will have a controller and some movement variables
    /// </summary>

    public abstract void Move(Vector3 direction, float speed);

    public abstract void Rotate(float speed);

    public abstract void RotateToLookAt(Vector3 target);

    public abstract void EquipWeapon(Weapon weaponToEquip);

    public abstract void UnequipWeapon();

}
