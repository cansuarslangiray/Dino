using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private float initialGameSpeed = 5f;
    [SerializeField] private float gameSpeedIncrease = 0.1f;
    public float GameSpeed { get; private set; }

    private Player _player;
    private Spawner _spawner;
    private AnimatedScripts _animatedScripts;

    public static bool IsDead;
    [SerializeField] private TextMeshProUGUI gameOverTex;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private Button retryButton;
    [SerializeField] private Sprite playerIdle;

    private float _score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _spawner = FindObjectOfType<Spawner>();
        _animatedScripts = _player.GetComponent<AnimatedScripts>();
        NewGame();
    }

    public void NewGame()
    {
        _player.GetComponent<SpriteRenderer>().sprite = playerIdle;
        IsDead = false;
        AnimatedScripts playerAnimatedScripts = _player.GetComponent<AnimatedScripts>();
        playerAnimatedScripts.SetIsAnimating(true);
        _score = 0;
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        GameSpeed = initialGameSpeed;
        enabled = true;
        _player.enabled = true;
        _animatedScripts.enabled = true;
        _spawner.gameObject.SetActive(true);
        gameOverTex.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        Debug.Log(IsDead);
        UpdateHighScore();
    }

    private void Update()
    {
        GameSpeed += gameSpeedIncrease * Time.deltaTime;
        _score += GameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(_score).ToString("D5");
    }

    public void GameOver()
    {
        GameSpeed = 0f;
        enabled = false;
        _player.enabled = false;
        _animatedScripts.enabled = false;
        _spawner.gameObject.SetActive(false);
        gameOverTex.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        AnimatedScripts playerAnimatedScripts = _player.GetComponent<AnimatedScripts>();
        playerAnimatedScripts.SetIsAnimating(false);
        UpdateHighScore();
    }

    private void UpdateHighScore()
    {
        float highScore = PlayerPrefs.GetFloat("highScore", 0);

        if (_score > highScore)
        {
            highScore = _score;
            PlayerPrefs.SetFloat("highScore",highScore);
        }

        highScoreText.text = Mathf.FloorToInt(highScore).ToString("D5");
    }
}