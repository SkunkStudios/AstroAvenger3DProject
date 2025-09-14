using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    public enum EnemyType { Off, StartPath, Path, TimeChase, StartChase, Chase, Move, Stop, StartTurrel, Turrel, StartShoot, Shoot }
    public EnemyType enemyTypes;
    public enum PathType { Off, Spin, Scorpion, PowerBull, Raptor, Giant }
    public PathType pathTypes;
    public float startSpeed;
    public float speed;
    public float tumble;
    public float offsetZ;
    public Transform[] targetAims;
    public EnemyWeapon[] weapons;
    public float distIn;
    public float distBlast;
    public float distOut;
    public AstroLiner astroLiner;
    public Shark shark;
    public Animation animation;
    public AnimationClip openAnimation;
    public AnimationClip closeAnimation;
    public float mathfRot = 45;

    private Transform target;
    private GameObject[] destroyers;
    private Rigidbody rb;
    private float time;
    private float waitChase;
    private bool isSlide;
    private float moveSlide;
    private float fastSpeed = 1;
    private float dist;
    private bool isBlasting = true;
    private int laserWeapon;
    private int randomSlide;
    private bool isSwitchAnimation;

    // Use this for initialization
    void Start ()
	{
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * tumble;
        if (transform.position.x < 0)
        {
            isSlide = true;
        }
        else if (transform.position.x == 0)
        {
            randomSlide = Random.Range(0, 2);
            if (randomSlide == 0)
            {
                isSlide = true;
            }
            else
            {
                isSlide = false;
            }
        }
        else if (transform.position.x > 0)
        {
            isSlide = false;
        }
    }

    // Update is called once per frame
    void Update ()
	{
        if (destroyers == null || destroyers != null)
        {
            destroyers = GameObject.FindGameObjectsWithTag("Destroyer");
        }
        if (destroyers.Length >= 1)
        {
            target = GameObject.FindGameObjectWithTag("Destroyer").transform;
        }
        if (enemyTypes == EnemyType.StartPath && transform.position.z < 17.5)
        {
            if (animation != null && !isSwitchAnimation)
            {
                animation.clip = openAnimation;
                animation.Play();
                isSwitchAnimation = true;
            }
            enemyTypes = EnemyType.Path;
        }
        else if (enemyTypes == EnemyType.StartShoot && transform.position.z < 30)
        {
            if (astroLiner != null)
            {
                astroLiner.WaitLauncher();
            }
            if (shark != null)
            {
                StartCoroutine(shark.EnemyLauncher());
            }
            enemyTypes = EnemyType.Shoot;
        }
        if (enemyTypes == EnemyType.Path)
        {
            time += Time.deltaTime;
        }
        else if (enemyTypes == EnemyType.TimeChase)
        {
            waitChase += Time.deltaTime;
        }
        if (enemyTypes == EnemyType.TimeChase && waitChase >= 3)
        {
            if (animation != null && !isSwitchAnimation)
            {
                animation.clip = openAnimation;
                animation.Play();
                isSwitchAnimation = true;
            }
            enemyTypes = EnemyType.Chase;
        }
        if (enemyTypes == EnemyType.Path && pathTypes == PathType.PowerBull && time >= 0.5f && time < 1.25f)
        {
            time = 2f;
        }
        else if (enemyTypes == EnemyType.Path && pathTypes == PathType.PowerBull && time >= 2.25f && time < 2.5f)
        {
            time = 3f;
            isSlide = !isSlide;
        }
        else if (enemyTypes == EnemyType.Path && pathTypes == PathType.PowerBull && time >= 3.75f && time < 4.25f || enemyTypes == EnemyType.Path && pathTypes == PathType.PowerBull && time >= 6.75f)
        {
            time = 5f;
            isSlide = !isSlide;
        }
        if (transform.position.x > 16 && enemyTypes == EnemyType.Path && pathTypes == PathType.Raptor || transform.position.x > 18 && enemyTypes == EnemyType.Path && pathTypes == PathType.Giant)
        {
            isSlide = true;
        }
        else if (transform.position.x < -16 && enemyTypes == EnemyType.Path && pathTypes == PathType.Raptor || transform.position.x < -18 && enemyTypes == EnemyType.Path && pathTypes == PathType.Giant)
        {
            isSlide = false;
        }
        if (moveSlide <= -10 && isSlide)
        {
            moveSlide = -10;
        }
        else if (moveSlide >= 10 && !isSlide)
        {
            moveSlide = 10;
        }
        if (moveSlide > -10 && isSlide && enemyTypes == EnemyType.Path && pathTypes == PathType.Raptor && time < 5)
        {
            moveSlide -= Time.deltaTime * 20;
        }
        else if (moveSlide < 10 && !isSlide && enemyTypes == EnemyType.Path && pathTypes == PathType.Raptor && time < 5)
        {
            moveSlide += Time.deltaTime * 20;
        }
        if (isBlasting)
        {
            if (enemyTypes == EnemyType.Path || dist < distBlast && enemyTypes == EnemyType.Chase || enemyTypes == EnemyType.Stop || dist < distBlast && enemyTypes == EnemyType.Turrel || dist < distBlast && enemyTypes == EnemyType.Shoot)
            {
                if (weapons.Length == 1)
                {
                    weapons[0].SetLaserBlast(true);
                }
                else if (weapons.Length == 2)
                {
                    if (laserWeapon == 0)
                    {
                        weapons[0].SetLaserBlast(true);
                        weapons[1].SetLaserBlast(false);
                    }
                    else if (laserWeapon == 1)
                    {
                        weapons[0].SetLaserBlast(false);
                        weapons[1].SetLaserBlast(true);
                    }
                    else if (laserWeapon == 2)
                    {
                        laserWeapon = 0;
                    }
                }
                else if (weapons.Length == 3)
                {
                    if (laserWeapon == 0)
                    {
                        weapons[0].SetLaserBlast(true);
                        weapons[1].SetLaserBlast(false);
                        weapons[2].SetLaserBlast(false);
                    }
                    else if (laserWeapon == 1)
                    {
                        weapons[0].SetLaserBlast(false);
                        weapons[1].SetLaserBlast(true);
                        weapons[2].SetLaserBlast(false);
                    }
                    else if (laserWeapon == 2)
                    {
                        weapons[0].SetLaserBlast(false);
                        weapons[1].SetLaserBlast(false);
                        weapons[2].SetLaserBlast(true);
                    }
                    else if (laserWeapon == 3)
                    {
                        laserWeapon = 0;
                    }
                }
                else if (weapons.Length == 6)
                {
                    if (laserWeapon == 0)
                    {
                        weapons[0].SetLaserBlast(true);
                        weapons[1].SetLaserBlast(false);
                        weapons[2].SetLaserBlast(false);
                        weapons[3].SetLaserBlast(false);
                        weapons[4].SetLaserBlast(false);
                        weapons[5].SetLaserBlast(false);
                    }
                    else if (laserWeapon == 1)
                    {
                        weapons[0].SetLaserBlast(false);
                        weapons[1].SetLaserBlast(true);
                        weapons[2].SetLaserBlast(false);
                        weapons[3].SetLaserBlast(false);
                        weapons[4].SetLaserBlast(false);
                        weapons[5].SetLaserBlast(false);
                    }
                    else if (laserWeapon == 2)
                    {
                        weapons[0].SetLaserBlast(false);
                        weapons[1].SetLaserBlast(false);
                        weapons[2].SetLaserBlast(true);
                        weapons[3].SetLaserBlast(false);
                        weapons[4].SetLaserBlast(false);
                        weapons[5].SetLaserBlast(false);
                    }
                    else if (laserWeapon == 3)
                    {
                        weapons[0].SetLaserBlast(false);
                        weapons[1].SetLaserBlast(false);
                        weapons[2].SetLaserBlast(false);
                        weapons[3].SetLaserBlast(true);
                        weapons[4].SetLaserBlast(false);
                        weapons[5].SetLaserBlast(false);
                    }
                    else if (laserWeapon == 4)
                    {
                        weapons[0].SetLaserBlast(false);
                        weapons[1].SetLaserBlast(false);
                        weapons[2].SetLaserBlast(false);
                        weapons[3].SetLaserBlast(false);
                        weapons[4].SetLaserBlast(true);
                        weapons[5].SetLaserBlast(false);
                    }
                    else if (laserWeapon == 5)
                    {
                        weapons[0].SetLaserBlast(false);
                        weapons[1].SetLaserBlast(false);
                        weapons[2].SetLaserBlast(false);
                        weapons[3].SetLaserBlast(false);
                        weapons[4].SetLaserBlast(false);
                        weapons[5].SetLaserBlast(true);
                    }
                    else if (laserWeapon == 6)
                    {
                        laserWeapon = 0;
                    }
                }
            }
            else if (dist > distBlast && enemyTypes == EnemyType.Chase || enemyTypes == EnemyType.TimeChase)
            {
                foreach (EnemyWeapon weapon in weapons)
                {
                    if (weapon != null)
                    {
                        weapon.SetLaserBlast(false);
                    }
                }
            }
        }
        else
        {
            foreach (EnemyWeapon weapon in weapons)
            {
                if (weapon != null)
                {
                    weapon.SetLaserBlast(false);
                }
            }
        }
        Vector3 direction;
        Quaternion lookRotation;
        if (destroyers.Length == 0)
        {
            direction = (new Vector3(0.0f, 0.0f, -17) - transform.position).normalized;
            dist = Vector3.Distance(new Vector3(0.0f, 0.0f, -17), transform.position);
            if (enemyTypes == EnemyType.Path && pathTypes == PathType.Spin || enemyTypes == EnemyType.Path && pathTypes == PathType.Raptor && time < 5 || enemyTypes == EnemyType.Chase || enemyTypes == EnemyType.Turrel)
            {
                lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * 60);
            }
            else if (enemyTypes == EnemyType.Path && pathTypes == PathType.Raptor && time >= 5)
            {
                lookRotation = Quaternion.Euler(0, 180, 0);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * 60);
            }
        }
        if (target != null)
        {
            if (destroyers.Length >= 1)
            {
                direction = (target.position - transform.position).normalized;
                dist = Vector3.Distance(target.position, transform.position);
                if (enemyTypes == EnemyType.Path && pathTypes == PathType.Spin || enemyTypes == EnemyType.Path && pathTypes == PathType.Raptor && time < 5 || enemyTypes == EnemyType.Chase || enemyTypes == EnemyType.Turrel)
                {
                    lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * 60);
                }
                else if (enemyTypes == EnemyType.Path && pathTypes == PathType.Raptor && time >= 5)
                {
                    lookRotation = Quaternion.Euler(0, 180, 0);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * 60);
                }
            }
        }
        if (targetAims.Length >= 1 && isBlasting)
        {
            foreach (Transform targetAim in targetAims)
            {
                if (destroyers.Length == 0)
                {
                    direction = (new Vector3(0.0f, 0.0f, -17) - targetAim.position).normalized;
                    lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                    if (enemyTypes == EnemyType.Path || enemyTypes == EnemyType.Chase || enemyTypes == EnemyType.Stop || enemyTypes == EnemyType.Turrel)
                    {
                        targetAim.rotation = Quaternion.RotateTowards(targetAim.rotation, lookRotation, Time.deltaTime * 75);
                        targetAim.eulerAngles = new Vector3(0, Mathf.Clamp(targetAim.eulerAngles.y, transform.eulerAngles.y - mathfRot, transform.eulerAngles.y + mathfRot), 0);
                    }
                }
                else if (destroyers.Length >= 1)
                {
                    direction = (target.position - targetAim.position).normalized;
                    lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                    if (enemyTypes == EnemyType.Path || enemyTypes == EnemyType.Chase || enemyTypes == EnemyType.Stop || enemyTypes == EnemyType.Turrel)
                    {
                        targetAim.rotation = Quaternion.RotateTowards(targetAim.rotation, lookRotation, Time.deltaTime * 75);
                        targetAim.rotation = Quaternion.Euler(0, Mathf.Clamp(targetAim.eulerAngles.y, transform.eulerAngles.y - mathfRot, transform.eulerAngles.y + mathfRot), 0);
                    }
                }
            }
        }
        if (enemyTypes == EnemyType.Off && transform.position.z <= offsetZ || enemyTypes == EnemyType.StartChase && transform.position.z <= offsetZ || enemyTypes == EnemyType.StartTurrel && transform.position.z <= offsetZ || enemyTypes == EnemyType.Turrel && transform.position.z <= offsetZ || enemyTypes == EnemyType.StartShoot && transform.position.z <= offsetZ || enemyTypes == EnemyType.Shoot && transform.position.z <= offsetZ)
        {
            Destroy(gameObject);
        }
        else if (enemyTypes == EnemyType.Path && transform.position.z <= offsetZ)
        {
            if (animation != null && isSwitchAnimation)
            {
                animation.clip = closeAnimation;
                animation.Play();
                isSwitchAnimation = false;
            }
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            if (transform.position.x < 0)
            {
                transform.position = new Vector3(-30, 0, 0);
                transform.rotation = Quaternion.Euler(0, 90, 0);
                if (targetAims != null)
                {
                    foreach (Transform targetAim in targetAims)
                    {
                        targetAim.rotation = Quaternion.Euler(0, 90, 0);
                    }
                }
            }
            else if (transform.position.x == 0)
            {
                randomSlide = Random.Range(0, 2);
                if (randomSlide == 0)
                {
                    transform.position = new Vector3(-30, 0, 0);
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                    if (targetAims != null)
                    {
                        foreach (Transform targetAim in targetAims)
                        {
                            targetAim.rotation = Quaternion.Euler(0, 90, 0);
                        }
                    }
                }
                else
                {
                    transform.position = new Vector3(30, 0, 0);
                    transform.rotation = Quaternion.Euler(0, -90, 0);
                    if (targetAims != null)
                    {
                        foreach (Transform targetAim in targetAims)
                        {
                            targetAim.rotation = Quaternion.Euler(0, -90, 0);
                        }
                    }
                }
            }
            else if (transform.position.x > 0)
            {
                transform.position = new Vector3(30, 0, 0);
                transform.rotation = Quaternion.Euler(0, -90, 0);
                if (targetAims != null)
                {
                    foreach (Transform targetAim in targetAims)
                    {
                        targetAim.rotation = Quaternion.Euler(0, -90, 0);
                    }
                }
            }
            enemyTypes = EnemyType.TimeChase;
        }
        else if (enemyTypes == EnemyType.Move && transform.position.z <= offsetZ)
        {
            enemyTypes = EnemyType.Stop;
        }
        else if (enemyTypes == EnemyType.Stop)
        {
            rb.velocity = Vector3.zero;
            transform.position = new Vector3(transform.position.x, 0, offsetZ);
        }
        if (dist < distIn)
        {
            Hit();
        }
        if (dist > distBlast && enemyTypes == EnemyType.Chase || enemyTypes == EnemyType.Path && pathTypes == PathType.Scorpion && time >= 3 || enemyTypes == EnemyType.Path && pathTypes == PathType.PowerBull && time >= 0.5f)
        {
            rb.velocity = transform.forward * speed;
        }
        if (dist > distOut && enemyTypes == EnemyType.Chase && dist < distBlast && enemyTypes == EnemyType.Chase)
        {
            rb.velocity = Vector3.zero;
        }
        if (dist < distOut && enemyTypes == EnemyType.Chase)
        {
            rb.velocity = transform.forward * -speed;
        }
        if (enemyTypes == EnemyType.Off || enemyTypes == EnemyType.Move || enemyTypes == EnemyType.StartTurrel || enemyTypes == EnemyType.Turrel || enemyTypes == EnemyType.StartShoot || enemyTypes == EnemyType.Shoot)
        {
            rb.velocity = Vector3.forward * -speed;
        }
        else if (enemyTypes == EnemyType.Path && pathTypes == PathType.Raptor && time >= 5)
        {
            fastSpeed += Time.deltaTime * 2;
            rb.velocity = Vector3.forward * -speed * fastSpeed;
        }
        else if (enemyTypes == EnemyType.Path && pathTypes == PathType.Giant && time >= 5)
        {
            rb.velocity = Vector3.forward * -speed * 2;
        }
        else if (enemyTypes == EnemyType.StartPath || enemyTypes == EnemyType.StartChase)
        {
            rb.velocity = Vector3.forward * -startSpeed;
        }
        if (transform.position.y < 0 && enemyTypes == EnemyType.Chase)
        {
            rb.velocity = Vector3.up * 5;
        }
        else if (transform.position.y > 0 && enemyTypes == EnemyType.Chase)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        if (enemyTypes == EnemyType.Path && pathTypes == PathType.Scorpion && time >= 3)
        {
            if (isSlide)
            {
                rb.angularVelocity = Vector3.up * -0.5f;
            }
            else
            {
                rb.angularVelocity = Vector3.up * 0.5f;
            }
        }
        else if (enemyTypes == EnemyType.Path && pathTypes == PathType.PowerBull && time >= 0.5f)
        {
            if (isSlide)
            {
                rb.angularVelocity = Vector3.up * -1.125f;
            }
            else
            {
                rb.angularVelocity = Vector3.up * 1.125f;
            }
        }
        if (enemyTypes == EnemyType.Path && pathTypes == PathType.Raptor && time < 5)
        {
            rb.velocity = new Vector3(moveSlide, 0, -startSpeed * 0.5f);
        }
        else if (enemyTypes == EnemyType.Path && pathTypes == PathType.Giant && time < 5)
        {
            if (isSlide)
            {
                rb.velocity = new Vector3(-5, 0, -startSpeed * 0.5f);
            }
            else
            {
                rb.velocity = new Vector3(5, 0, -startSpeed * 0.5f);
            }
        }
    }

    public void Hit()
    {
        if (enemyTypes == EnemyType.StartChase && transform.position.z < 25)
        {
            if (animation != null && !isSwitchAnimation)
            {
                animation.clip = openAnimation;
                animation.Play();
                isSwitchAnimation = true;
            }
            enemyTypes = EnemyType.Chase;
        }
        else if (enemyTypes == EnemyType.StartTurrel && transform.position.z < 25)
        {
            enemyTypes = EnemyType.Turrel;
        }
    }

    public void DeathPath()
    {
        isBlasting = false;
    }

    public void NextWeapon()
    {
        laserWeapon++;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, distIn);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distBlast);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distOut);
    }
}
