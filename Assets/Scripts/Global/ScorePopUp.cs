using TMPro;
using UnityEngine;

namespace Global
{
    public class ScorePopUp : MonoBehaviour
    {
        [SerializeField] private GameObject _scorePopUpPrefab;
        [SerializeField] private float _scorePopUpDuration = 0.5f;
        [SerializeField] private Vector2 _scorePositionOffset = new(0, -1f);
        [SerializeField, Tooltip("Range in which the text can rotate")] private float _randomRotationRange = 30f;
        [SerializeField] private float _textFontSize = 20f;
        [SerializeField] private Color _positiveScoreColor = Color.green;
        [SerializeField] private Color _negativeScoreColor = Color.red;

        private void Awake()
        {
            if (_scorePopUpPrefab == null)
            {
                Debug.LogError("ScorePopUp prefab is not assigned in the inspector!");
            }
            else
            {
                _scorePopUpPrefab.GetComponent<TMP_Text>().fontSize = _textFontSize;
            }
        }

        public void ShowPopUp(Vector2 popUpPosition, float scoreAmount)
        {

            GameObject popUp =
            Instantiate(_scorePopUpPrefab, popUpPosition + _scorePositionOffset,
            Quaternion.Euler(0, 0, Random.Range(-_randomRotationRange, _randomRotationRange)));

            popUp.GetComponent<TMP_Text>().color = (scoreAmount < 0) ? Color.red : Color.green;
            popUp.GetComponent<TMP_Text>().text = (scoreAmount < 0) ? scoreAmount.ToString() : "+" + scoreAmount.ToString();

            Destroy(popUp, _scorePopUpDuration);
        }
    }
}


