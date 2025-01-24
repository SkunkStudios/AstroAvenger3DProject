using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public EnemyPath enemyPath;
    public GameObject laser;
    public GameObject burst;
    public Transform[] laserSpawns;
    public Transform[] laserSpawnsL;
    public Transform[] laserSpawnsR;
    public Transform[] burstSpawns;
    public Transform[] burstSpawnsL;
    public Transform[] burstSpawnsR;
    public float startTime;
    public float maxTime;
    public float maxRetime;
    public int maxLaserCount;
    public bool isLaserRotate;
    public string playName;

    private SoundClip soundClip;
    private float time;
    private int laserCount;
    private bool isLaser;

    void Start ()
	{
        soundClip = GameObject.FindObjectOfType<SoundClip>();
    }

    void Update ()
	{
        if (isLaser)
        {
            time += Time.deltaTime;
        }
        if (time >= startTime && laserCount == 0 || time >= maxTime && laserCount >= 1 && laserCount < maxLaserCount || time >= maxRetime && laserCount >= maxLaserCount)
        {
            LaserGun();
        }
        if (laserCount >= maxLaserCount && enemyPath != null)
        {
            enemyPath.NextWeapon();
            laserCount = 0;
            time = 0;
        }
    }

    void LaserGun()
    {
        soundClip.PlaySound(playName);
        if (laserCount >= maxLaserCount)
        {
            laserCount = 0;
        }
        time = 0;
        if (isLaserRotate)
        {
            if (laser != null)
            {
                Vector3 directionL = new Vector3(laserSpawnsL[laserCount].position.x, 0, laserSpawnsL[laserCount].position.z);
                Vector3 directionR = new Vector3(laserSpawnsR[laserCount].position.x, 0, laserSpawnsR[laserCount].position.z);
                Instantiate(laser, directionL, laserSpawnsL[laserCount].rotation);
                Instantiate(laser, directionR, laserSpawnsR[laserCount].rotation);
            }
            if (burst != null)
            {
                Instantiate(burst, burstSpawnsL[laserCount]);
                Instantiate(burst, burstSpawnsR[laserCount]);
            }
        }
        else
        {
            if (laserSpawns.Length >= 1)
            {
                for (int i = 0; i < laserSpawns.Length; i++)
                {
                    if (laser != null)
                    {
                        Vector3 direction = new Vector3(laserSpawns[i].position.x, 0, laserSpawns[i].position.z);
                        Instantiate(laser, direction, laserSpawns[i].rotation);
                    }
                }
            }
            if (burstSpawns.Length >= 1)
            {
                for (int i = 0; i < burstSpawns.Length; i++)
                {
                    if (burst != null)
                    {
                        Instantiate(burst, burstSpawns[i]);
                    }
                }
            }
        }
        laserCount++;
    }

    public void SetLaserBlast(bool isBlast)
    {
        isLaser = isBlast;
    }
}
