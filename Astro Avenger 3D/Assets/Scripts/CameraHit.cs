using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHit : MonoBehaviour
{
    private Camera cam;
	private float viewHit;

	// Use this for initialization
	void Start ()
	{
        cam = GetComponent<Camera>();
		viewHit = cam.fieldOfView;
    }

    // Update is called once per frame
    void Update ()
	{
        cam.fieldOfView = viewHit - GameManager.zoomHit * Mathf.Sin(GameManager.zoomHit * 3) * 0.25f;
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 0, -GameManager.zoomRotHit * Mathf.Sin(GameManager.zoomRotHit * 3) * 0.25f);
	}

    public void Hit(float hit)
    {
        GameManager.zoomHit += hit;
        if (hit == 3 && transform.eulerAngles.z == 0 || transform.eulerAngles.z != 0)
        {
            GameManager.zoomRotHit = GameManager.zoomHit;
        }
    }
}
