using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
	public GameObject enemy;
	public Vector3[] spawnPosition = new Vector3[1];
    public float time;

    public void SpawnAttack()
    {
        for (int i = 0; i < spawnPosition.Length; i++)
        {
            Instantiate(enemy, spawnPosition[i], Quaternion.Euler(0, 180, 0));
        }
    }

    public void SpawnAttackWithBonus(GameObject bonuses)
    {
        for (int i = 0; i < spawnPosition.Length; i++)
        {
            GameObject e = Instantiate(enemy) as GameObject;
            e.transform.position = spawnPosition[i];
            e.transform.rotation = Quaternion.Euler(0, 180, 0);
            if (e.GetComponent<EnemyHealth>() != null)
            {
                e.GetComponent<EnemyHealth>().bonus = bonuses;
            }
        }
    }
}
