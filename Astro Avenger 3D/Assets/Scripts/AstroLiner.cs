using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroLiner : MonoBehaviour
{
    public Animation animation;
    public GameObject explore;
    public GameObject termit;
    public GameObject termitPart;
    public GameObject[] enemySpawns = new GameObject[8];

    private int enemyCount;

    public void WaitLauncher()
    {
        animation.Play();
    }

    public void EnemyLauncher()
    {
        if (enemyCount == 0 && enemySpawns[0] != null && enemySpawns[4] != null)
        {
            Instantiate(termit, enemySpawns[0].transform.position, enemySpawns[0].transform.rotation);
            Destroy(enemySpawns[0]);
            Instantiate(termit, enemySpawns[4].transform.position, enemySpawns[4].transform.rotation);
            Destroy(enemySpawns[4]);
        }
        else if (enemyCount == 1 && enemySpawns[1] != null && enemySpawns[5] != null)
        {
            Instantiate(termit, enemySpawns[1].transform.position, enemySpawns[1].transform.rotation);
            Destroy(enemySpawns[1]);
            Instantiate(termit, enemySpawns[5].transform.position, enemySpawns[5].transform.rotation);
            Destroy(enemySpawns[5]);
        }
        else if (enemyCount == 2 && enemySpawns[2] != null && enemySpawns[6] != null)
        {
            Instantiate(termit, enemySpawns[2].transform.position, enemySpawns[2].transform.rotation);
            Destroy(enemySpawns[2]);
            Instantiate(termit, enemySpawns[6].transform.position, enemySpawns[6].transform.rotation);
            Destroy(enemySpawns[6]);
        }
        else if (enemyCount == 3 && enemySpawns[3] != null && enemySpawns[7] != null)
        {
            Instantiate(termit, enemySpawns[3].transform.position, enemySpawns[3].transform.rotation);
            Destroy(enemySpawns[3]);
            Instantiate(termit, enemySpawns[7].transform.position, enemySpawns[7].transform.rotation);
            Destroy(enemySpawns[7]);
        }
        enemyCount++;
    }

    public IEnumerator TermitExplores()
    {
        animation.Stop();
        yield return new WaitForSeconds(0.25f);
        if (enemyCount == 0)
        {
            Instantiate(explore, enemySpawns[0].transform.position, enemySpawns[0].transform.rotation);
            Instantiate(termitPart, enemySpawns[0].transform.position, enemySpawns[0].transform.rotation);
            Destroy(enemySpawns[0]);
            Instantiate(explore, enemySpawns[4].transform.position, enemySpawns[4].transform.rotation);
            Instantiate(termitPart, enemySpawns[4].transform.position, enemySpawns[4].transform.rotation);
            Destroy(enemySpawns[4]);
        }
        yield return new WaitForSeconds(0.25f);
        if (enemyCount <= 1)
        {
            Instantiate(explore, enemySpawns[1].transform.position, enemySpawns[1].transform.rotation);
            Instantiate(termitPart, enemySpawns[1].transform.position, enemySpawns[1].transform.rotation);
            Destroy(enemySpawns[1]);
            Instantiate(explore, enemySpawns[5].transform.position, enemySpawns[5].transform.rotation);
            Instantiate(termitPart, enemySpawns[5].transform.position, enemySpawns[5].transform.rotation);
            Destroy(enemySpawns[5]);
        }
        yield return new WaitForSeconds(0.25f);
        if (enemyCount <= 2)
        {
            Instantiate(explore, enemySpawns[2].transform.position, enemySpawns[2].transform.rotation);
            Instantiate(termitPart, enemySpawns[2].transform.position, enemySpawns[2].transform.rotation);
            Destroy(enemySpawns[2]);
            Instantiate(explore, enemySpawns[6].transform.position, enemySpawns[6].transform.rotation);
            Instantiate(termitPart, enemySpawns[6].transform.position, enemySpawns[6].transform.rotation);
            Destroy(enemySpawns[6]);
        }
        yield return new WaitForSeconds(0.25f);
        if (enemyCount <= 3)
        {
            Instantiate(explore, enemySpawns[3].transform.position, enemySpawns[3].transform.rotation);
            Instantiate(termitPart, enemySpawns[3].transform.position, enemySpawns[3].transform.rotation);
            Destroy(enemySpawns[3]);
            Instantiate(explore, enemySpawns[7].transform.position, enemySpawns[7].transform.rotation);
            Instantiate(termitPart, enemySpawns[7].transform.position, enemySpawns[7].transform.rotation);
            Destroy(enemySpawns[7]);
        }
    }
}
