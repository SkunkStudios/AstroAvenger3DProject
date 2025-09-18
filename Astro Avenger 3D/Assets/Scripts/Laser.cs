using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public enum TypeSound { LazerHit, RocetExplore }
    public TypeSound typeSounds;
    public enum Type { Off, Destroyer, Enemy, Both }
    public Type types;
    public GameObject explore;
    public GameObject missile;
    public Transform[] missileSpawn;
    public int damage;
    [HideInInspector]
    public int damageHit;
    public int maxHit;
    public MeshRenderer laserObject;
    public MeshRenderer fireTail;
    public ParticleSystem laserPS;
    public TrailRenderer laserTR;
    public float exploreTime;
    public string playName;
    public bool isBoom;
    public bool isCanHit;
    public float camHit;

    private GameManager gameManager;
    private CameraHit cameraHit;
    private SoundClip soundClip;
    private bool isHit = true;
    private bool isHitBoth = false;

    void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        cameraHit = GameObject.FindObjectOfType<CameraHit>();
        soundClip = GameObject.FindObjectOfType<SoundClip>();
    }

    void Start ()
	{
        if (types == Type.Destroyer || types == Type.Enemy)
        {
            isHitBoth = true;
        }
        StartCoroutine(WaitHit());
    }

    void Update ()
	{
		if (transform.position.z >= 24 && explore != null || transform.position.z <= -24 && explore != null || transform.position.x >= 40 && explore != null || transform.position.x <= -40 && explore != null)
		{
            damageHit = maxHit;
            if (laserObject != null)
            {
                laserObject.enabled = false;
            }
            if (fireTail != null)
            {
                fireTail.enabled = false;
            }
            if (laserPS != null)
            {
                laserPS.Stop();
            }
            if (laserTR != null)
            {
                Destroy(gameObject, 0.64f);
            }
            else
            {
                Destroy(gameObject);
            }
            isHit = false;
        }
        if (damageHit >= maxHit)
        {
            GetComponent<Collider>().enabled = false;
        }
    }

    void Hit()
    {
        cameraHit.Hit(camHit);
        if (missile != null && missileSpawn.Length == 0)
        {
            Instantiate(missile, transform.position, Quaternion.identity);
        }
        else if (missile != null && missileSpawn.Length >= 1)
        {
            for (int i = 0; i < missileSpawn.Length; i++)
            {
                Instantiate(missile, missileSpawn[i].position, missileSpawn[i].rotation);
            }
        }
        if (isBoom)
        {
            if (gameManager.isMemeSounds)
            {
                soundClip.PlayClip("wtfboom");
            }
            soundClip.PlayExplosion();
        }
        else
        {
            if (gameManager.isMemeSounds)
            {
                soundClip.PlaySound("HITMARKER");
            }
            if (typeSounds == TypeSound.LazerHit)
            {
                soundClip.PlaySound("lazer_hit");
            }
            else if (typeSounds == TypeSound.RocetExplore)
            {
                soundClip.PlaySound("explosion_rocket");
            }
        }
        if (explore != null)
        {
            if (maxHit > 1)
            {
                transform.rotation = Quaternion.Euler(0, -transform.eulerAngles.y, 0);
            }
            Instantiate(explore, transform.position, Quaternion.identity);
            damageHit++;
        }
        if (damageHit >= maxHit || transform.rotation.y == 0)
        {
            if (laserObject != null)
            {
                laserObject.enabled = false;
            }
            if (fireTail != null)
            {
                fireTail.enabled = false;
            }
            if (laserPS != null)
            {
                laserPS.Stop();
            }
            if (laserTR != null)
            {
                Destroy(gameObject, 0.64f);
            }
            else
            {
                Destroy(gameObject);
            }
            isHit = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyCollider") && types == Type.Destroyer || other.CompareTag("Meteorit") && types == Type.Destroyer || other.CompareTag("AsteroidCollider") && types == Type.Destroyer || other.CompareTag("LightningCollider") && types == Type.Destroyer || other.CompareTag("DestroyerCollider") && types == Type.Enemy || other.CompareTag("DestroyerImmortal") && types == Type.Enemy || other.CompareTag("Meteorit") && types == Type.Enemy || other.CompareTag("AsteroidCollider") && types == Type.Enemy || other.CompareTag("LightningCollider") && types == Type.Enemy || other.CompareTag("EnemyCollider") && isHitBoth && types == Type.Both || other.CompareTag("DestroyerCollider") && isHitBoth && types == Type.Both || other.CompareTag("DestroyerImmortal") && isHitBoth && types == Type.Both || other.CompareTag("Meteorit") && isHitBoth && types == Type.Both || other.CompareTag("AsteroidCollider") && isHitBoth && types == Type.Both || other.CompareTag("LightningCollider") && isHitBoth && types == Type.Both)
        {
            Hit();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("EnemyCollider") && other.GetComponent<DamageCollider>() != null && isHitBoth && types == Type.Destroyer || other.CompareTag("Meteorit") && other.GetComponent<DamageCollider>() != null && isHitBoth && types == Type.Destroyer || other.CompareTag("DestroyerCollider") && other.GetComponent<DamageCollider>() != null && isHitBoth && types == Type.Enemy || other.CompareTag("Meteorit") && other.GetComponent<DamageCollider>() != null && isHitBoth && types == Type.Enemy || other.CompareTag("DestroyerCollider") && other.GetComponent<DamageCollider>() != null && isHitBoth && types == Type.Both || other.CompareTag("EnemyCollider") && other.GetComponent<DamageCollider>() != null && isHitBoth && types == Type.Both || other.CompareTag("Meteorit") && other.GetComponent<DamageCollider>() != null && isHitBoth && types == Type.Both)
        {
            other.GetComponent<DamageCollider>().Damage(damage);
            if (!isCanHit)
            {
                isHitBoth = false;
            }
        }
        else if (other.CompareTag("EnemyCollider") && other.GetComponent<EnemyHealth>() != null && isHitBoth && types == Type.Destroyer || other.CompareTag("Meteorit") && other.GetComponent<EnemyHealth>() != null && isHitBoth && types == Type.Destroyer || other.CompareTag("DestroyerCollider") && other.GetComponent<EnemyHealth>() != null && isHitBoth && types == Type.Enemy || other.CompareTag("Meteorit") && other.GetComponent<EnemyHealth>() != null && isHitBoth && types == Type.Enemy || other.CompareTag("DestroyerCollider") && other.GetComponent<EnemyHealth>() != null && isHitBoth && types == Type.Both || other.CompareTag("EnemyCollider") && other.GetComponent<EnemyHealth>() != null && isHitBoth && types == Type.Both || other.CompareTag("Meteorit") && other.GetComponent<EnemyHealth>() != null && isHitBoth && types == Type.Both)
        {
            other.GetComponent<EnemyHealth>().Damage(damage);
            if (!isCanHit)
            {
                isHitBoth = false;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DestroyerCollider") || other.CompareTag("EnemyCollider") || other.CompareTag("EnemyTrigger"))
        {
            isHitBoth = true;
        }
    }

    IEnumerator WaitHit()
    {
        yield return new WaitForSeconds(exploreTime);
        if (isHit)
        {
            Hit();
        }
    }
}
