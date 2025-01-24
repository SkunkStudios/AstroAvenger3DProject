using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Parts
{
    public GameObject part;
    public Transform partSpawns;
}
public class EnemyHealth : MonoBehaviour
{
	public GameObject explore;
    public Vector3 exploreValues;
    public bool isExplore;
    public GameObject bonus;
    public Parts[] parts;
    public float maxHealth;
    public bool isMeteorit;
    public GameObject healthUI;
    public Slider healthBar;
    public AstroLiner astroLiner;
    public float camHit;

    private float health;
    private bool waitExplore;
    private bool isHealthBar;
    private GameManager gameManager;
    private CameraHit cameraHit;
    private SoundClip soundClip;
    private Collider[] colliders;
    private MeshRenderer[] enemyObjects;
    private EnemyPath path;

    void Start ()
	{
        health = maxHealth;
        waitExplore = isExplore;
        gameManager = GameObject.FindObjectOfType<GameManager>();
        cameraHit = GameObject.FindObjectOfType<CameraHit>();
        soundClip = GameObject.FindObjectOfType<SoundClip>();
        path = GetComponent<EnemyPath>();
        colliders = GetComponentsInChildren<Collider>();
        if (transform.position.z > 30)
        {
            foreach (Collider collider in colliders)
            {
                collider.enabled = false;
            }
        }
    }

    void Update ()
	{
        colliders = GetComponentsInChildren<Collider>();
        enemyObjects = GetComponentsInChildren<MeshRenderer>();
        if (transform.position.z < 30 && health > 0)
        {
            foreach (Collider collider in colliders)
            {
                collider.enabled = true;
            }
        }
        if (transform.position.z <= 60 && healthUI != null && !isHealthBar)
        {
            healthUI.SetActive(true);
            isHealthBar = true;
        }
        if (healthBar != null)
        {
            healthBar.value = health;
            healthBar.maxValue = maxHealth;
        }
        if (health <= 0)
        {
            path.DeathPath();
            foreach (Collider collider in colliders)
            {
                collider.enabled = false;
            }
            if (waitExplore)
            {
                if (gameManager.isMemeSounds)
                {
                    gameManager.MemeClipsUI();
                    soundClip.PlayMemes();
                }
                if (healthUI != null)
                {
                    healthUI.SetActive(false);
                }
                if (astroLiner != null)
                {
                    StartCoroutine(astroLiner.TermitExplores());
                }
                StartCoroutine(WaitExplore());
                waitExplore = false;
            }
            else if(!isExplore)
            {
                cameraHit.Hit(camHit);
                if (gameManager.isMemeSounds)
                {
                    gameManager.MemeClipsUI();
                    soundClip.PlayMemes();
                }
                if (isMeteorit)
                {
                    soundClip.PlaySound("meteorit_explode");
                }
                soundClip.PlayExplosion();
                Instantiate(explore, transform.position, Quaternion.identity);
                if (bonus != null)
                {
                    Instantiate(bonus, transform.position, Quaternion.identity);
                }
                ExploreDebris();
                Destroy(gameObject);
            }
        }
    }

    public void Damage(float damage)
    {
        health -= damage;
    }

    void ExploreDebris()
    {
        if (parts.Length >= 1)
        {
            foreach (Parts part in parts)
            {
                if (part != null)
                {
                    Instantiate(part.part, part.partSpawns.position, part.partSpawns.rotation);
                }
            }
        }
    }

    IEnumerator WaitExplore()
    {
        for (int i = 0; i < 250; i++)
        {
            soundClip.PlayExplosion();
            Vector3 explorePoint = new Vector3(Random.Range(-exploreValues.x + transform.position.x, exploreValues.x + transform.position.x), Random.Range(-exploreValues.y + transform.position.y, exploreValues.y + transform.position.y), Random.Range(-exploreValues.z + transform.position.z, exploreValues.z + transform.position.z));
            Instantiate(explore, explorePoint, Quaternion.identity);
            yield return new WaitForSeconds(0.01f);
        }
        path.enemyTypes = EnemyPath.EnemyType.Death;
        ExploreDebris();
        foreach (MeshRenderer enemyObject in enemyObjects)
        {
            if (enemyObject != null)
            {
                enemyObject.enabled = false;
            }
        }
        for (int i = 0; i < 250; i++)
        {
            soundClip.PlayExplosion();
            Vector3 explorePoint = new Vector3(Random.Range(-exploreValues.x + transform.position.x, exploreValues.x + transform.position.x), Random.Range(-exploreValues.y + transform.position.y, exploreValues.y + transform.position.y), Random.Range(-exploreValues.z + transform.position.z, exploreValues.z + transform.position.z));
            Instantiate(explore, explorePoint, Quaternion.identity);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(gameObject);
    }
}
