﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atmosphere : MonoBehaviour
{
    public bool isLook;
    public bool isSlide;

    private Transform cam;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void LateUpdate()
    {
        if (isLook)
        {
            if (isSlide)
            {
                transform.LookAt(transform.position + cam.forward);
            }
            else
            {
                transform.LookAt(transform.position + cam.up);
            }
        }
        else
        {
            transform.rotation = Quaternion.Euler(50, 180, 180);
        }
    }
}
