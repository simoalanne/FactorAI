using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace Tutorial
{
    public class MinigameTutorialMenu : MonoBehaviour
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

            if (Global.GameManager.Instance.CurrentProduct == "Product1" || Global.GameManager.Instance == null)
            {
                GameObject.Find("ValueText1").GetComponent<TMP_Text>().text =
                FindObjectOfType<Minigame1.GameStatsManager>().MinCompletedProducts.ToString();
                foreach (var product in FindObjectOfType<Minigame1.ObjectSpawner>().Product1Objects)
                {
                    objectSprites.Add(product.ObjectToSpawn.GetComponent<SpriteRenderer>().sprite);
                }

                GameObject.Find("TargetProduct").GetComponent<Image>().sprite = objectSprites[6]; // => Shovel

                GameObject.Find("Rule1").transform.Find("Product1").GetComponent<Image>().sprite = objectSprites[0]; // => Metal
                GameObject.Find("Rule1").transform.Find("Product2").GetComponent<Image>().sprite = objectSprites[0]; // => Metal
                GameObject.Find("Rule1").transform.Find("ResultProduct").GetComponent<Image>().sprite = objectSprites[2]; // => Bar

                GameObject.Find("Rule2").transform.Find("Product1").GetComponent<Image>().sprite = objectSprites[1]; // => Wood
                GameObject.Find("Rule2").transform.Find("Product2").GetComponent<Image>().sprite = objectSprites[1]; // => Wood
                GameObject.Find("Rule2").transform.Find("ResultProduct").GetComponent<Image>().sprite = objectSprites[3]; // => Plank

                GameObject.Find("Rule3").transform.Find("Product1").GetComponent<Image>().sprite = objectSprites[2]; // => Bar
                GameObject.Find("Rule3").transform.Find("Product2").GetComponent<Image>().sprite = objectSprites[2]; // => Bar
                GameObject.Find("Rule3").transform.Find("ResultProduct").GetComponent<Image>().sprite = objectSprites[5]; // => Head

                GameObject.Find("Rule4").transform.Find("Product1").GetComponent<Image>().sprite = objectSprites[3]; // => Plank
                GameObject.Find("Rule4").transform.Find("Product2").GetComponent<Image>().sprite = objectSprites[3]; // => Plank
                GameObject.Find("Rule4").transform.Find("ResultProduct").GetComponent<Image>().sprite = objectSprites[4]; // => Handle

                GameObject.Find("Rule5").transform.Find("Product1").GetComponent<Image>().sprite = objectSprites[4]; // => Handle
                GameObject.Find("Rule5").transform.Find("Product2").GetComponent<Image>().sprite = objectSprites[5]; // => Head

                GameObject.Find("Rule5").transform.Find("ResultProduct").GetComponent<Image>().sprite = objectSprites[6]; // => Shovel
                _tutorialMenu.SetActive(false);
            }

            else if (Global.GameManager.Instance.CurrentProduct == "Product2")
            {
                GameObject.Find("ValueText1").GetComponent<TMP_Text>().text =
                (FindObjectOfType<Minigame1.GameStatsManager>().MinCompletedProducts + 4).ToString();
                foreach (var product in FindObjectOfType<Minigame1.ObjectSpawner>().Product2Objects)
                {
                    objectSprites.Add(product.ObjectToSpawn.GetComponent<SpriteRenderer>().sprite);
                }

                GameObject.Find("TargetProduct").GetComponent<Image>().sprite = objectSprites[4];

                GameObject.Find("Rule1").transform.Find("Product1").GetComponent<Image>().sprite = objectSprites[6]; // => Sheep
                GameObject.Find("Rule1").transform.Find("Product2").GetComponent<Image>().sprite = objectSprites[5]; // => Shears
                GameObject.Find("Rule1").transform.Find("ResultProduct").GetComponent<Image>().sprite = objectSprites[10]; // => Wool

                GameObject.Find("Rule2").transform.Find("Product1").GetComponent<Image>().sprite = objectSprites[10]; // => Wool 
                GameObject.Find("Rule2").transform.Find("Product2").GetComponent<Image>().sprite = objectSprites[7]; // => Spinning Wheel
                GameObject.Find("Rule2").transform.Find("ResultProduct").GetComponent<Image>().sprite = objectSprites[9]; // => Twine

                GameObject.Find("Rule3").transform.Find("Product1").GetComponent<Image>().sprite = objectSprites[1]; // => Chicken
                GameObject.Find("Rule3").transform.Find("Product2").GetComponent<Image>().sprite = objectSprites[2]; // => Egg
                GameObject.Find("Rule3").transform.Find("ResultProduct").GetComponent<Image>().sprite = objectSprites[0]; // => Chick

                GameObject.Find("Rule4").transform.Find("Product1").GetComponent<Image>().sprite = objectSprites[0]; // => Chick
                GameObject.Find("Rule4").transform.Find("Product2").GetComponent<Image>().sprite = objectSprites[8]; // => Tweezers
                GameObject.Find("Rule4").transform.Find("ResultProduct").GetComponent<Image>().sprite = objectSprites[3]; // => Feather

                GameObject.Find("Rule5").transform.Find("Product1").GetComponent<Image>().sprite = objectSprites[3]; // => Feather
                GameObject.Find("Rule5").transform.Find("Product2").GetComponent<Image>().sprite = objectSprites[9]; // => Twine
                GameObject.Find("Rule5").transform.Find("ResultProduct").GetComponent<Image>().sprite = objectSprites[4]; // => Pillow
            }

            _tutorialMenu.SetActive(false);
        }

        private void Minigame2Tutorial()
        {
            GameObject.Find("ValueText2").GetComponent<TMP_Text>().text =
            FindObjectOfType<Minigame2.GameStatsManager>().HowManyForWin.ToString();

            if (Global.GameManager.Instance.CurrentProduct == "Product1" || Global.GameManager.Instance == null)
            {
                GameObject.Find("ProductImage2").GetComponent<Image>().sprite = FindObjectOfType<Minigame2.ManageSprites>().SpawnedObjects[0].GetComponent<SpriteRenderer>().sprite;
                GameObject.Find("Rule3Text").GetComponent<TMP_Text>().text = "X X X X X";
            }
            else if (Global.GameManager.Instance.CurrentProduct == "Product2")
            {
                GameObject.Find("ProductImage2").GetComponent<Image>().sprite = FindObjectOfType<Minigame2.ManageSprites>().SpawnedObjects2[0].GetComponent<SpriteRenderer>().sprite;
                GameObject.Find("Rule3Text").GetComponent<TMP_Text>().text = "X X X"; // Less fails allowed
            }
            
            _tutorialMenu.SetActive(false);
        }

        public void OpenTutorial()
        {
            Time.timeScale = 0;
            _openTutorialButton.gameObject.SetActive(false);
            _tutorialMenu.SetActive(true);
        }

        public void CloseTutorial()
        {
            _openTutorialButton.gameObject.SetActive(true);
            _tutorialMenu.SetActive(false);
            StartCoroutine(ResumeGame());
        }

        private IEnumerator ResumeGame()
        {
            yield return new WaitForSecondsRealtime(0.5f);
            Time.timeScale = 1;
        }
    }
}
