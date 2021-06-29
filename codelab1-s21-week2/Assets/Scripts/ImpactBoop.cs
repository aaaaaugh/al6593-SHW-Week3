using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactBoop : MonoBehaviour
{
    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        source.Play();
        
        print("lil boop"); //shows up on console
    }

}