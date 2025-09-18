using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject[] levelLand = new GameObject[5];
    public Rank[] ranks = new Rank[20];
    public int score = 0;
    public int money = 0;
    public int life = 3;
    public string[] deadName;

    public GameObject GameUI;
    public Slider healthBar;
    public Slider energyBar;
    public Text scoreText;
    public Text moneyText;
    public Text lifeText;
    public Text rankText;
    public Text nextRankText;
    public Text deadText;
    public GameObject gameOverScreen;
    [HideInInspector] public bool gameOver;
    [HideInInspector] public bool shop;
    public GameObject fadeScreen;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindObjectOfType<GameManager>().mode == GameManager.Mode.Survival)
        {
            Instantiate(levelLand[Random.Range(0, levelLand.Length)], Vector3.zero, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        if (moneyText != null)
        {
            moneyText.text = "Money: " + money;
        }
        if (lifeText != null)
        {
            lifeText.text = "Life: " + life;
        }
        if (life <= 0 && gameOver)
        {
            GameObject.FindObjectOfType<SoundClip>().AudioStop();
        }
        RankScore();
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        money += newScoreValue;
    }

    public void DeadLives()
    {
        life--;
        StartCoroutine(DeadText());
        if (life <= 0 && !gameOver)
        {
            GameObject.FindObjectOfType<SoundClip>().wastedFlashAudio.Play();
            StartCoroutine(GameOverScreen());
        }
    }

    void RankScore()
    {
        for (int i = 0; i < ranks.Length; i++)
        {
            if (score < ranks[1].rankScore)
            {
                rankText.text = "Rank: " + ranks[0].rankName;
                nextRankText.text = "Next Rank at: " + ranks[1].rankScore;
            }
            else if (i < ranks.Length - 1 && score >= ranks[i].rankScore && score < ranks[i + 1].rankScore)
            {
                rankText.text = "Rank: " + ranks[i].rankName;
                nextRankText.text = "Next Rank at: " + ranks[i + 1].rankScore;
            }
            else if (score >= ranks[ranks.Length - 1].rankScore)
            {
                rankText.text = "Rank: " + ranks[i].rankName;
                nextRankText.text = "Next Rank at: Infinity";
            }
        }
    }

    IEnumerator DeadText()
    {
        deadText.text = deadName[Random.Range(0, deadName.Length)] + "!";
        yield return new WaitForSeconds(1);
        deadText.text = "";
    }

    IEnumerator GameOverScreen()
    {
        GameUI.SetActive(false);
        Time.timeScale = 0.25f;
        gameOver = true;
        GameObject.FindObjectOfType<SoundClip>().wastedAudio.Play();
        yield return new WaitForSeconds(0.5f);
        GameObject.FindObjectOfType<SoundClip>().wastedTextAudio.Play();
        gameOverScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
        fadeScreen.SetActive(true);
        yield return new WaitForSeconds(1.25f);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
