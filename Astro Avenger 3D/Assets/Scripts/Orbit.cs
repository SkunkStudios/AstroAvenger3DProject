using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
	public Vector3 axis;
	public bool isAxisX;
	public bool isOrbit;

    void Update ()
	{
        if (isOrbit)
		{
            transform.Rotate(axis * Time.deltaTime);
        }
        else
		{
            if (isAxisX)
            {
                transform.Rotate(Vector3.right * Time.deltaTime * 2);
            }
            else
            {
                transform.Rotate(-Vector3.forward * Time.deltaTime * 3);
            }
        }
    }
}
