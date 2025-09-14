using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoTexture : MonoBehaviour
{
	public Texture2D[] movieFrame;
    public bool isLoop;
    public float maxTime = 0.03f;

    private Renderer renderer;
	private int movieCount;
    private float time;

    void Start ()
	{
        renderer = GetComponent<Renderer>();
    }

    void Update ()
	{
        time += Time.deltaTime;
        if (time >= maxTime)
        {
            movieCount++;
            time = 0;
        }
        if (movieCount >= movieFrame.Length)
        {
            if (isLoop)
            {
                movieCount = 0;
            }
            else
            {
                renderer.enabled = false;
            }
        }
        else if (movieCount < movieFrame.Length)
        {
            renderer.material.mainTexture = movieFrame[movieCount];
        }
    }
}
