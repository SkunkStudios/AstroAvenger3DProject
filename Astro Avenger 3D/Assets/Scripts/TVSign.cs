using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TVSign : MonoBehaviour
{
    public Renderer screen;

    private GameManager gameManager;
    private Texture2D[] screens;
    private int infoIndex;

    void Start ()
	{
        gameManager = GameObject.FindObjectOfType<GameManager>();
        screens = Resources.LoadAll<Texture2D>("TVScreen");
        ScreenUI();
    }

    public void ScreenUI()
    {
        if (gameManager.urlInfoScreen.Length == 0)
        {
            infoIndex = 0;
        }
        else if (gameManager.urlInfoScreen.Length > 0)
        {
            infoIndex = Random.Range(0, 2);
        }
        if (infoIndex == 0)
        {
            screen.material.mainTexture = screens[Random.Range(0, screens.Length)];
        }
        else if (infoIndex == 1)
        {
            StartCoroutine(DownloadImage(gameManager.urlInfoScreen[Random.Range(0, gameManager.urlInfoScreen.Length)]));
        }
    }

    IEnumerator DownloadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            screen.material.mainTexture = screens[Random.Range(0, screens.Length)];
        }
        else
        {
            screen.material.mainTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
    }
}
