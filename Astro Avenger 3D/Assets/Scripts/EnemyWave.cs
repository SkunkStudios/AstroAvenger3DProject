using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    public GameObject[] meteorit;
    public EnemySpawn[] enemySpawns;
    public EnemySpawn[] enemySpawnsC;
    public EnemySpawn[] enemySpawnsL;
    public EnemySpawn[] enemySpawnsR;
    public EnemySpawn[] enemySpawnsLR;
    public EnemySpawn[] platforms;
    public EnemySpawn[] platformsC;
    public EnemySpawn[] platformsL;
    public EnemySpawn[] platformsR;
    public EnemySpawn[] platformsLR;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(SpawnWavesSurvival());
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    IEnumerator SpawnWavesSurvival()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            int rotateSurvival = Random.Range(0, 5);
            if (rotateSurvival == 0)
            {
                platforms[Random.Range(0, platforms.Length)].SpawnAttack();
            }
            else if (rotateSurvival == 1)
            {
                platformsLR[Random.Range(0, platformsLR.Length)].SpawnAttack();
            }
            else if (rotateSurvival == 2)
            {
                platformsL[Random.Range(0, platformsL.Length)].SpawnAttack();
            }
            else if (rotateSurvival == 3)
            {
                platformsR[Random.Range(0, platformsR.Length)].SpawnAttack();
            }
            else if (rotateSurvival == 4)
            {
                platformsC[Random.Range(0, platformsC.Length)].SpawnAttack();
            }
            for (int i = 0; i < hazardCount; i++)
            {
                Instantiate(meteorit[Random.Range(0, meteorit.Length)], new Vector3(Random.Range(-30, 30), 0, 25), Quaternion.Euler(0, 180, 0));
                if (rotateSurvival == 0)
                {
                    enemySpawns[Random.Range(0, enemySpawns.Length)].SpawnAttack();
                }
                else if (rotateSurvival == 1)
                {
                    enemySpawnsC[Random.Range(0, enemySpawnsC.Length)].SpawnAttack();
                }
                else if (rotateSurvival == 2)
                {
                    enemySpawnsL[Random.Range(0, enemySpawnsL.Length)].SpawnAttack();
                }
                else if (rotateSurvival == 3)
                {
                    enemySpawnsR[Random.Range(0, enemySpawnsR.Length)].SpawnAttack();
                }
                else if (rotateSurvival == 4)
                {
                    enemySpawnsLR[Random.Range(0, enemySpawnsLR.Length)].SpawnAttack();
                }
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }
}
