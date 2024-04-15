using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


namespace Tutorial
{
    public class MinigameTutorial : MonoBehaviour
    {
        [SerializeField] private Button _openTutorialButton;
        [SerializeField] private GameObject _minigame1Tutorial;
        [SerializeField] private GameObject _minigame2Tutorial;
        private GameObject _tutorialMenu;

        void Awake()
        {
            if (SceneManager.GetActiveScene().name == "Minigame1")
            {
                _tutorialMenu = _minigame1Tutorial;
                _tutorialMenu.SetActive(true);
                Minigame1Tutorial();

            }
            else if (SceneManager.GetActiveScene().name == "Minigame2")
            {
                _tutorialMenu = _minigame2Tutorial;
                _tutorialMenu.SetActive(true);
                Minigame2Tutorial();
            }
        }
        private void Minigame1Tutorial()
        {
            List<Sprite> objectSprites = new();

            foreach (var product in FindObjectOfType<Minigame1.ObjectSpawner>().Product1Objects)
            {
                objectSprites.Add(product.ObjectToSpawn.GetComponent<SpriteRenderer>().sprite);
            }

            GameObject.Find("ValueText1").GetComponent<TMP_Text>().text =
            FindObjectOfType<Minigame1.GameStatsManager>().MinCompletedProducts.ToString();

            GameObject.Find("ProductImage1").GetComponent<Image>().sprite = objectSprites[6];

            GameObject.Find("Rule1").transform.Find("Product1").GetComponent<Image>().sprite = objectSprites[0];
            GameObject.Find("Rule1").transform.Find("Product2").GetComponent<Image>().sprite = objectSprites[0];
            GameObject.Find("Rule1").transform.Find("ResultProduct").GetComponent<Image>().sprite = objectSprites[2];

            GameObject.Find("Rule2").transform.Find("Product1").GetComponent<Image>().sprite = objectSprites[1];
            GameObject.Find("Rule2").transform.Find("Product2").GetComponent<Image>().sprite = objectSprites[1];
            GameObject.Find("Rule2").transform.Find("ResultProduct").GetComponent<Image>().sprite = objectSprites[3];

            GameObject.Find("Rule3").transform.Find("Product1").GetComponent<Image>().sprite = objectSprites[2];
            GameObject.Find("Rule3").transform.Find("Product2").GetComponent<Image>().sprite = objectSprites[2];
            GameObject.Find("Rule3").transform.Find("ResultProduct").GetComponent<Image>().sprite = objectSprites[5];

            GameObject.Find("Rule4").transform.Find("Product1").GetComponent<Image>().sprite = objectSprites[3];
            GameObject.Find("Rule4").transform.Find("Product2").GetComponent<Image>().sprite = objectSprites[3];
            GameObject.Find("Rule4").transform.Find("ResultProduct").GetComponent<Image>().sprite = objectSprites[4];

            GameObject.Find("Rule5").transform.Find("Product1").GetComponent<Image>().sprite = objectSprites[4];
            GameObject.Find("Rule5").transform.Find("Product2").GetComponent<Image>().sprite = objectSprites[5];
            GameObject.Find("Rule5").transform.Find("ResultProduct").GetComponent<Image>().sprite = objectSprites[6];
            _tutorialMenu.SetActive(false);
        }

        private void Minigame2Tutorial()
        {
            GameObject.Find("ValueText2").GetComponent<TMP_Text>().text =
            FindObjectOfType<Minigame2.GameStatsManager>().HowManyForWin.ToString();

            GameObject.Find("ProductImage2").GetComponent<Image>().sprite =
            FindObjectOfType<Minigame2.ManageSprites>().SpawnedObjects[0].GetComponent<SpriteRenderer>().sprite;
            _tutorialMenu.SetActive(false);
        }

        public void OpenTutorial()
        {
            Time.timeScale = 0;
            _openTutorialButton.gameObject.SetActive(false);
            _tutorialMenu.gameObject.SetActive(true);
        }

        public void CloseTutorial()
        {
            _tutorialMenu.gameObject.SetActive(false);
            _openTutorialButton.gameObject.SetActive(true);
            StartCoroutine(ResumeGame());
        }

        private IEnumerator ResumeGame()
        {
            yield return new WaitForSecondsRealtime(0.5f);
            Time.timeScale = 1;
        }
    }
}
