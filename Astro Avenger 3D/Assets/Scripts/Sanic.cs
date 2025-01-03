using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sanic : MonoBehaviour
{
    private Vector3 randomMove;

    void Start ()
	{
		randomMove.x = Random.Range(-100, 100);
        randomMove.y = Random.Range(-100, 100);
        GetComponent<AudioSource>().Play();
        Destroy(gameObject, 2);
    }

    void Update () 
	{
		transform.Rotate(Vector3.back * 1000 * Time.deltaTime);
        transform.Translate(randomMove * Time.deltaTime, Space.World);
        if (transform.position.x >= 13)
        {
            randomMove.x = -randomMove.x;
        }
        else if (transform.position.x <= -13)
        {
            randomMove.x = -randomMove.x;
        }
        else if (transform.position.y >= 10)
        {
            randomMove.y = -randomMove.y;
        }
        else if (transform.position.y <= -10)
        {
            randomMove.y = -randomMove.y;
        }
    }
}
