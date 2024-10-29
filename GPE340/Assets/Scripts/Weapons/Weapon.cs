using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent OnPrimaryAttackBegin;
    public UnityEvent OnPrimaryAttackEnd;
    public UnityEvent OnSecondaryAttackBegin;
    public UnityEvent OnSecondaryAttackEnd;
    public UnityEvent OnShoot;

    [Header("Details")]
    public float fireRate;
    public float damageDone;

    [Header("IK Animation Data")]
    public Transform RightHandIKTarget;
    public Transform LeftHandIKTarget;
}
