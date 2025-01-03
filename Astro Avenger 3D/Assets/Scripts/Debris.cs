using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    public GameObject explore;
    public GameObject part;
    public float waitTime;
    public bool isChildren;
    public float camHit;
    public float tumble;

    private CameraHit cameraHit;
    private SoundClip soundClip;
    private MeshRenderer meshRenderer;
    private MeshRenderer[] meshRenderers;
    private ParticleSystem fireTail;
    private Rigidbody rb;

    void Start ()
	{
        if (isChildren)
        {
            meshRenderers = GetComponentsInChildren<MeshRenderer>();
        }
        else
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }
        cameraHit = GameObject.FindObjectOfType<CameraHit>();
        soundClip = GameObject.FindObjectOfType<SoundClip>();
        fireTail = GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = Random.insideUnitSphere * tumble;
        rb.angularVelocity = Random.insideUnitSphere * tumble;
        StartCoroutine(WaitExplore(waitTime));
    }

    IEnumerator WaitExplore(float time)
    {
        yield return new WaitForSeconds(time);
        cameraHit.Hit(camHit);
        soundClip.PlayExplosion();
        if (isChildren)
        {
            foreach (MeshRenderer meshRenderer in meshRenderers)
            {
                meshRenderer.enabled = false;
            }
        }
        else
        {
            meshRenderer.enabled = false;
        }
        fireTail.Stop();
        Instantiate(explore, transform.position, Quaternion.identity);
        if (part != null)
        {
            Instantiate(part, transform.position, transform.rotation);
        }
        Destroy(gameObject, 1.28f);
    }
}
