using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed;
    public float slowSpeed;
    public bool isHoming;
    public string targetName;

    private Transform target;
    private Rigidbody rb;
    private GameObject[] enemys;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (enemys == null)
        {
            enemys = GameObject.FindGameObjectsWithTag("Enemy");
        }
        else if (enemys != null)
        {
            enemys = GameObject.FindGameObjectsWithTag("Enemy");
        }
        if (isHoming && enemys.Length >= 1)
        {
            target = GameObject.FindGameObjectWithTag(targetName).transform;
        }
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * 60);
        }
        if (speed > slowSpeed)
        {
            speed -= Time.deltaTime * 50;
        }
        if (speed < slowSpeed)
        {
            speed = slowSpeed;
        }
        rb.velocity = transform.forward * speed;
    }
}