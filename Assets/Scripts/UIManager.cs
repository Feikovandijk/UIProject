using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject overviewCanvas;
    public GameObject bitcoinCanvas;
    public GameObject startmenuCanvas;

    void Start()
    {
        overviewCanvas.SetActive(false);
        bitcoinCanvas.SetActive(false);
        startmenuCanvas.SetActive(true);
    }

    public void StartGame()
    {
        overviewCanvas.SetActive(true);
        startmenuCanvas.SetActive(false);
    }

    public void Quitgame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void TradeBitcoins()
    {
        overviewCanvas.SetActive(false);
        bitcoinCanvas.SetActive(true);
        startmenuCanvas.SetActive(false);
    }

    public void NextTurn()
    {
        
    }
}