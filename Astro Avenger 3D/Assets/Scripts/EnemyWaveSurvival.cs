using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSurvival : MonoBehaviour
{
    public GameObject[] bonuses;
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
    public EnemySpawn[] astroLinerC;
    public EnemySpawn[] astroLinerL;
    public EnemySpawn[] astroLinerR;
    public EnemySpawn[] astroLinerLR;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    // Use this for initialization
    void Start ()
    {
        if (GameObject.FindObjectOfType<GameManager>().mode == GameManager.Mode.Survival)
        {
            StartCoroutine(SpawnWaves());
        }
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            int rotateSurvival = Random.Range(0, 10);
            if (rotateSurvival == 0)
            {
                platforms[Random.Range(0, platforms.Length)].SpawnAttack();
            }
            else if (rotateSurvival == 1 || rotateSurvival == 8 || rotateSurvival == 9)
            {
                platformsC[Random.Range(0, platformsC.Length)].SpawnAttack();
            }
            else if (rotateSurvival == 2)
            {
                platformsL[Random.Range(0, platformsL.Length)].SpawnAttack();
            }
            else if (rotateSurvival == 3)
            {
                platformsR[Random.Range(0, platformsR.Length)].SpawnAttack();
            }
            if (rotateSurvival == 4)
            {
                astroLinerC[Random.Range(0, astroLinerC.Length)].SpawnAttack();
            }
            else if (rotateSurvival == 5 || rotateSurvival == 8)
            {
                astroLinerL[Random.Range(0, astroLinerL.Length)].SpawnAttack();
            }
            else if (rotateSurvival == 6 || rotateSurvival == 9)
            {
                astroLinerR[Random.Range(0, astroLinerR.Length)].SpawnAttack();
            }
            else if (rotateSurvival == 7)
            {
                astroLinerLR[Random.Range(0, astroLinerLR.Length)].SpawnAttack();
            }
            for (int i = 0; i < hazardCount; i++)
            {
                Instantiate(meteorit[Random.Range(0, meteorit.Length)], new Vector3(Random.Range(-30, 30), 0, 25), Quaternion.Euler(0, 180, 0));
                if (rotateSurvival == 0)
                {
                    enemySpawns[Random.Range(0, enemySpawns.Length)].SpawnAttackWithBonus(bonuses[Random.Range(0, bonuses.Length)]);
                }
                else if (rotateSurvival == 7)
                {
                    enemySpawnsC[Random.Range(0, enemySpawnsC.Length)].SpawnAttackWithBonus(bonuses[Random.Range(0, bonuses.Length)]);
                }
                else if (rotateSurvival == 2 || rotateSurvival == 5 || rotateSurvival == 8)
                {
                    enemySpawnsL[Random.Range(0, enemySpawnsL.Length)].SpawnAttackWithBonus(bonuses[Random.Range(0, bonuses.Length)]);
                }
                else if (rotateSurvival == 3 || rotateSurvival == 6 || rotateSurvival == 9)
                {
                    enemySpawnsR[Random.Range(0, enemySpawnsR.Length)].SpawnAttackWithBonus(bonuses[Random.Range(0, bonuses.Length)]);
                }
                else if (rotateSurvival == 1 || rotateSurvival == 4)
                {
                    enemySpawnsLR[Random.Range(0, enemySpawnsLR.Length)].SpawnAttackWithBonus(bonuses[Random.Range(0, bonuses.Length)]);
                }
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }
}
