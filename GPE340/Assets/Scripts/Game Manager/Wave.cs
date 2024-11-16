using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Make visible in the inspector
[System.Serializable]
public class Wave
{
    // Each wave holds a list of pawns
    public List<Pawn> enemies;
}
