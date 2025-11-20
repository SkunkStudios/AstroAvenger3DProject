using System.Collections;
using UnityEngine;

public class Shark : MonoBehaviour
{
    public GameObject locust;
    public Transform[] enemySpawns = new Transform[2];

    public IEnumerator EnemyLauncher()
    {
        yield return new WaitForSeconds(15);
        for (int i = 0; i < 4; i++)
        {
            GameObject enemy;
            enemy = Instantiate(locust) as GameObject;
            enemy.transform.position = enemySpawns[0].transform.position;
            enemy.transform.rotation = enemySpawns[0].transform.rotation;
            enemy.GetComponent<EnemyPath>().enemyTypes = EnemyPath.EnemyType.Chase;
            enemy = Instantiate(locust) as GameObject;
            enemy.transform.position = enemySpawns[1].transform.position;
            enemy.transform.rotation = enemySpawns[1].transform.rotation;
            enemy.GetComponent<EnemyPath>().enemyTypes = EnemyPath.EnemyType.Chase;
            yield return new WaitForSeconds(1f);
        }
    }
}
