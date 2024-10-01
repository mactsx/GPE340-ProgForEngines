using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public Pawn pawn;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (pawn != null)
        {
            PossessPawn(pawn);
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // Movement make decisions
        MakeDecsions();
    }

    protected abstract void MakeDecsions();

    protected virtual void PossessPawn(Pawn pawnToPossess)
    {
        pawn = pawnToPossess;

        // set the pawns controller to this controller
        pawn.controller = this; 
    }

    protected virtual void UnpossessPawn()
    {
        // Set the pawn and its controller to null
        pawn.controller = null;

        pawn = null;
    }
}