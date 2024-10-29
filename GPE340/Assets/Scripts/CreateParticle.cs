using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateParticle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Spawn a new particle system
    

    public void SpawnParticle(ParticleSystem inPart)
    {
        
        Instantiate(inPart, gameObject.transform.position, gameObject.transform.rotation);
        
    }

}
