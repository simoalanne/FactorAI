using System.Collections;
using System.Collections.Generic;
using Global;
using UnityEngine;

public class GameStatsManager : MonoBehaviour
{

    [SerializeField] float _miniGameLengthInSeconds = 60.0f;
    [SerializeField] int _minCompletedProducts = 2;

    private OnMinigameEnd _onMinigameEnd;
    private string _minigameTime;
    private int _score;
    private int _completedProducts;

    public string MinigameTime => _minigameTime;
    public int Score => _score;
    public int MinCompletedProducts => _minCompletedProducts;
    public int CompletedProducts => _completedProducts;

    void Start()
    {
        _onMinigameEnd = GetComponent<OnMinigameEnd>();

    }

    void Update()
    {
        _miniGameLengthInSeconds -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(_miniGameLengthInSeconds / 60);
        int seconds = Mathf.FloorToInt(_miniGameLengthInSeconds % 60);
        float fraction = _miniGameLengthInSeconds * 100 % 100;
        _minigameTime = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, Mathf.FloorToInt(fraction));

        if (_miniGameLengthInSeconds <= 0.0f && _completedProducts < _minCompletedProducts)
        {
            _minigameTime = "00:00:00";
            _onMinigameEnd.OnGameLost();
        }

        else if (_miniGameLengthInSeconds <= 0.0f && _completedProducts >= _minCompletedProducts)
        {
            _onMinigameEnd.OnGameWon(_score);
        }
    }

    public void IncreaseScore(int score)
    {
        _score += score;
    }

    public void IncreaseCompletedProducts()
    {
        _completedProducts++;
    }
}
