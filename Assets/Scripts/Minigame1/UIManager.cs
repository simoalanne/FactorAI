using UnityEngine;
using TMPro;
using Global;
using UnityEngine.UI;

namespace Minigame1
{
    public class UIManager : MonoBehaviour

    {
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _completedProdcutsText;

        private GameStatsManager _gameStatsManager;

        void Awake()
        {
            _gameStatsManager = GetComponent<GameStatsManager>();

            if (GameManager.Instance.CurrentProduct == "Product1" || GameManager.Instance == null)
            {
                GameObject.Find("ProductImage").GetComponent<Image>().sprite = FindObjectOfType<ObjectSpawner>().Product1Objects[6].ObjectToSpawn.GetComponent<SpriteRenderer>().sprite;
            }
            else if (GameManager.Instance.CurrentProduct == "Product2")
            {
                GameObject.Find("ProductImage").GetComponent<Image>().sprite = FindObjectOfType<ObjectSpawner>().Product2Objects[4].ObjectToSpawn.GetComponent<SpriteRenderer>().sprite;
            }

   
        }

        void Update()
        {
            _timerText.text = _gameStatsManager.MinigameTime;
            _scoreText.text = _gameStatsManager.Score.ToString();
            _completedProdcutsText.text = _gameStatsManager.CompletedProducts + "/" + _gameStatsManager.MinCompletedProducts;
        }
    }
}