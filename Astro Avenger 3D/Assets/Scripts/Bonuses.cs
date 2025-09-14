using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonuses : MonoBehaviour
{
    public enum BonusesType { energy, immortal, life, modificator, money, repair, rocket_medusa, rocket_nuke, rocket_smart, rocket_strait, rocket_swarm }
    public BonusesType type;
    public GameObject bonuseffect;

    private Game game;
    private SoundClip soundClip;
    private Rigidbody rb;

    void Awake()
    {
        game = GameObject.FindObjectOfType<Game>();
        soundClip = GameObject.FindObjectOfType<SoundClip>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rb.velocity = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        rb.angularVelocity = Random.insideUnitSphere * 10;
    }

    void Update()
    {
        if (transform.position.z >= 22.5f || transform.position.z <= -22.5f || transform.position.x >= 40 || transform.position.x <= -40)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DestroyerCollider") || other.CompareTag("DestroyerImmortal"))
        {
            if (type == Bonuses.BonusesType.energy)
            {
                other.GetComponent<DamageCollider>().RestoreEnergy();
            }
            else if (type == Bonuses.BonusesType.immortal)
            {
                other.GetComponent<DamageCollider>().Immortal();
            }
            else if (type == Bonuses.BonusesType.life)
            {
                game.life++;
            }
            else if (type == Bonuses.BonusesType.modificator)
            {
                other.GetComponent<DamageCollider>().LazerModificator();
            }
            else if (type == Bonuses.BonusesType.money)
            {
                game.AddScore(500);
            }
            else if (type == Bonuses.BonusesType.repair)
            {
                other.GetComponent<DamageCollider>().RestoreHealth();
            }
            else if (type == Bonuses.BonusesType.rocket_medusa)
            {
                other.GetComponent<DamageCollider>().AddMedusa();
            }
            else if (type == Bonuses.BonusesType.rocket_nuke)
            {
                other.GetComponent<DamageCollider>().AddNuke();
            }
            else if (type == Bonuses.BonusesType.rocket_smart)
            {
                other.GetComponent<DamageCollider>().AddSmart();
            }
            else if (type == Bonuses.BonusesType.rocket_strait)
            {
                other.GetComponent<DamageCollider>().AddStrait();
            }
            else if (type == Bonuses.BonusesType.rocket_swarm)
            {
                other.GetComponent<DamageCollider>().AddSwarm();
            }
            soundClip.PlaySound("bonus");
            Instantiate(bonuseffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
