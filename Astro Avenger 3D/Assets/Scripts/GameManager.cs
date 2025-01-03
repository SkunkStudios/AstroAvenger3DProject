﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[Header("360 No Scope")]
	public bool isMemeSounds;
    public GameObject dealWithIt;
    public GameObject frog;
    public GameObject[] randomVideo;
    public GameObject[] randomGIF;
    public GameObject sanic;

    public string[] urlInfoScreen;
    public static float zoomHit;

    void Start ()
	{
		
	}
	
	void Update ()
	{
        if (zoomHit > 0)
        {
            zoomHit -= Time.deltaTime * 10;
        }
        else if (zoomHit < 0)
        {
            zoomHit = 0;
        }
    }

    public void MemeClipsUI()
    {
        int randomMemes = Random.Range(0, 6);
        int randomSlide = Random.Range(0, 4);
        Vector3 pos;

        if (randomMemes == 1)
        {
            if (randomSlide == 0)
            {
                pos = new Vector3(-8.34f, -10, -500);
                Instantiate(dealWithIt, pos, Quaternion.Euler(0, 0, 0));
            }
            else if (randomSlide == 1)
            {
                pos = new Vector3(8.34f, -10, -500);
                Instantiate(dealWithIt, pos, Quaternion.Euler(0, 180, 0));
            }
            else if (randomSlide == 2)
            {
                pos = new Vector3(8.34f, 10, -500);
                Instantiate(dealWithIt, pos, Quaternion.Euler(0, 0, 180));
            }
            else if (randomSlide == 3)
            {
                pos = new Vector3(-8.34f, 10, -500);
                Instantiate(dealWithIt, pos, Quaternion.Euler(0, 180, 180));
            }
        }
        else if (randomMemes == 2)
        {
            if (randomSlide == 0)
            {
                pos = new Vector3(10.84f, -10, -500);
                Instantiate(frog, pos, Quaternion.Euler(0, 0, 0));
            }
            else if (randomSlide == 1)
            {
                pos = new Vector3(-10.84f, -10, -500);
                Instantiate(frog, pos, Quaternion.Euler(0, 180, 0));
            }
            else if (randomSlide == 2)
            {
                pos = new Vector3(-10.84f, 10, -500);
                Instantiate(frog, pos, Quaternion.Euler(0, 0, 180));
            }
            else if (randomSlide == 3)
            {
                pos = new Vector3(10.84f, 10, -500);
                Instantiate(frog, pos, Quaternion.Euler(0, 180, 180));
            }
        }
        else if (randomMemes == 3)
        {
            if (randomSlide == 0)
            {
                pos = new Vector3(8.34f, -10, -500);
                Instantiate(randomVideo[Random.Range(0, randomVideo.Length)], pos, Quaternion.Euler(0, 0, 0));
            }
            else if (randomSlide == 1)
            {
                pos = new Vector3(-8.34f, -10, -500);
                Instantiate(randomVideo[Random.Range(0, randomVideo.Length)], pos, Quaternion.Euler(0, 180, 0));
            }
            else if (randomSlide == 2)
            {
                pos = new Vector3(-8.34f, 10, -500);
                Instantiate(randomVideo[Random.Range(0, randomVideo.Length)], pos, Quaternion.Euler(0, 0, 180));
            }
            else if (randomSlide == 3)
            {
                pos = new Vector3(8.34f, 10, -500);
                Instantiate(randomVideo[Random.Range(0, randomVideo.Length)], pos, Quaternion.Euler(0, 180, 180));
            }
        }
        else if (randomMemes == 4)
        {
            if (randomSlide == 0)
            {
                pos = new Vector3(Random.Range(-10.84f, 10.84f), -10, -500);
                Instantiate(randomGIF[Random.Range(0, randomGIF.Length)], pos, Quaternion.Euler(0, 0, 0));
            }
            else if (randomSlide == 1)
            {
                pos = new Vector3(Random.Range(-10.84f, 10.84f), -10, -500);
                Instantiate(randomGIF[Random.Range(0, randomGIF.Length)], pos, Quaternion.Euler(0, 180, 0));
            }
            else if (randomSlide == 2)
            {
                pos = new Vector3(Random.Range(-10.84f, 10.84f), 10, -500);
                Instantiate(randomGIF[Random.Range(0, randomGIF.Length)], pos, Quaternion.Euler(0, 0, 180));
            }
            else if (randomSlide == 3)
            {
                pos = new Vector3(Random.Range(-10.84f, 10.84f), 10, -500);
                Instantiate(randomGIF[Random.Range(0, randomGIF.Length)], pos, Quaternion.Euler(0, 180, 180));
            }
        }
        else if (randomMemes == 5)
        {
            pos = new Vector3(Random.Range(-13, 13), Random.Range(-10, 10), -500);
            Instantiate(sanic, pos, Quaternion.identity);
        }
    }
}
