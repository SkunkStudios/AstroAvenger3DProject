using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
	public Transform target;
	public float speed;
	public Vector3 axis;

    void Update ()
	{
		transform.Rotate(axis * Time.deltaTime);
		if (target != null)
		{
            transform.RotateAround(target.transform.position, axis, speed * Time.deltaTime);
        }
    }
}
