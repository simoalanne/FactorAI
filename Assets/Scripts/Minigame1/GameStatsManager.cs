using Global;
using UnityEngine;

public class GameStatsManager : MonoBehaviour
{

    [SerializeField] float _miniGameLengthInSeconds = 60.0f;
    [SerializeField] int _minCompletedProducts = 2;

    private OnMinigameEnd _onMinigameEnd;
    private string _minigameTime;
    private float _score;
    private int _completedProducts;
    private bool _gameActive = true;
    private ScorePopUp _scorePopUp;

    public string MinigameTime => _minigameTime;
    public float Score => _score;
    public int MinCompletedProducts => _minCompletedProducts;
    public int CompletedProducts => _completedProducts;

    void Start()
    {
        _onMinigameEnd = GetComponent<OnMinigameEnd>();
        _scorePopUp = GetComponent<ScorePopUp>();

    }

    void Update()
    {
        if (_miniGameLengthInSeconds <= 0f && _gameActive)
        {
            _gameActive = false;
            CheckGameEnd();
            return;
        }

        _miniGameLengthInSeconds -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(_miniGameLengthInSeconds / 60);
        int seconds = Mathf.FloorToInt(_miniGameLengthInSeconds % 60);
        float fraction = _miniGameLengthInSeconds * 100 % 100;
        _minigameTime = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, Mathf.FloorToInt(fraction));
    }

    private void CheckGameEnd()
    {
        if (_completedProducts >= _minCompletedProducts)
        {
            _onMinigameEnd.OnGameWon(_score);
        }
        else
        {
            _onMinigameEnd.OnGameLost();
        }
    }

    public void IncreaseScore(float score, Transform transform)
    {
        _score += score;
        _scorePopUp.ShowPopUp(transform.position, score);
    }

    public void IncreaseCompletedProducts()
    {
        _completedProducts++;
    }
}
