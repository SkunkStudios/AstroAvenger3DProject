using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class Destroyer : MonoBehaviour
{
    public enum Lazer { Lazer1, Lazer2, Lazer3, Lazer4, Lazer5 }
    public Lazer lazers;
    public enum Rocket { Rocket1, Rocket2, Rocket3, Rocket4, Rocket5 }
    public Rocket rockets;
    public float speed;
    public float tilt;
    public Boundary boundary;
    public GameObject destroyerParts;
    public Transform destroyerMover;
    public GameObject explore;
    public Slider healthBar;
    public Slider energyBar;

    [Range(0, 4)]
    public int healthUpgrade;
    [Range(0, 4)]
    public int energyUpgrade;
    [Range(0, 4)]
    public int lazer1Upgrade;
    [Range(0, 4)]
    public int lazer2Upgrade;
    [Range(0, 4)]
    public int lazer3Upgrade;
    [Range(0, 4)]
    public int lazer4Upgrade;
    [Range(0, 4)]
    public int lazer5Upgrade;
    public int rocket1Count;
    public int rocket2Count;
    public int rocket3Count;
    public int rocket4Count;
    public int rocket5Count;
    public float[] maxHealth = new float[5];
    public float[] maxEnergy = new float[5];
    public DestroyerWeapon weapon;
    public AudioSource healthAlert;

    private float health;
    private float energy;
    private GameManager gameManager;
    private SoundClip soundClip;
    private Rigidbody rb;
    private float nextFire;
    private bool isFire;
    private bool isAlert;
    private GameObject[] enemys;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        soundClip = GameObject.FindObjectOfType<SoundClip>();
        rb = GetComponent<Rigidbody>();
        RestoreHealth();
        RestoreEnergy();
    }

    void Update()
    {
        if (enemys == null)
        {
            enemys = GameObject.FindGameObjectsWithTag("Enemy");
        }
        else if (enemys != null)
        {
            enemys = GameObject.FindGameObjectsWithTag("Enemy");
        }
        healthBar.value = health;
        healthBar.maxValue = maxHealth[healthUpgrade];
        energyBar.value = energy;
        energyBar.maxValue = maxEnergy[energyUpgrade];
        if (health <= maxHealth[healthUpgrade] * 0.25f && !isAlert)
        {
            healthAlert.Play();
            healthAlert.loop = true;
            isAlert = true;
        }
        if (health > maxHealth[healthUpgrade] * 0.25f && isAlert)
        {
            healthAlert.loop = false;
            isAlert = false;
        }
        if (health <= 0)
        {
            Instantiate(explore, transform.position, Quaternion.identity);
            soundClip.PlayExplosion();
            Instantiate(destroyerParts, destroyerMover.position, Quaternion.identity);
            RestoreHealth();
            RestoreEnergy();
            rb.position = new Vector3(0.0f, 0.0f, -17);
        }
        if (energy < maxEnergy[energyUpgrade])
        {
            energy += Time.deltaTime * 300;
        }
        else if (energy > maxEnergy[energyUpgrade])
        {
            energy = maxEnergy[energyUpgrade];
        }
        if (Input.GetButtonDown("Fire1"))
        {
            isFire = true;
            if (lazers == Destroyer.Lazer.Lazer3 && energy >= weapon.lazers3[lazer3Upgrade].gunLazers)
            {
                soundClip.PlaySound("destroyer_lazer4");
            }
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            nextFire = Time.time;
            isFire = false;
            for (int i = 0; i < 5; i++)
            {
                weapon.dischargesPS[i].Stop();
                weapon.discharges[i].enabled = false;
            }
        }
        if (isFire && Time.time > nextFire)
        {
            FireLazer();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            FireRocket();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            soundClip.PlaySound("rocketswitch");
            rockets++;
            if (rockets > Destroyer.Rocket.Rocket5)
            {
                rockets = Rocket.Rocket1;
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            soundClip.PlaySound("laserswitch");
            lazers++;
            if (lazers == Destroyer.Lazer.Lazer3 && isFire)
            {
                soundClip.PlaySound("destroyer_lazer4");
            }
            if (lazers > Destroyer.Lazer.Lazer5)
            {
                lazers = Lazer.Lazer1;
            }
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed * Time.fixedDeltaTime;

        rb.position = new Vector3(
                Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        destroyerMover.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }

    public void FireLazer()
    {
        GameObject b;

        if (lazers == Destroyer.Lazer.Lazer1 && Time.time > nextFire && energy >= weapon.lazers1[lazer1Upgrade].gunLazers)
        {
            soundClip.PlaySound("destroyer_lazer1");
            nextFire = Time.time + 0.15f;
            energy -= weapon.lazers1[lazer1Upgrade].gunLazers;

            if (weapon.lazerBurst1 != null)
            {
                Instantiate(weapon.lazerBurst1, weapon.brustSpawn);
            }

            for (int i = 0; i < 5; i++)
            {
                weapon.dischargesPS[i].Stop();
                weapon.discharges[i].enabled = false;
            }
            for (int i = 0; i < weapon.lazers1[lazer1Upgrade].lazerSpawn.Length; i++)
            {
                b = Instantiate(weapon.lazer1) as GameObject;
                b.transform.position = weapon.lazers1[lazer1Upgrade].lazerSpawn[i].position;
                b.transform.rotation = weapon.lazers1[lazer1Upgrade].lazerSpawn[i].rotation;
                b.GetComponent<Laser>().damage = weapon.lazers1[lazer1Upgrade].gunLazers;
            }
        }
        else if (lazers == Destroyer.Lazer.Lazer2 && Time.time > nextFire && energy >= weapon.lazers2[lazer2Upgrade].gunLazers)
        {
            soundClip.PlaySound("destroyer_lazer3");
            nextFire = Time.time + 0.15f;
            energy -= weapon.lazers2[lazer2Upgrade].gunLazers;

            if (weapon.lazerBurst2 != null)
            {
                Instantiate(weapon.lazerBurst2, weapon.brustSpawn);
            }

            for (int i = 0; i < 5; i++)
            {
                weapon.dischargesPS[i].Stop();
                weapon.discharges[i].enabled = false;
            }
            for (int i = 0; i < weapon.lazers2[lazer2Upgrade].lazerSpawn.Length; i++)
            {
                b = Instantiate(weapon.lazer2) as GameObject;
                b.transform.position = weapon.lazers2[lazer2Upgrade].lazerSpawn[i].position;
                b.transform.rotation = weapon.lazers2[lazer2Upgrade].lazerSpawn[i].rotation;
                b.GetComponent<Laser>().damage = weapon.lazers2[lazer2Upgrade].gunLazers;
            }
        }
        else if (lazers == Destroyer.Lazer.Lazer3 && Time.time > nextFire && energy < weapon.lazers3[lazer3Upgrade].gunLazers)
        {
            for (int i = 0; i < 5; i++)
            {
                weapon.dischargesPS[i].Stop();
                weapon.discharges[i].enabled = false;
            }
        }
        else if (lazers == Destroyer.Lazer.Lazer3 && Time.time > nextFire && energy >= weapon.lazers3[lazer3Upgrade].gunLazers)
        {
            if (enemys.Length == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    weapon.dischargesPS[i].Stop();
                    weapon.discharges[i].enabled = false;
                }
            }
            else if (enemys.Length >= 1)
            {
                nextFire = Time.time + 0.01f;
                energy -= weapon.lazers3[lazer3Upgrade].gunLazers;
                if (lazer3Upgrade == 0)
                {
                    if (enemys.Length >= 1)
                    {
                        b = Instantiate(weapon.lazer3, enemys[0].transform.position, Quaternion.identity) as GameObject;
                        weapon.discharges[0].enabled = true;
                        weapon.discharges[0].SetPosition(1, enemys[0].transform.position - transform.position);
                        weapon.dischargesPS[0].transform.position = enemys[0].transform.position;
                        weapon.dischargesPS[0].Play();
                        weapon.dischargesPS[1].Stop();
                        weapon.dischargesPS[2].Stop();
                        weapon.dischargesPS[3].Stop();
                        weapon.dischargesPS[4].Stop();
                        weapon.discharges[1].enabled = false;
                        weapon.discharges[2].enabled = false;
                        weapon.discharges[3].enabled = false;
                        weapon.discharges[4].enabled = false;
                        b.GetComponent<Laser>().damage = weapon.lazers3[lazer3Upgrade].gunLazers;
                    }
                }
                else if (lazer3Upgrade == 1)
                {
                    if (enemys.Length == 1)
                    {
                        b = Instantiate(weapon.lazer3, enemys[0].transform.position, Quaternion.identity) as GameObject;
                        weapon.discharges[0].enabled = true;
                        weapon.discharges[0].SetPosition(1, enemys[0].transform.position - transform.position);
                        weapon.dischargesPS[0].transform.position = enemys[0].transform.position;
                        weapon.dischargesPS[0].Play();
                        weapon.dischargesPS[1].Stop();
                        weapon.dischargesPS[2].Stop();
                        weapon.dischargesPS[3].Stop();
                        weapon.dischargesPS[4].Stop();
                        weapon.discharges[1].enabled = false;
                        weapon.discharges[2].enabled = false;
                        weapon.discharges[3].enabled = false;
                        weapon.discharges[4].enabled = false;
                        b.GetComponent<Laser>().damage = weapon.lazers3[lazer3Upgrade].gunLazers;
                    }
                    if (enemys.Length >= 2)
                    {
                        b = Instantiate(weapon.lazer3, enemys[0].transform.position, Quaternion.identity) as GameObject;
                        b = Instantiate(weapon.lazer3, enemys[1].transform.position, Quaternion.identity) as GameObject;
                        weapon.discharges[0].enabled = true;
                        weapon.discharges[1].enabled = true;
                        weapon.discharges[0].SetPosition(1, enemys[0].transform.position - transform.position);
                        weapon.discharges[1].SetPosition(1, enemys[1].transform.position - transform.position);
                        weapon.dischargesPS[0].transform.position = enemys[0].transform.position;
                        weapon.dischargesPS[1].transform.position = enemys[1].transform.position;
                        weapon.dischargesPS[0].Play();
                        weapon.dischargesPS[1].Play();
                        weapon.dischargesPS[2].Stop();
                        weapon.dischargesPS[3].Stop();
                        weapon.dischargesPS[4].Stop();
                        weapon.discharges[2].enabled = false;
                        weapon.discharges[3].enabled = false;
                        weapon.discharges[4].enabled = false;
                        b.GetComponent<Laser>().damage = weapon.lazers3[lazer3Upgrade].gunLazers;
                    }
                }
                else if (lazer3Upgrade == 2)
                {
                    if (enemys.Length == 1)
                    {
                        b = Instantiate(weapon.lazer3, enemys[0].transform.position, Quaternion.identity) as GameObject;
                        weapon.discharges[0].enabled = true;
                        weapon.discharges[0].SetPosition(1, enemys[0].transform.position - transform.position);
                        weapon.dischargesPS[0].transform.position = enemys[0].transform.position;
                        weapon.dischargesPS[0].Play();
                        weapon.dischargesPS[1].Stop();
                        weapon.dischargesPS[2].Stop();
                        weapon.dischargesPS[3].Stop();
                        weapon.dischargesPS[4].Stop();
                        weapon.discharges[1].enabled = false;
                        weapon.discharges[2].enabled = false;
                        weapon.discharges[3].enabled = false;
                        weapon.discharges[4].enabled = false;
                        b.GetComponent<Laser>().damage = weapon.lazers3[lazer3Upgrade].gunLazers;
                    }
                    if (enemys.Length == 2)
                    {
                        b = Instantiate(weapon.lazer3, enemys[0].transform.position, Quaternion.identity) as GameObject;
                        b = Instantiate(weapon.lazer3, enemys[1].transform.position, Quaternion.identity) as GameObject;
                        weapon.discharges[0].enabled = true;
                        weapon.discharges[1].enabled = true;
                        weapon.discharges[0].SetPosition(1, enemys[0].transform.position - transform.position);
                        weapon.discharges[1].SetPosition(1, enemys[1].transform.position - transform.position);
                        weapon.dischargesPS[0].transform.position = enemys[0].transform.position;
                        weapon.dischargesPS[1].transform.position = enemys[1].transform.position;
                        weapon.dischargesPS[0].Play();
                        weapon.dischargesPS[1].Play();
                        weapon.dischargesPS[2].Stop();
                        weapon.dischargesPS[3].Stop();
                        weapon.dischargesPS[4].Stop();
                        weapon.discharges[2].enabled = false;
                        weapon.discharges[3].enabled = false;
                        weapon.discharges[4].enabled = false;
                        b.GetComponent<Laser>().damage = weapon.lazers3[lazer3Upgrade].gunLazers;
                    }
                    if (enemys.Length >= 3)
                    {
                        b = Instantiate(weapon.lazer3, enemys[0].transform.position, Quaternion.identity) as GameObject;
                        b = Instantiate(weapon.lazer3, enemys[1].transform.position, Quaternion.identity) as GameObject;
                        b = Instantiate(weapon.lazer3, enemys[2].transform.position, Quaternion.identity) as GameObject;
                        weapon.discharges[0].enabled = true;
                        weapon.discharges[1].enabled = true;
                        weapon.discharges[2].enabled = true;
                        weapon.discharges[0].SetPosition(1, enemys[0].transform.position - transform.position);
                        weapon.discharges[1].SetPosition(1, enemys[1].transform.position - transform.position);
                        weapon.discharges[2].SetPosition(1, enemys[2].transform.position - transform.position);
                        weapon.dischargesPS[0].transform.position = enemys[0].transform.position;
                        weapon.dischargesPS[1].transform.position = enemys[1].transform.position;
                        weapon.dischargesPS[2].transform.position = enemys[2].transform.position;
                        weapon.dischargesPS[0].Play();
                        weapon.dischargesPS[1].Play();
                        weapon.dischargesPS[2].Play();
                        weapon.dischargesPS[3].Stop();
                        weapon.dischargesPS[4].Stop();
                        weapon.discharges[3].enabled = false;
                        weapon.discharges[4].enabled = false;
                        b.GetComponent<Laser>().damage = weapon.lazers3[lazer3Upgrade].gunLazers;
                    }
                }
                else if (lazer3Upgrade == 3)
                {
                    if (enemys.Length == 1)
                    {
                        b = Instantiate(weapon.lazer3, enemys[0].transform.position, Quaternion.identity) as GameObject;
                        weapon.discharges[0].enabled = true;
                        weapon.discharges[0].SetPosition(1, enemys[0].transform.position - transform.position);
                        weapon.dischargesPS[0].transform.position = enemys[0].transform.position;
                        weapon.dischargesPS[0].Play();
                        weapon.dischargesPS[1].Stop();
                        weapon.dischargesPS[2].Stop();
                        weapon.dischargesPS[3].Stop();
                        weapon.dischargesPS[4].Stop();
                        weapon.discharges[1].enabled = false;
                        weapon.discharges[2].enabled = false;
                        weapon.discharges[3].enabled = false;
                        weapon.discharges[4].enabled = false;
                        b.GetComponent<Laser>().damage = weapon.lazers3[lazer3Upgrade].gunLazers;
                    }
                    if (enemys.Length == 2)
                    {
                        b = Instantiate(weapon.lazer3, enemys[0].transform.position, Quaternion.identity) as GameObject;
                        b = Instantiate(weapon.lazer3, enemys[1].transform.position, Quaternion.identity) as GameObject;
                        weapon.discharges[0].enabled = true;
                        weapon.discharges[1].enabled = true;
                        weapon.discharges[0].SetPosition(1, enemys[0].transform.position - transform.position);
                        weapon.discharges[1].SetPosition(1, enemys[1].transform.position - transform.position);
                        weapon.dischargesPS[0].transform.position = enemys[0].transform.position;
                        weapon.dischargesPS[1].transform.position = enemys[1].transform.position;
                        weapon.dischargesPS[0].Play();
                        weapon.dischargesPS[1].Play();
                        weapon.dischargesPS[2].Stop();
                        weapon.dischargesPS[3].Stop();
                        weapon.dischargesPS[4].Stop();
                        weapon.discharges[2].enabled = false;
                        weapon.discharges[3].enabled = false;
                        weapon.discharges[4].enabled = false;
                        b.GetComponent<Laser>().damage = weapon.lazers3[lazer3Upgrade].gunLazers;
                    }
                    if (enemys.Length == 3)
                    {
                        b = Instantiate(weapon.lazer3, enemys[0].transform.position, Quaternion.identity) as GameObject;
                        b = Instantiate(weapon.lazer3, enemys[1].transform.position, Quaternion.identity) as GameObject;
                        b = Instantiate(weapon.lazer3, enemys[2].transform.position, Quaternion.identity) as GameObject;
                        weapon.discharges[0].enabled = true;
                        weapon.discharges[1].enabled = true;
                        weapon.discharges[2].enabled = true;
                        weapon.discharges[0].SetPosition(1, enemys[0].transform.position - transform.position);
                        weapon.discharges[1].SetPosition(1, enemys[1].transform.position - transform.position);
                        weapon.discharges[2].SetPosition(1, enemys[2].transform.position - transform.position);
                        weapon.dischargesPS[0].transform.position = enemys[0].transform.position;
                        weapon.dischargesPS[1].transform.position = enemys[1].transform.position;
                        weapon.dischargesPS[2].transform.position = enemys[2].transform.position;
                        weapon.dischargesPS[0].Play();
                        weapon.dischargesPS[1].Play();
                        weapon.dischargesPS[2].Play();
                        weapon.dischargesPS[3].Stop();
                        weapon.dischargesPS[4].Stop();
                        weapon.discharges[3].enabled = false;
                        weapon.discharges[4].enabled = false;
                        b.GetComponent<Laser>().damage = weapon.lazers3[lazer3Upgrade].gunLazers;
                    }
                    if (enemys.Length >= 4)
                    {
                        b = Instantiate(weapon.lazer3, enemys[0].transform.position, Quaternion.identity) as GameObject;
                        b = Instantiate(weapon.lazer3, enemys[1].transform.position, Quaternion.identity) as GameObject;
                        b = Instantiate(weapon.lazer3, enemys[2].transform.position, Quaternion.identity) as GameObject;
                        b = Instantiate(weapon.lazer3, enemys[3].transform.position, Quaternion.identity) as GameObject;
                        weapon.discharges[0].enabled = true;
                        weapon.discharges[1].enabled = true;
                        weapon.discharges[2].enabled = true;
                        weapon.discharges[3].enabled = true;
                        weapon.discharges[0].SetPosition(1, enemys[0].transform.position - transform.position);
                        weapon.discharges[1].SetPosition(1, enemys[1].transform.position - transform.position);
                        weapon.discharges[2].SetPosition(1, enemys[2].transform.position - transform.position);
                        weapon.discharges[3].SetPosition(1, enemys[3].transform.position - transform.position);
                        weapon.dischargesPS[0].transform.position = enemys[0].transform.position;
                        weapon.dischargesPS[1].transform.position = enemys[1].transform.position;
                        weapon.dischargesPS[2].transform.position = enemys[2].transform.position;
                        weapon.dischargesPS[3].transform.position = enemys[3].transform.position;
                        weapon.dischargesPS[0].Play();
                        weapon.dischargesPS[1].Play();
                        weapon.dischargesPS[2].Play();
                        weapon.dischargesPS[3].Play();
                        weapon.dischargesPS[4].Stop();
                        weapon.discharges[4].enabled = false;
                        b.GetComponent<Laser>().damage = weapon.lazers3[lazer3Upgrade].gunLazers;
                    }
                }
                else if (lazer3Upgrade == 4)
                {
                    if (enemys.Length == 1)
                    {
                        b = Instantiate(weapon.lazer3, enemys[0].transform.position, Quaternion.identity) as GameObject;
                        weapon.discharges[0].enabled = true;
                        weapon.discharges[0].SetPosition(1, enemys[0].transform.position - transform.position);
                        weapon.dischargesPS[0].transform.position = enemys[0].transform.position;
                        weapon.dischargesPS[0].Play();
                        weapon.dischargesPS[1].Stop();
                        weapon.dischargesPS[2].Stop();
                        weapon.dischargesPS[3].Stop();
                        weapon.dischargesPS[4].Stop();
                        weapon.discharges[1].enabled = false;
                        weapon.discharges[2].enabled = false;
                        weapon.discharges[3].enabled = false;
                        weapon.discharges[4].enabled = false;
                        b.GetComponent<Laser>().damage = weapon.lazers3[lazer3Upgrade].gunLazers;
                    }
                    if (enemys.Length == 2)
                    {
                        b = Instantiate(weapon.lazer3, enemys[0].transform.position, Quaternion.identity) as GameObject;
                        b = Instantiate(weapon.lazer3, enemys[1].transform.position, Quaternion.identity) as GameObject;
                        weapon.discharges[0].enabled = true;
                        weapon.discharges[1].enabled = true;
                        weapon.discharges[0].SetPosition(1, enemys[0].transform.position - transform.position);
                        weapon.discharges[1].SetPosition(1, enemys[1].transform.position - transform.position);
                        weapon.dischargesPS[0].transform.position = enemys[0].transform.position;
                        weapon.dischargesPS[1].transform.position = enemys[1].transform.position;
                        weapon.dischargesPS[0].Play();
                        weapon.dischargesPS[1].Play();
                        weapon.dischargesPS[2].Stop();
                        weapon.dischargesPS[3].Stop();
                        weapon.dischargesPS[4].Stop();
                        weapon.discharges[2].enabled = false;
                        weapon.discharges[3].enabled = false;
                        weapon.discharges[4].enabled = false;
                        b.GetComponent<Laser>().damage = weapon.lazers3[lazer3Upgrade].gunLazers;
                    }
                    if (enemys.Length == 3)
                    {
                        b = Instantiate(weapon.lazer3, enemys[0].transform.position, Quaternion.identity) as GameObject;
                        b = Instantiate(weapon.lazer3, enemys[1].transform.position, Quaternion.identity) as GameObject;
                        b = Instantiate(weapon.lazer3, enemys[2].transform.position, Quaternion.identity) as GameObject;
                        weapon.discharges[0].enabled = true;
                        weapon.discharges[1].enabled = true;
                        weapon.discharges[2].enabled = true;
                        weapon.discharges[0].SetPosition(1, enemys[0].transform.position - transform.position);
                        weapon.discharges[1].SetPosition(1, enemys[1].transform.position - transform.position);
                        weapon.discharges[2].SetPosition(1, enemys[2].transform.position - transform.position);
                        weapon.dischargesPS[0].transform.position = enemys[0].transform.position;
                        weapon.dischargesPS[1].transform.position = enemys[1].transform.position;
                        weapon.dischargesPS[2].transform.position = enemys[2].transform.position;
                        weapon.dischargesPS[0].Play();
                        weapon.dischargesPS[1].Play();
                        weapon.dischargesPS[2].Play();
                        weapon.dischargesPS[3].Stop();
                        weapon.dischargesPS[4].Stop();
                        weapon.discharges[3].enabled = false;
                        weapon.discharges[4].enabled = false;
                        b.GetComponent<Laser>().damage = weapon.lazers3[lazer3Upgrade].gunLazers;
                    }
                    if (enemys.Length == 4)
                    {
                        b = Instantiate(weapon.lazer3, enemys[0].transform.position, Quaternion.identity) as GameObject;
                        b = Instantiate(weapon.lazer3, enemys[1].transform.position, Quaternion.identity) as GameObject;
                        b = Instantiate(weapon.lazer3, enemys[2].transform.position, Quaternion.identity) as GameObject;
                        b = Instantiate(weapon.lazer3, enemys[3].transform.position, Quaternion.identity) as GameObject;
                        weapon.discharges[0].enabled = true;
                        weapon.discharges[1].enabled = true;
                        weapon.discharges[2].enabled = true;
                        weapon.discharges[3].enabled = true;
                        weapon.discharges[0].SetPosition(1, enemys[0].transform.position - transform.position);
                        weapon.discharges[1].SetPosition(1, enemys[1].transform.position - transform.position);
                        weapon.discharges[2].SetPosition(1, enemys[2].transform.position - transform.position);
                        weapon.discharges[3].SetPosition(1, enemys[3].transform.position - transform.position);
                        weapon.dischargesPS[0].transform.position = enemys[0].transform.position;
                        weapon.dischargesPS[1].transform.position = enemys[1].transform.position;
                        weapon.dischargesPS[2].transform.position = enemys[2].transform.position;
                        weapon.dischargesPS[3].transform.position = enemys[3].transform.position;
                        weapon.dischargesPS[0].Play();
                        weapon.dischargesPS[1].Play();
                        weapon.dischargesPS[2].Play();
                        weapon.dischargesPS[3].Play();
                        weapon.dischargesPS[4].Stop();
                        weapon.discharges[4].enabled = false;
                        b.GetComponent<Laser>().damage = weapon.lazers3[lazer3Upgrade].gunLazers;
                    }
                    if (enemys.Length >= 5)
                    {
                        b = Instantiate(weapon.lazer3, enemys[0].transform.position, Quaternion.identity) as GameObject;
                        b = Instantiate(weapon.lazer3, enemys[1].transform.position, Quaternion.identity) as GameObject;
                        b = Instantiate(weapon.lazer3, enemys[2].transform.position, Quaternion.identity) as GameObject;
                        b = Instantiate(weapon.lazer3, enemys[3].transform.position, Quaternion.identity) as GameObject;
                        b = Instantiate(weapon.lazer3, enemys[4].transform.position, Quaternion.identity) as GameObject;
                        weapon.discharges[0].enabled = true;
                        weapon.discharges[1].enabled = true;
                        weapon.discharges[2].enabled = true;
                        weapon.discharges[3].enabled = true;
                        weapon.discharges[4].enabled = true;
                        weapon.discharges[0].SetPosition(1, enemys[0].transform.position - transform.position);
                        weapon.discharges[1].SetPosition(1, enemys[1].transform.position - transform.position);
                        weapon.discharges[2].SetPosition(1, enemys[2].transform.position - transform.position);
                        weapon.discharges[3].SetPosition(1, enemys[3].transform.position - transform.position);
                        weapon.discharges[4].SetPosition(1, enemys[4].transform.position - transform.position);
                        weapon.dischargesPS[0].transform.position = enemys[0].transform.position;
                        weapon.dischargesPS[1].transform.position = enemys[1].transform.position;
                        weapon.dischargesPS[2].transform.position = enemys[2].transform.position;
                        weapon.dischargesPS[3].transform.position = enemys[3].transform.position;
                        weapon.dischargesPS[4].transform.position = enemys[4].transform.position;
                        weapon.dischargesPS[0].Play();
                        weapon.dischargesPS[1].Play();
                        weapon.dischargesPS[2].Play();
                        weapon.dischargesPS[3].Play();
                        weapon.dischargesPS[4].Play();
                        b.GetComponent<Laser>().damage = weapon.lazers3[lazer3Upgrade].gunLazers;
                    }
                }
            }
        }
        else if (lazers == Destroyer.Lazer.Lazer4 && Time.time > nextFire && energy >= weapon.lazers4[lazer4Upgrade].gunLazers)
        {
            soundClip.PlaySound("destroyer_lazer5");
            nextFire = Time.time + 0.6f;
            energy -= weapon.lazers4[lazer4Upgrade].gunLazers;

            for (int i = 0; i < 5; i++)
            {
                weapon.dischargesPS[i].Stop();
                weapon.discharges[i].enabled = false;
            }
            for (int i = 0; i < weapon.lazers4[lazer4Upgrade].lazerSpawn.Length; i++)
            {
                b = Instantiate(weapon.lazer4, weapon.lazers4[lazer4Upgrade].lazerSpawn[i]) as GameObject;
                b.GetComponent<Laser>().damage = weapon.lazers4[lazer4Upgrade].gunLazers;
            }
        }
        else if (lazers == Destroyer.Lazer.Lazer5 && Time.time > nextFire && energy >= weapon.lazers5[lazer5Upgrade].gunLazers)
        {
            soundClip.PlaySound("destroyer_lazer2");
            nextFire = Time.time + 0.15f;
            energy -= weapon.lazers5[lazer5Upgrade].gunLazers;

            if (weapon.lazerBurst3 != null)
            {
                Instantiate(weapon.lazerBurst3, weapon.brustSpawn);
            }

            for (int i = 0; i < 5; i++)
            {
                weapon.dischargesPS[i].Stop();
                weapon.discharges[i].enabled = false;
            }
            for (int i = 0; i < weapon.lazers5[lazer5Upgrade].lazerSpawn.Length; i++)
            {
                b = Instantiate(weapon.lazer5) as GameObject;
                b.transform.position = weapon.lazers5[lazer5Upgrade].lazerSpawn[i].position;
                b.transform.rotation = weapon.lazers5[lazer5Upgrade].lazerSpawn[i].rotation;
                b.GetComponent<Laser>().damage = weapon.lazers5[lazer5Upgrade].gunLazers;
            }
        }
    }

    public void FireRocket()
    {
        if (weapon.rockets[(int)rockets].isWTFBoom && gameManager.isMemeSounds)
        {
            soundClip.PlaySound("wtfstart");
            soundClip.PlaySound("fire-in-the-hole");
        }
        soundClip.PlaySound(weapon.rockets[(int)rockets].playRocket);

        for (int i = 0; i < weapon.rockets[(int)rockets].rocketSpawn.Length; i++)
        {
            Instantiate(weapon.rockets[(int)rockets].rocket, weapon.rockets[(int)rockets].rocketSpawn[i].position, weapon.rockets[(int)rockets].rocketSpawn[i].rotation);
        }
    }

    public void Damage(float damage)
    {
        health -= damage;
    }

    public void RestoreHealth()
    {
        health = maxHealth[healthUpgrade];
    }

    public void RestoreEnergy()
    {
        energy = maxEnergy[energyUpgrade];
    }
}
