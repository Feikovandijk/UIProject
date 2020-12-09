using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIMoney : MonoBehaviour
{
    private int money = 50;
    public Text moneyText;

    void Update()
    {
        moneyText.text = "Balance : " + money;

        if (Input.GetKeyDown(KeyCode.P))
        {
            money++;
        }

    }
}