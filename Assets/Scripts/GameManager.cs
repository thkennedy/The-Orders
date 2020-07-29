using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public CharStats[] playerStats;

    public bool gameMenuOpen;
    public bool dialogActive;
    public bool transitioningBetweenAreas;

    public string[] itemsHeld;
    public int[] numberOfItems;
    public Item[] referenceItems;

    public static GameManager Instance()
    {
        return instance;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        SortItems();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMenuOpen || dialogActive || transitioningBetweenAreas)
        {
            PlayerController.Instance().canMove = false;
        }
        else
        {
            PlayerController.Instance().canMove = true;
        }
    }

    public Item GetItemDetails(string itemToGrab)
    {
        for (int i = 0; i < referenceItems.Length; i++)
        {
            if (referenceItems[i].itemName == itemToGrab)
            {
                return referenceItems[i];
            }
        }

        return null;
    }

    public void SortItems()
    {
        bool bItemAfterSpace = true;
        while (bItemAfterSpace)
        {
            bItemAfterSpace = false;
            for (int i = 0; i < itemsHeld.Length - 1; i++)
            {
                if (itemsHeld[i] == "")
                {
                    itemsHeld[i] = itemsHeld[i + 1];
                    itemsHeld[i + 1] = "";

                    numberOfItems[i] = numberOfItems[i + 1];
                    numberOfItems[i + 1] = 0;

                    if (itemsHeld[i] != "")
                    {
                        bItemAfterSpace = true;
                    }
                }
            }
        }
    }

    public void AddItem(string itemToAdd)
    {
        int newItemPosition = 0;
        bool foundSpace = false;

        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] == "" || itemsHeld[i] == itemToAdd)
            {
                newItemPosition = i;
                foundSpace = true;
                break;
            }
        }

        if (foundSpace && GameManager.Instance().GetItemDetails(itemToAdd) != null)
        {
            itemsHeld[newItemPosition] = itemToAdd;
            numberOfItems[newItemPosition]++;
        }
        else
        {
            Debug.LogError("Attempting to Add Invalid Item: " + itemToAdd);
        }

        GameMenu.Instance().ShowItems();
    }

    public void RemoveItem(string itemToRemove)
    {
        if (!GameManager.Instance().GetItemDetails(itemToRemove))
        {
            Debug.LogError("Attempting to Remove Invalid Item " + itemToRemove);
            return;
        }

        int deleteItemPosition = 0;
        bool foundItem = false;

        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] == itemToRemove)
            {
                deleteItemPosition = i;
                foundItem = true;
                break;
            }
        }

        if (foundItem)
        {
            numberOfItems[deleteItemPosition]--;
            if (numberOfItems[deleteItemPosition] <= 0)
            {
                itemsHeld[deleteItemPosition] = "";
                GameMenu.Instance().activeItem = null;
            }
        }
        else
        {
            Debug.LogError("Attempting to Remove Item Not In Inventory" + itemToRemove);
        }

        GameMenu.Instance().ShowItems();
    }
}