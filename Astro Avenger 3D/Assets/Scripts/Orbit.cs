using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
	public Transform target;
	public float speed;
	public Vector3 axis;
	public bool isAxisX;

    void Update ()
	{
        if (target == null)
		{
            if (isAxisX)
            {
                transform.Rotate(Vector3.right * Time.deltaTime * 5);
            }
            else
            {
                transform.Rotate(-Vector3.forward * Time.deltaTime * 3);
            }
        }
        else
		{
            transform.RotateAround(target.transform.position, axis, speed * Time.deltaTime);
        }
    }
}
