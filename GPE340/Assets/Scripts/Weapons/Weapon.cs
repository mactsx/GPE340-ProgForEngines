using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [HideInInspector]
    public Pawn owner;

    public Sprite weaponSprite;

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

    [Header("Accuracy")]
    public float maxAccuracyOffset;

    // Get the accuracy rotation
    // A random percent between min and max accuracy rotation
    public virtual float GetAccuracyRotationDegrees(float accuracyMod = 2)
    {
        // Random number 0 - 1
        float accuracyDeltaPercent = Random.value;

        // Find the percent beween the negative and positive values of the offset
        float accuracyDeltaDegrees = Mathf.Lerp(-maxAccuracyOffset, maxAccuracyOffset, accuracyDeltaPercent);
        accuracyDeltaDegrees *= accuracyMod;

        return accuracyDeltaDegrees;
    }
}
