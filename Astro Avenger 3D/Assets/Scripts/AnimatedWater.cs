using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedWater : MonoBehaviour
{
	public float speedX = 0.1f;
    public float speedY = 0.1f;
    private float curX;
    private float curY;

    // Use this for initialization
    void Start ()
	{
        if (GetComponent<Renderer>() != null)
        {
            curX = GetComponent<Renderer>().material.mainTextureOffset.x;
            curY = GetComponent<Renderer>().material.mainTextureOffset.y;
        }
        if (GetComponent<LineRenderer>() != null)
        {
            curX = GetComponent<LineRenderer>().material.mainTextureOffset.x;
            curY = GetComponent<LineRenderer>().material.mainTextureOffset.y;
        }
    }

    void FixedUpdate ()
	{
        curX += Time.deltaTime * speedX;
        curY += Time.deltaTime * speedY;
        if (GetComponent<Renderer>() != null)
        {
            GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(curX, curY));
        }
        if (GetComponent<LineRenderer>() != null)
        {
            GetComponent<LineRenderer>().material.SetTextureOffset("_MainTex", new Vector2(curX, curY));
        }
    }
}
