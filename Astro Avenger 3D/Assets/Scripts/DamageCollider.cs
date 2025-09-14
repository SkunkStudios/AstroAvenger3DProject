using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    public Destroyer destroyer;
    public EnemyHealth enemyHealth;
    public EnemyPath enemyPath;
    public bool isHitEnemy;

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
            if (!destroyer.isImmortal)
            {
                destroyer.Damage(100);
            }
        }
        if (other.CompareTag("DestroyerCollider") && enemyHealth != null || other.CompareTag("DestroyerImmortal") && enemyHealth != null || other.CompareTag("EnemyCollider") && enemyHealth != null && isHitEnemy || other.CompareTag("Meteorit") && enemyHealth != null || other.CompareTag("AsteroidCollider") && enemyHealth != null && isHitEnemy || other.CompareTag("LightningCollider") && enemyHealth != null)
        {
            enemyHealth.Damage(100);
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

    public void Immortal()
    {
        destroyer.Immortal();
    }

    public void LazerModificator()
    {
        destroyer.LazerModificator();
    }

    public void AddStrait()
    {
        destroyer.AddStrait();
    }

    public void AddSmart()
    {
        destroyer.AddSmart();
    }

    public void AddMedusa()
    {
        destroyer.AddMedusa();
    }

    public void AddSwarm()
    {
        destroyer.AddSwarm();
    }

    public void AddNuke()
    {
        destroyer.AddNuke();
    }
}
