using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Planets
{
    public GameObject planet;
    public Vector3 planetPos;
    public Vector3 planetRot;
}

public class LandPlanets : MonoBehaviour
{
    public Planets[] planets;

    void Start()
    {
        for (int i = 0; i < planets.Length; i++)
        {
            Instantiate(planets[i].planet, planets[i].planetPos, Quaternion.Euler(planets[i].planetRot));
        }
    }
}
