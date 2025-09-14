using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Destroyer : MonoBehaviour
{
    public enum Lazer { Lazer1, Lazer2, Lazer3, Lazer4, Lazer5 }
    public Lazer lazers;
    public enum Rocket { Rocket1, Rocket2, Rocket3, Rocket4, Rocket5 }
    public Rocket rockets;
    public float speed;
    public float tilt;
    public Vector2 axis;
    public GameObject destroyerParts;
    public Transform destroyerMover;
    public GameObject explore;
    public GameObject cabin;
    public GameObject enginel;
    public GameObject enginer;
    public GameObject rocketl;
    public GameObject rocketr;
    public GameObject wingl;
    public GameObject wingr;
    public GameObject destroyerImmortal;
    public GameObject immortalSphere;

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
    [HideInInspector]
    public bool isImmortal;

    private float health;
    private float energy;
    private float immortalTime = 3.0f;
    private Game game;
    private GameManager gameManager;
    private SoundClip soundClip;
    private Rigidbody rb;
    private float nextFireLazer;
    private float nextFireRocket;
    private bool isFireLazer;
    private bool isFireRocket;
    private bool isAlert;
    private GameObject[] enemys;

    void Awake()
    {
        game = GameObject.FindObjectOfType<Game>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        soundClip = GameObject.FindObjectOfType<SoundClip>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
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
        if (enemys.Length == 1)
        {
            weapon.discharges[0].SetPosition(1, enemys[0].transform.position - transform.position);
            weapon.dischargesPS[0].transform.position = enemys[0].transform.position;
        }
        if (enemys.Length == 2)
        {
            weapon.discharges[0].SetPosition(1, enemys[0].transform.position - transform.position);
            weapon.discharges[1].SetPosition(1, enemys[1].transform.position - transform.position);
            weapon.dischargesPS[0].transform.position = enemys[0].transform.position;
            weapon.dischargesPS[1].transform.position = enemys[1].transform.position;
        }
        if (enemys.Length == 3)
        {
            weapon.discharges[0].SetPosition(1, enemys[0].transform.position - transform.position);
            weapon.discharges[1].SetPosition(1, enemys[1].transform.position - transform.position);
            weapon.discharges[2].SetPosition(1, enemys[2].transform.position - transform.position);
            weapon.dischargesPS[0].transform.position = enemys[0].transform.position;
            weapon.dischargesPS[1].transform.position = enemys[1].transform.position;
            weapon.dischargesPS[2].transform.position = enemys[2].transform.position;
        }
        if (enemys.Length == 4)
        {
            weapon.discharges[0].SetPosition(1, enemys[0].transform.position - transform.position);
            weapon.discharges[1].SetPosition(1, enemys[1].transform.position - transform.position);
            weapon.discharges[2].SetPosition(1, enemys[2].transform.position - transform.position);
            weapon.discharges[3].SetPosition(1, enemys[3].transform.position - transform.position);
            weapon.dischargesPS[0].transform.position = enemys[0].transform.position;
            weapon.dischargesPS[1].transform.position = enemys[1].transform.position;
            weapon.dischargesPS[2].transform.position = enemys[2].transform.position;
            weapon.dischargesPS[3].transform.position = enemys[3].transform.position;
        }
        if (enemys.Length >= 5)
        {
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
        }
        game.healthBar.value = health;
        game.healthBar.maxValue = maxHealth[healthUpgrade];
        game.energyBar.value = energy;
        game.energyBar.maxValue = maxEnergy[energyUpgrade];
        if (health <= maxHealth[healthUpgrade] * 0.25f && !isAlert && !game.shop)
        {
            soundClip.healthAlert.Play();
            soundClip.healthAlert.loop = true;
            isAlert = true;
        }
        if (health > maxHealth[healthUpgrade] * 0.25f && isAlert && !game.shop)
        {
            soundClip.healthAlert.loop = false;
            isAlert = false;
        }
        if (health <= 0)
        {
            Instantiate(explore, transform.position, Quaternion.identity);
            soundClip.PlayClip("random wow/doh.swf");
            soundClip.PlayExplosion();
            Instantiate(destroyerParts, destroyerMover.position, Quaternion.identity);
            game.DeadLives();
            if (game.life <= 0)
            {
                soundClip.healthAlert.loop = false;
                isAlert = false;
                gameObject.SetActive(false);
            }
            RestoreHealth();
            RestoreEnergy();
            rb.position = new Vector3(0.0f, 0.0f, -17);
            immortalTime = 3;
        }
        if (energy < maxEnergy[energyUpgrade])
        {
            energy += Time.deltaTime * 500;
        }
        else if (energy > maxEnergy[energyUpgrade])
        {
            energy = maxEnergy[energyUpgrade];
        }
        if (Input.GetButtonDown("Fire1") && !game.shop)
        {
            isFireLazer = true;
            if (lazers == Destroyer.Lazer.Lazer3 && energy >= weapon.lazers3[lazer3Upgrade].gunLazers)
            {
                weapon.dischargeLight.enabled = true;
                soundClip.PlaySound("destroyer_lazer4");
            }
        }
        else if (Input.GetButtonUp("Fire1") && !game.shop)
        {
            nextFireLazer = Time.time;
            isFireLazer = false;
            weapon.dischargeLight.enabled = false;
            for (int i = 0; i < 5; i++)
            {
                weapon.dischargesPS[i].Stop();
                weapon.discharges[i].enabled = false;
            }
        }
        if (Input.GetButtonDown("Fire2") && !game.shop)
        {
            isFireRocket = true;
        }
        else if (Input.GetButtonUp("Fire2") && !game.shop)
        {
            nextFireRocket = Time.time;
            isFireRocket = false;
        }
        if (isFireLazer && Time.time > nextFireLazer)
        {
            FireLazer();
        }
        if (isFireRocket && Time.time > nextFireRocket)
        {
            FireRocket();
        }
        if (Input.GetKeyDown(KeyCode.X) && !game.shop)
        {
            soundClip.PlaySound("rocketswitch");
            rockets++;
            if (rockets > Destroyer.Rocket.Rocket5)
            {
                rockets = Rocket.Rocket1;
            }
        }
        if (Input.GetKeyDown(KeyCode.Z) && !game.shop)
        {
            soundClip.PlaySound("laserswitch");
            lazers++;
            if (lazers == Destroyer.Lazer.Lazer3 && isFireLazer)
            {
                weapon.dischargeLight.enabled = true;
                soundClip.PlaySound("destroyer_lazer4");
            }
            if (lazers > Destroyer.Lazer.Lazer5)
            {
                lazers = Lazer.Lazer1;
            }
        }
        if (immortalTime <= 0)
        {
            isImmortal = false;
            cabin.SetActive(true);
            enginel.SetActive(true);
            enginer.SetActive(true);
            rocketl.SetActive(true);
            rocketr.SetActive(true);
            wingl.SetActive(true);
            wingr.SetActive(true);
            destroyerImmortal.SetActive(false);
            immortalSphere.SetActive(false);
            destroyerImmortal.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
        }
        else
        {
            immortalTime -= Time.deltaTime;
            isImmortal = true;
            cabin.SetActive(false);
            enginel.SetActive(false);
            enginer.SetActive(false);
            rocketl.SetActive(false);
            rocketr.SetActive(false);
            wingl.SetActive(false);
            wingr.SetActive(false);
            destroyerImmortal.SetActive(true);
            destroyerImmortal.GetComponent<Renderer>().material.color = new Color(1, 1, 1, Mathf.PingPong(Time.time * 3, 1));
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (!game.shop)
        {
            rb.velocity = movement * speed * Time.fixedDeltaTime;
        }

        rb.position = new Vector3(
                Mathf.Clamp(rb.position.x, -axis.x - rb.position.z / 5, axis.x + rb.position.z / 5),
                0.0f,
                Mathf.Clamp(rb.position.z, -axis.y, axis.y)
        );

        destroyerMover.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }

    public void FireLazer()
    {
        GameObject b;

        if (lazers == Destroyer.Lazer.Lazer1 && Time.time > nextFireLazer && energy >= weapon.lazers1[lazer1Upgrade].gunLazers)
        {
            soundClip.PlaySound("destroyer_lazer1");
            nextFireLazer = Time.time + 0.15f;
            energy -= weapon.lazers1[lazer1Upgrade].gunLazers;

            if (weapon.lazerBurst1 != null)
            {
                Instantiate(weapon.lazerBurst1, weapon.brustSpawn);
            }

            weapon.dischargeLight.enabled = false;
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
        else if (lazers == Destroyer.Lazer.Lazer2 && Time.time > nextFireLazer && energy >= weapon.lazers2[lazer2Upgrade].gunLazers)
        {
            soundClip.PlaySound("destroyer_lazer3");
            nextFireLazer = Time.time + 0.15f;
            energy -= weapon.lazers2[lazer2Upgrade].gunLazers;

            if (weapon.lazerBurst2 != null)
            {
                Instantiate(weapon.lazerBurst2, weapon.brustSpawn);
            }

            weapon.dischargeLight.enabled = false;
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
        else if (lazers == Destroyer.Lazer.Lazer3 && Time.time > nextFireLazer && energy < weapon.lazers3[lazer3Upgrade].gunLazers * 0.1f)
        {
            weapon.dischargeLight.enabled = false;
            for (int i = 0; i < 5; i++)
            {
                weapon.dischargesPS[i].Stop();
                weapon.discharges[i].enabled = false;
            }
        }
        else if (lazers == Destroyer.Lazer.Lazer3 && Time.time > nextFireLazer && energy >= weapon.lazers3[lazer3Upgrade].gunLazers * 0.1f)
        {
            weapon.dischargeLight.enabled = true;
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
                nextFireLazer = Time.time + 0.05f;
                energy -= weapon.lazers3[lazer3Upgrade].gunLazers * 0.1f;
                if (lazer3Upgrade == 0)
                {
                    if (enemys.Length >= 1)
                    {
                        b = Instantiate(weapon.lazer3, enemys[0].transform.position, Quaternion.identity) as GameObject;
                        weapon.discharges[0].enabled = true;
                        if (!weapon.dischargesPS[0].isPlaying)
                        {
                            weapon.dischargesPS[0].Play();
                        }
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
                        if (!weapon.dischargesPS[0].isPlaying)
                        {
                            weapon.dischargesPS[0].Play();
                        }
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
                        if (!weapon.dischargesPS[0].isPlaying)
                        {
                            weapon.dischargesPS[0].Play();
                        }
                        if (!weapon.dischargesPS[1].isPlaying)
                        {
                            weapon.dischargesPS[1].Play();
                        }
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
                        if (!weapon.dischargesPS[0].isPlaying)
                        {
                            weapon.dischargesPS[0].Play();
                        }
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
                        if (!weapon.dischargesPS[0].isPlaying)
                        {
                            weapon.dischargesPS[0].Play();
                        }
                        if (!weapon.dischargesPS[1].isPlaying)
                        {
                            weapon.dischargesPS[1].Play();
                        }
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
                        if (!weapon.dischargesPS[0].isPlaying)
                        {
                            weapon.dischargesPS[0].Play();
                        }
                        if (!weapon.dischargesPS[1].isPlaying)
                        {
                            weapon.dischargesPS[1].Play();
                        }
                        if (!weapon.dischargesPS[2].isPlaying)
                        {
                            weapon.dischargesPS[2].Play();
                        }
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
                        if (!weapon.dischargesPS[0].isPlaying)
                        {
                            weapon.dischargesPS[0].Play();
                        }
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
                        if (!weapon.dischargesPS[0].isPlaying)
                        {
                            weapon.dischargesPS[0].Play();
                        }
                        if (!weapon.dischargesPS[1].isPlaying)
                        {
                            weapon.dischargesPS[1].Play();
                        }
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
                        if (!weapon.dischargesPS[0].isPlaying)
                        {
                            weapon.dischargesPS[0].Play();
                        }
                        if (!weapon.dischargesPS[1].isPlaying)
                        {
                            weapon.dischargesPS[1].Play();
                        }
                        if (!weapon.dischargesPS[2].isPlaying)
                        {
                            weapon.dischargesPS[2].Play();
                        }
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
                        if (!weapon.dischargesPS[0].isPlaying)
                        {
                            weapon.dischargesPS[0].Play();
                        }
                        if (!weapon.dischargesPS[1].isPlaying)
                        {
                            weapon.dischargesPS[1].Play();
                        }
                        if (!weapon.dischargesPS[2].isPlaying)
                        {
                            weapon.dischargesPS[2].Play();
                        }
                        if (!weapon.dischargesPS[3].isPlaying)
                        {
                            weapon.dischargesPS[3].Play();
                        }
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
                        if (!weapon.dischargesPS[0].isPlaying)
                        {
                            weapon.dischargesPS[0].Play();
                        }
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
                        if (!weapon.dischargesPS[0].isPlaying)
                        {
                            weapon.dischargesPS[0].Play();
                        }
                        if (!weapon.dischargesPS[1].isPlaying)
                        {
                            weapon.dischargesPS[1].Play();
                        }
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
                        if (!weapon.dischargesPS[0].isPlaying)
                        {
                            weapon.dischargesPS[0].Play();
                        }
                        if (!weapon.dischargesPS[1].isPlaying)
                        {
                            weapon.dischargesPS[1].Play();
                        }
                        if (!weapon.dischargesPS[2].isPlaying)
                        {
                            weapon.dischargesPS[2].Play();
                        }
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
                        if (!weapon.dischargesPS[0].isPlaying)
                        {
                            weapon.dischargesPS[0].Play();
                        }
                        if (!weapon.dischargesPS[1].isPlaying)
                        {
                            weapon.dischargesPS[1].Play();
                        }
                        if (!weapon.dischargesPS[2].isPlaying)
                        {
                            weapon.dischargesPS[2].Play();
                        }
                        if (!weapon.dischargesPS[3].isPlaying)
                        {
                            weapon.dischargesPS[3].Play();
                        }
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
                        if (!weapon.dischargesPS[0].isPlaying)
                        {
                            weapon.dischargesPS[0].Play();
                        }
                        if (!weapon.dischargesPS[1].isPlaying)
                        {
                            weapon.dischargesPS[1].Play();
                        }
                        if (!weapon.dischargesPS[2].isPlaying)
                        {
                            weapon.dischargesPS[2].Play();
                        }
                        if (!weapon.dischargesPS[3].isPlaying)
                        {
                            weapon.dischargesPS[3].Play();
                        }
                        if (!weapon.dischargesPS[4].isPlaying)
                        {
                            weapon.dischargesPS[4].Play();
                        }
                        b.GetComponent<Laser>().damage = weapon.lazers3[lazer3Upgrade].gunLazers;
                    }
                }
            }
        }
        else if (lazers == Destroyer.Lazer.Lazer4 && Time.time > nextFireLazer && energy >= weapon.lazers4[lazer4Upgrade].gunLazers)
        {
            soundClip.PlaySound("destroyer_lazer5");
            nextFireLazer = Time.time + 0.6f;
            energy -= weapon.lazers4[lazer4Upgrade].gunLazers;

            weapon.dischargeLight.enabled = false;
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
        else if (lazers == Destroyer.Lazer.Lazer5 && Time.time > nextFireLazer && energy >= weapon.lazers5[lazer5Upgrade].gunLazers)
        {
            soundClip.PlaySound("destroyer_lazer2");
            nextFireLazer = Time.time + 0.15f;
            energy -= weapon.lazers5[lazer5Upgrade].gunLazers;

            if (weapon.lazerBurst3 != null)
            {
                Instantiate(weapon.lazerBurst3, weapon.brustSpawn);
            }

            weapon.dischargeLight.enabled = false;
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
        nextFireRocket = Time.time + 2f;
        if (weapon.rockets[(int)rockets].isWTFBoom && gameManager.isMemeSounds)
        {
            soundClip.PlayClip("fire-in-the-hole");
            soundClip.PlayClip("wtfstart");
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

    public void Immortal()
    {
        immortalTime = 90;
        immortalSphere.SetActive(true);
    }

    public void LazerModificator()
    {
        if (lazers == Destroyer.Lazer.Lazer1 && lazer1Upgrade < 4)
        {
            lazer1Upgrade++;
        }
        else if (lazers == Destroyer.Lazer.Lazer2 && lazer2Upgrade < 4)
        {
            lazer2Upgrade++;
        }
        else if (lazers == Destroyer.Lazer.Lazer3 && lazer3Upgrade < 4)
        {
            lazer3Upgrade++;
        }
        else if (lazers == Destroyer.Lazer.Lazer4 && lazer4Upgrade < 4)
        {
            lazer4Upgrade++;
        }
        else if (lazers == Destroyer.Lazer.Lazer5 && lazer5Upgrade < 4)
        {
            lazer5Upgrade++;
        }
    }

    public void AddStrait()
    {
        rocket1Count += 10;
    }

    public void AddSmart()
    {
        rocket2Count += 5;
    }

    public void AddMedusa()
    {
        rocket3Count += 3;
    }

    public void AddSwarm()
    {
        rocket4Count += 3;
    }

    public void AddNuke()
    {
        rocket5Count += 1;
    }
}
