using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    public GameObject turrel;
    public GameObject enemy;
    public Transform[] turrelSpawns;
    public Transform[] enemySpawns;


    // Use this for initialization
    void Start ()
	{
        if (turrelSpawns.Length >= 1)
        {
            for (int i = 0; i < turrelSpawns.Length; i++)
            {
                Instantiate(turrel, turrelSpawns[i].position, turrelSpawns[i].rotation);
            }
        }
        if (enemySpawns.Length >= 1)
        {
            for (int i = 0; i < enemySpawns.Length; i++)
            {
                Instantiate(enemy, enemySpawns[i].position, enemySpawns[i].rotation);
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
