using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMove : MonoBehaviour
{
    public float speed;
    public bool isMove;

    void Update ()
	{
        if (isMove)
        {
            transform.Translate(0, 0, -speed * Time.deltaTime, Space.World);
        }
        else
        {
            transform.Translate(-transform.forward * speed * Time.deltaTime);
        }
    }
}
