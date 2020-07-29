using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{

    public Image buttonImage;
    public TextMeshProUGUI amountText;
    public int buttonValue;

    public void Press()
    {
        if (GameManager.Instance().itemsHeld[buttonValue] != "")
        {
            GameMenu.Instance()
                .SelectItem(GameManager.Instance().GetItemDetails(GameManager.Instance().itemsHeld[buttonValue]));
        }
    }
}
