using System.Collections;
using UnityEngine;

public class Shark : MonoBehaviour
{
    public GameObject locust;
    public Transform[] enemySpawns = new Transform[2];

    public IEnumerator EnemyLauncher()
    {
        yield return new WaitForSeconds(16f);
        Instantiate(locust, enemySpawns[0].position, enemySpawns[0].rotation);
        Instantiate(locust, enemySpawns[1].position, enemySpawns[1].rotation);
        yield return new WaitForSeconds(1f);
        Instantiate(locust, enemySpawns[0].position, enemySpawns[0].rotation);
        Instantiate(locust, enemySpawns[1].position, enemySpawns[1].rotation);
        yield return new WaitForSeconds(1f);
        Instantiate(locust, enemySpawns[0].position, enemySpawns[0].rotation);
        Instantiate(locust, enemySpawns[1].position, enemySpawns[1].rotation);
        yield return new WaitForSeconds(1f);
        Instantiate(locust, enemySpawns[0].position, enemySpawns[0].rotation);
        Instantiate(locust, enemySpawns[1].position, enemySpawns[1].rotation);
    }
}
