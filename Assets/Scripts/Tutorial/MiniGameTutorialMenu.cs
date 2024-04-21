using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Global;

namespace Tutorial
{
    public class MinigameTutorialMenu : MonoBehaviour
    {
        [SerializeField] private Button _openTutorialButton;
        [SerializeField] private GameObject _minigame1Tutorial;
        [SerializeField] private GameObject _minigame2Tutorial;
        private GameObject _tutorialMenu;
        private RaycastManager _raycastManager;

        void Awake()
        {
            _raycastManager = GetComponent<RaycastManager>();

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

            if (GameManager.Instance.CurrentProduct == "Product1" || Global.GameManager.Instance == null)
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

            else if (GameManager.Instance.CurrentProduct == "Product2")
            {
                GameObject.Find("ValueText1").GetComponent<TMP_Text>().text =
                (FindObjectOfType<Minigame1.GameStatsManager>().MinCompletedProducts * 2).ToString(); // Double the amount of products needed for Product2
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
            GameObject.Find("ValueText2").GetComponent<TMP_Text>().text = FindObjectOfType<Minigame2.GameStatsManager>().HowManyForWin.ToString();

            if (Global.GameManager.Instance.CurrentProduct == "Product1" || Global.GameManager.Instance == null)
            {
                GameObject.Find("TargetProduct2").GetComponent<Image>().sprite = FindObjectOfType<Minigame2.ManageSprites>().SpawnedObjects[0].GetComponent<SpriteRenderer>().sprite;
                GameObject.Find("Rule3Text").GetComponent<TMP_Text>().text = "<color=#C42C36FF>x x x x x</color><color=#DBE0E7FF> -> fail</color>"; // x chars are redish and "-> fail" is grayish 
            }
            else if (Global.GameManager.Instance.CurrentProduct == "Product2")
            {
                GameObject.Find("TargetProduct2").GetComponent<Image>().sprite = FindObjectOfType<Minigame2.ManageSprites>().SpawnedObjects2[0].GetComponent<SpriteRenderer>().sprite;
                GameObject.Find("Rule3Text").GetComponent<TMP_Text>().text = "<color=#C42C36FF>x x x</color><color=#DBE0E7FF> -> fail</color>"; // less fails allowed for Product2
            }

            // Find Rule1Text and Rule2Text
            TMP_Text rule1Text = GameObject.Find("Rule1Text").GetComponent<TMP_Text>();
            TMP_Text rule2Text = GameObject.Find("Rule2Text").GetComponent<TMP_Text>();

            // Set their color
            rule1Text.text = SetLastCharacterColor(rule1Text.text, "#DBE0E7FF", "#C42C36FF");
            rule2Text.text = SetLastCharacterColor(rule2Text.text, "#DBE0E7FF", "#C42C36FF");

            _tutorialMenu.SetActive(false);
        }

        private string SetLastCharacterColor(string text, string color1, string color2)
        {
            Debug.Log(text);
            print("Method called");
            if (string.IsNullOrEmpty(text)) return text;

            string allButLast = $"<color={color1}>{text[..^1]}</color>"; // Get all but the last character and wrap them in color1

            string last = $"<color={color2}>{text[^1]}</color>"; // Get the last character and wrap it in color2

            // Combine the colored parts
            return allButLast + last;
        }

        public void OpenTutorial()
        {
            if (_tutorialMenu.activeSelf)
            {
                _raycastManager.EnableOtherCanvasesRaycasting();
                _tutorialMenu.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                _raycastManager.DisableOtherCanvasesRaycasting();
                Time.timeScale = 0;
                _tutorialMenu.SetActive(true);
            }
        }

        public void CloseTutorial()
        {
            _raycastManager.EnableOtherCanvasesRaycasting();
            _tutorialMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
