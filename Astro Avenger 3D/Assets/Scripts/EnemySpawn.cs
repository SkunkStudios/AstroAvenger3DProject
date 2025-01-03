using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
	public GameObject enemy;
	public Vector3[] spawnPosition = new Vector3[1];

    public void SpawnAttack()
    {
        for (int i = 0; i < spawnPosition.Length; i++)
        {
            Instantiate(enemy, spawnPosition[i], Quaternion.Euler(0, 180, 0));
        }
    }
}
