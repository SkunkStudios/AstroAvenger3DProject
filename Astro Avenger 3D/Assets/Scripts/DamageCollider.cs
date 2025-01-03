using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    public Destroyer destroyer;
    public EnemyHealth enemyHealth;
    public EnemyPath enemyPath;
    public bool isHitEnemy;

    // Use this for initialization
    void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NukeCollider") && enemyHealth != null)
        {
            enemyHealth.Damage(1000);
        }
        if (other.CompareTag("EnemyTrigger") && enemyPath != null)
        {
            enemyPath.Hit();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("EnemyCollider") && destroyer != null || other.CompareTag("Meteorit") && destroyer != null || other.CompareTag("AsteroidCollider") && destroyer != null || other.CompareTag("LightningCollider") && destroyer != null)
        {
            destroyer.Damage(50);
        }
        if (other.CompareTag("DestroyerCollider") && enemyHealth != null || other.CompareTag("EnemyCollider") && enemyHealth != null && isHitEnemy || other.CompareTag("Meteorit") && enemyHealth != null || other.CompareTag("AsteroidCollider") && enemyHealth != null && isHitEnemy || other.CompareTag("LightningCollider") && enemyHealth != null)
        {
            enemyHealth.Damage(50);
        }
        else if (other.CompareTag("DestroyerLaser") && enemyPath != null)
        {
            enemyPath.Hit();
        }
    }

    public void Damage(float damage)
    {
        if (destroyer != null)
        {
            destroyer.Damage(damage);
        }
        if (enemyHealth != null)
        {
            enemyHealth.Damage(damage);
        }
    }

    public void RestoreHealth()
    {
        destroyer.RestoreHealth();
    }

    public void RestoreEnergy()
    {
        destroyer.RestoreEnergy();
    }
}
