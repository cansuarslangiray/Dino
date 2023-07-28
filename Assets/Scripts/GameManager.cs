using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set; }
    [SerializeField] private float initialGameSpeed = 5f;
    [SerializeField] private float gameSpeedIncrease = 0.1f;
    public float GameSpeed { get; private set; }

    private Player _player;
    private Spawner _spawner;

    public static bool IsDead;

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
        NewGame();
    }

    private void NewGame()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }
        GameSpeed = initialGameSpeed;
        enabled = true;
        _player.enabled = true;
        _spawner.gameObject.SetActive(true);
    }

    private void Update()
    {
        GameSpeed += gameSpeedIncrease * Time.deltaTime;
    }

    public void GameOver()
    {
        GameSpeed = 0f;        
        enabled = false;
        _player.enabled=false;
        _spawner.gameObject.SetActive(false);
    }

    
}
