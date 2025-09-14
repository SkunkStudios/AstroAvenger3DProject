using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public enum TargetType { Off, Destroyer, Enemy }
    public TargetType targetTypes;
    public float speed;
    public float slowSpeed;
    public bool isHoming;

    private Transform target;
    private Rigidbody rb;
    private GameObject[] enemys;
    private GameObject[] destroyers;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (destroyers == null || destroyers != null)
        {
            destroyers = GameObject.FindGameObjectsWithTag("Destroyer");
        }
        if (enemys == null || enemys != null)
        {
            enemys = GameObject.FindGameObjectsWithTag("Enemy");
        }
        if (isHoming && destroyers.Length >= 1 && targetTypes == TargetType.Enemy)
        {
            target = GameObject.FindGameObjectWithTag("Destroyer").transform;
        }
        if (isHoming && enemys.Length >= 1 && targetTypes == TargetType.Destroyer)
        {
            target = GameObject.FindGameObjectWithTag("Enemy").transform;
        }
        if (speed > slowSpeed)
        {
            speed -= Time.deltaTime * 50;
        }
        if (speed < slowSpeed)
        {
            speed = slowSpeed;
        }
        if (GetComponent<Laser>().damageHit >= GetComponent<Laser>().maxHit)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            if (target != null)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * 60);
            }
            rb.velocity = transform.forward * speed;
        }
    }
}