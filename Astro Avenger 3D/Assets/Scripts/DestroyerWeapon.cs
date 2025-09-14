using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Lazer
{
    public Transform[] lazerSpawn;
    public int gunLazers;
}

[Serializable]
public class Rocket
{
    public GameObject rocket;
    public Transform[] rocketSpawn;
    public string playRocket;
    public bool isWTFBoom;
}

public class DestroyerWeapon : MonoBehaviour
{
    public GameObject lazer1;
    public GameObject lazer2;
    public GameObject lazer3;
    public GameObject lazer4;
    public GameObject lazer5;
    public GameObject lazerBurst1;
    public GameObject lazerBurst2;
    public GameObject lazerBurst3;
    public Transform brustSpawn;
    public Lazer[] lazers1 = new Lazer[5];
    public Lazer[] lazers2 = new Lazer[5];
    public Lazer[] lazers3 = new Lazer[5];
    public Lazer[] lazers4 = new Lazer[5];
    public Lazer[] lazers5 = new Lazer[5];
    public Rocket[] rockets = new Rocket[5];
    public Light dischargeLight;
    public ParticleSystem[] dischargesPS;
    public LineRenderer[] discharges;
}
