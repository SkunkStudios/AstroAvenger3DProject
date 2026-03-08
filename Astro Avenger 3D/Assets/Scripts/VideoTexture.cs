using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoTexture : MonoBehaviour
{
	public Texture2D[] movieFrame;
    public bool isLoop;
    public int targetFPS = 20;

    private Renderer renderer;
	private int movieCount;

    void Start ()
	{
        renderer = GetComponent<Renderer>();
        StartCoroutine(FrameRate());
    }

    void Update ()
	{
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

    IEnumerator FrameRate()
    {
        yield return new WaitForSeconds(1f / targetFPS);
        while (true)
        {
            if (movieCount < movieFrame.Length)
            {
                movieCount++;
            }
            yield return new WaitForSeconds(1f / targetFPS);
        }
    }
}
