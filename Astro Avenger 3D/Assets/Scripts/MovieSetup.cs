using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieSetup : MonoBehaviour
{
	public Texture2D[] movieFrame;
    public bool isLoop;

    private Renderer renderer;
	private int movieCount;
    private float time;

    void Start ()
	{
        renderer = GetComponent<Renderer>();
    }

    void Update ()
	{
        time += 0.01f;
        if (time >= 0.01)
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
