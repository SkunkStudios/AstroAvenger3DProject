using System.Collections;
using UnityEngine;

[System.Serializable]
public class Parts
{
    public GameObject part;
    public Transform partSpawns;
    public MeshRenderer[] enemyObjects;
}
public class EnemyHealth : MonoBehaviour
{
    public enum ExploreType { Off, WaitTime, Enterprise }
    public ExploreType exploreTypes;

    public GameObject explore;
    public GameObject bigExplore;
    public Vector3 exploreValues;
    public GameObject bonus;
    public Parts[] parts;
    public float maxHealth;
    public bool isMeteorit;
    public GameObject healthUI;
    public HealthBar healthBar;
    public AstroLiner astroLiner;
    public float camHit;
    public int scoreValue;
    public int countTime;
    public Collider[] colliders;

    private float health;
    private bool waitExplore;
    private Game game;
    private GameManager gameManager;
    private CameraHit cameraHit;
    private SoundClip soundClip;
    private MeshRenderer[] enemyObjects;
    private EnemyPath path;

    void Awake()
    {
        game = GameObject.FindObjectOfType<Game>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        cameraHit = GameObject.FindObjectOfType<CameraHit>();
        soundClip = GameObject.FindObjectOfType<SoundClip>();
        path = GetComponent<EnemyPath>();
    }

    void Start ()
	{
        health = maxHealth;
        if (exploreTypes == ExploreType.Off)
        {
            waitExplore = false;
        }
        else
        {
            waitExplore = true;
        }
        if (transform.position.z > 30)
        {
            foreach (Collider collider in colliders)
            {
                if (collider != null)
                {
                    collider.enabled = false;
                }
            }
        }
    }

    void Update ()
	{
        enemyObjects = GetComponentsInChildren<MeshRenderer>();
        if (transform.position.z < 30 && health > 0)
        {
            foreach (Collider collider in colliders)
            {
                collider.enabled = true;
            }
        }
        if (GameObject.FindObjectOfType<Game>().gameOver && healthUI != null)
        {
            healthUI.SetActive(false);
        }
        if (healthBar != null)
        {
            healthBar.slider.value = health;
            healthBar.slider.maxValue = maxHealth;
            healthBar.fill.color = healthBar.gradient.Evaluate(healthBar.slider.normalizedValue);
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
                game.AddScore(scoreValue);
                if (gameManager.isMemeSounds)
                {
                    gameManager.MemeClipsUI();
                }
                if (healthUI != null)
                {
                    healthUI.SetActive(false);
                }
                if (astroLiner != null)
                {
                    StartCoroutine(astroLiner.TermitExplores());
                }
                if (exploreTypes == ExploreType.WaitTime)
                {
                    StartCoroutine(WaitExplore());
                }
                if (exploreTypes == ExploreType.Enterprise)
                {
                    StartCoroutine(EnterpriseExplore());
                }
                waitExplore = false;
            }
            if(exploreTypes == ExploreType.Off)
            {
                game.AddScore(scoreValue);
                cameraHit.Hit(camHit);
                if (gameManager.isMemeSounds)
                {
                    gameManager.MemeClipsUI();
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
        for (int i = 0; i < countTime; i++)
        {
            soundClip.PlayExplosion();
            Vector3 explorePoint = new Vector3(Random.Range(-exploreValues.x + transform.position.x, exploreValues.x + transform.position.x), Random.Range(-exploreValues.y + transform.position.y, exploreValues.y + transform.position.y), Random.Range(-exploreValues.z + transform.position.z, exploreValues.z + transform.position.z));
            Instantiate(explore, explorePoint, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
        if (parts.Length <= 0)
        {
            foreach (MeshRenderer enemyObject in enemyObjects)
            {
                if (enemyObject != null)
                {
                    enemyObject.enabled = false;
                }
            }
        }
        else if (parts.Length >= 1)
        {
            foreach (Parts part in parts)
            {
                for (int i = 0; i < 5; i++)
                {
                    soundClip.PlayExplosion();
                    Vector3 explorePoint = new Vector3(Random.Range(-exploreValues.x + transform.position.x, exploreValues.x + transform.position.x), Random.Range(-exploreValues.y + transform.position.y, exploreValues.y + transform.position.y), Random.Range(-exploreValues.z + transform.position.z, exploreValues.z + transform.position.z));
                    Instantiate(explore, explorePoint, Quaternion.identity);
                    yield return new WaitForSeconds(0.1f);
                }
                if (part != null)
                {
                    Instantiate(part.part, part.partSpawns.position, part.partSpawns.rotation);
                }
                foreach (MeshRenderer enemyObject in part.enemyObjects)
                {
                    if (enemyObject != null)
                    {
                        enemyObject.enabled = false;
                    }
                }
            }
        }
        for (int i = 0; i < countTime; i++)
        {
            soundClip.PlayExplosion();
            Vector3 explorePoint = new Vector3(Random.Range(-exploreValues.x + transform.position.x, exploreValues.x + transform.position.x), Random.Range(-exploreValues.y + transform.position.y, exploreValues.y + transform.position.y), Random.Range(-exploreValues.z + transform.position.z, exploreValues.z + transform.position.z));
            Instantiate(explore, explorePoint, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
    }

    IEnumerator EnterpriseExplore()
    {
        Vector3 explorePoint;
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(1.5f);
            cameraHit.Hit(3);
            soundClip.PlayExplosion();
            explorePoint = new Vector3(Random.Range(-exploreValues.x + transform.position.x, exploreValues.x + transform.position.x), Random.Range(-exploreValues.y + transform.position.y, exploreValues.y + transform.position.y), Random.Range(-exploreValues.z + transform.position.z, exploreValues.z + transform.position.z));
            Instantiate(explore, explorePoint, Quaternion.identity);
        }
        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(0.5f);
            cameraHit.Hit(3);
            soundClip.PlayExplosion();
            explorePoint = new Vector3(Random.Range(-exploreValues.x + transform.position.x, exploreValues.x + transform.position.x), Random.Range(-exploreValues.y + transform.position.y, exploreValues.y + transform.position.y), Random.Range(-exploreValues.z + transform.position.z, exploreValues.z + transform.position.z));
            Instantiate(explore, explorePoint, Quaternion.identity);
        }
        for (int i = 0; i < 7; i++)
        {
            yield return new WaitForSeconds(0.4f);
            cameraHit.Hit(3);
            soundClip.PlayExplosion();
            explorePoint = new Vector3(Random.Range(-exploreValues.x + transform.position.x, exploreValues.x + transform.position.x), Random.Range(-exploreValues.y + transform.position.y, exploreValues.y + transform.position.y), Random.Range(-exploreValues.z + transform.position.z, exploreValues.z + transform.position.z));
            Instantiate(explore, explorePoint, Quaternion.identity);
        }
        for (int i = 0; i < 8; i++)
        {
            yield return new WaitForSeconds(0.3f);
            cameraHit.Hit(3);
            soundClip.PlayExplosion();
            explorePoint = new Vector3(Random.Range(-exploreValues.x + transform.position.x, exploreValues.x + transform.position.x), Random.Range(-exploreValues.y + transform.position.y, exploreValues.y + transform.position.y), Random.Range(-exploreValues.z + transform.position.z, exploreValues.z + transform.position.z));
            Instantiate(explore, explorePoint, Quaternion.identity);
        }
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.2f);
            cameraHit.Hit(3);
            soundClip.PlayExplosion();
            explorePoint = new Vector3(Random.Range(-exploreValues.x + transform.position.x, exploreValues.x + transform.position.x), Random.Range(-exploreValues.y + transform.position.y, exploreValues.y + transform.position.y), Random.Range(-exploreValues.z + transform.position.z, exploreValues.z + transform.position.z));
            Instantiate(explore, explorePoint, Quaternion.identity);
        }
        for (int i = 0; i < 40; i++)
        {
            yield return new WaitForSeconds(0.1f);
            cameraHit.Hit(3);
            soundClip.PlayExplosion();
            explorePoint = new Vector3(Random.Range(-exploreValues.x + transform.position.x, exploreValues.x + transform.position.x), Random.Range(-exploreValues.y + transform.position.y, exploreValues.y + transform.position.y), Random.Range(-exploreValues.z + transform.position.z, exploreValues.z + transform.position.z));
            Instantiate(explore, explorePoint, Quaternion.identity);
        }
        for (int i = 0; i < 50; i++)
        {
            cameraHit.Hit(3);
            soundClip.PlayExplosion();
            explorePoint = new Vector3(Random.Range(-exploreValues.x + transform.position.x, exploreValues.x + transform.position.x), Random.Range(-exploreValues.y + transform.position.y, exploreValues.y + transform.position.y), Random.Range(-exploreValues.z + transform.position.z, exploreValues.z + transform.position.z));
            Instantiate(bigExplore, explorePoint, Quaternion.identity);
        }
        soundClip.PlaySound("xplode");
        Destroy(gameObject);
    }
}
