using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Material[] explosionMat;
    public Mesh[] part;
    private Light explosionLight;
    private ParticleSystemRenderer explosionPs;
    public ParticleSystemRenderer partPs;

    void Awake()
    {
        explosionLight = GetComponent<Light>();
        explosionPs = GetComponent<ParticleSystemRenderer>();
    }

    void Start ()
	{
        explosionPs.material = explosionMat[Random.Range(0, explosionMat.Length)];
        partPs.mesh = part[Random.Range(0, part.Length)];
    }
}
