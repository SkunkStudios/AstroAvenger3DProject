using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTime : MonoBehaviour
{
    public float time;

    void Start ()
	{
        Destroy(gameObject, time);
    }
}
