using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using CodeMonkey.Utils;

public class UIBitcoin : MonoBehaviour
{
    [Range (0, 100)] private int bitcoinPrice = 5;
    public Text bitcoinText;
    private int bitcoinPreviousPrice;
    public Text bitcoinPrevious;

    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;
    public List<int> valueList = new List<int>() { 5, 20, 33, 69, 60, 22, 17, 35, 20, 14, 10, 5};

    private void Awake()
    {
        graphContainer = GameObject.Find("graphContainer").GetComponent<RectTransform>();

        
        ShowGraph(valueList);
    }

    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    private void ShowGraph(List<int> valueList)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float yMaximum = 100f;
        float xSize = 40f;

        GameObject lastCircleGameObject = null;
        for (int i = 0; i < valueList.Count; i++)
        {
            float xPosition = 20f + i * xSize;
            float yPosition = (valueList[i] / yMaximum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            if (lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject;
        }
    }

    private void Cleanup()
    {
        foreach (Transform child in graphContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
    }
    void Start()
    {
        bitcoinText = GameObject.Find("BitcoinPrice").GetComponent<Text>();
        bitcoinPrevious = GameObject.Find("BitcoinPriceHistory").GetComponent<Text>();
        bitcoinPreviousPrice = bitcoinPrice - Random.Range(1, 3);
    }

    void Update()
    {
        bitcoinText.text = "Bitcoin Price : " + bitcoinPrice;
        bitcoinPrevious.text = "Previous Price: " + bitcoinPreviousPrice;

        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePrice(Random.Range(-18, 20));
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Cleanup();
        }

    }
    void ChangePrice(int change)
    {
        bitcoinPreviousPrice = bitcoinPrice;
        bitcoinPrice = Mathf.Clamp(bitcoinPrice, 0, 90);
        bitcoinPrice += change;

        Cleanup();

        valueList.RemoveAt(0);
        valueList.Add(bitcoinPrice);
        ShowGraph(valueList);
    }

    public void setInactive()
    {
        gameObject.SetActive(false);
    }
}
