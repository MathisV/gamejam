using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UIController : MonoBehaviour
{
    public GameObject gameOverPanel;

    public Player player;

    public Tilemap tilemap;

    public int maxScore;

    public int currentScore;

    public TMP_Text maxScoreText;

    public TMP_Text currentScoreText;

    public TMP_Text scoreText;

    private (float x, float y, float z) startTransform;

    private (float x, float y, float z) PlayerTransform;

    public void Start()
    {
        maxScore = 0;

        //Get max score from player prefs
        if (PlayerPrefs.HasKey("maxScore"))
        {
            maxScore = PlayerPrefs.GetInt("maxScore");
        }
        else
        {
            PlayerPrefs.SetInt("maxScore", maxScore);
        }
        gameOverPanel.SetActive(false);

        //Get transform of the gameobject one time
        startTransform =
            (transform.position.x, transform.position.y, transform.position.z);
        PlayerTransform =
            (
                player.transform.position.x,
                player.transform.position.y,
                player.transform.position.z
            );
    }

    public void Update()
    {
        currentScore = (int) player.transform.position.x;
        scoreText.text = currentScore - (int)PlayerTransform.x + "m";
    }

    public void dieUi()
    {
        player.isDead = true;
        if (maxScore < currentScore)
        {
            maxScore = currentScore - (int)PlayerTransform.x;
            PlayerPrefs.SetInt("maxScore", maxScore);
        }
        gameOverPanel.SetActive(true);
        maxScoreText.text = maxScore + "m";
        currentScoreText.text = currentScore - (int)PlayerTransform.x + "m";
    }

    public void restartGame()
    {
        // for each deletable tag delete 
        GameObject[] deletableObjects = GameObject.FindGameObjectsWithTag("Deletable");
        foreach (GameObject deletableObject in deletableObjects)
        {
            Destroy(deletableObject);
        }
        gameOverPanel.SetActive(false);
        player.isDead = false;
        transform.position =
            new Vector3(startTransform.x, startTransform.y, startTransform.z);
        player.transform.position =
            new Vector3(PlayerTransform.x, 6, PlayerTransform.z);
    }

    //on trigger enter 2D pour le joueu
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            dieUi();
        }
    }
}
