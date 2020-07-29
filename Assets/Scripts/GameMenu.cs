using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    private static GameMenu instance;

    public GameObject menu;
    public GameObject[] windows;

    private CharStats[] playerStats;

    public TextMeshProUGUI[] nameText;
    public TextMeshProUGUI[] hpText;
    public TextMeshProUGUI[] mpText;
    public TextMeshProUGUI[] lvlText;
    public TextMeshProUGUI[] expText;
    public Slider[] expSlider;
    public Image[] charImage;
    public GameObject[] charStatHolder;
    public GameObject[] statButtons;

    public TextMeshProUGUI statName,
        statHp,
        statMp,
        statStrength,
        statDefense,
        statWeaponEquipped,
        statWeaponPwr,
        statArmorEquipped,
        statArmorPwr,
        statExperience;

    public Image statImage;

    public ItemButton[] itemButtons;

    public string selectedItem;
    public Item activeItem;
    public TextMeshProUGUI itemName, itemDescription, useButtonText;

    public GameObject itemCharacterChoiceMenu;
    public TextMeshProUGUI[] itemCharacterChoiceNames;

    public static GameMenu Instance()
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
        GameManager.Instance().gameMenuOpen = menu.activeInHierarchy;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            // toggle menu
            if (menu.activeInHierarchy)
            {
                CloseMenu();
                GameManager.Instance().gameMenuOpen = false;
            }
            else
            {
                menu.SetActive(true);
                GameManager.Instance().gameMenuOpen = true;
                UpdateMainStats();
            }
        }
    }

    public void UpdateMainStats()
    {
        playerStats = GameManager.Instance().playerStats;

        for (int i = 0; i < playerStats.Length; i++)
        {
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                charStatHolder[i].SetActive(true);
                nameText[i].text = playerStats[i].charName;
                hpText[i].text = "HP: " + playerStats[i].currentHp + "/" + playerStats[i].maxHp;
                mpText[i].text = "MP: " + playerStats[i].currentMp + "/" + playerStats[i].maxMp;
                lvlText[i].text = "Lvl: " + playerStats[i].playerLevel;
                expText[i].text = playerStats[i].currentExp + "/" +
                                  playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                expSlider[i].maxValue = playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                expSlider[i].value = playerStats[i].currentExp;
                charImage[i].sprite = playerStats[i].characterImage;
            }
            else
            {
                charStatHolder[i].SetActive(false);
            }
        }
    }

    public void ToggleWindow(int windowIndex)
    {
        UpdateMainStats();

        for (int i = 0; i < windows.Length; i++)
        {
            if (i == windowIndex)
            {
                windows[i].SetActive(!windows[i].activeInHierarchy);
            }
            else
            {
                windows[i].SetActive(false);
            }
        }
        
        CloseItemCharacterChoice();
    }

    public void CloseMenu()
    {
        foreach (GameObject window in windows)
        {
            window.SetActive(false);
        }

        menu.SetActive(false);
        GameManager.Instance().gameMenuOpen = false;
        
        CloseItemCharacterChoice();
    }

    public void OpenStatsWindow()
    {
        // update info that is shown

        for (int i = 0; i < statButtons.Length; i++)
        {
            statButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
            statButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = playerStats[i].charName;
        }

        UpdateCharStatWindow(0);
    }

    public void UpdateCharStatWindow(int charIndex)
    {
        statName.text = playerStats[charIndex].charName;
        statHp.text = playerStats[charIndex].currentHp + "/" + playerStats[charIndex].maxHp;
        statMp.text = playerStats[charIndex].currentMp + "/" + playerStats[charIndex].maxMp;
        statStrength.text = playerStats[charIndex].strength.ToString();
        statDefense.text = playerStats[charIndex].defense.ToString();
        statWeaponEquipped.text = playerStats[charIndex].equippedWeaponName == String.Empty
            ? "None"
            : playerStats[charIndex].equippedWeaponName;
        statWeaponPwr.text = playerStats[charIndex].weaponPower.ToString();
        statArmorEquipped.text = playerStats[charIndex].equippedArmor == String.Empty
            ? "None"
            : playerStats[charIndex].equippedArmor;
        statArmorPwr.text = playerStats[charIndex].armorPower.ToString();
        statExperience.text =
            (playerStats[charIndex].expToNextLevel[playerStats[charIndex].playerLevel] -
             playerStats[charIndex].currentExp).ToString();
        statImage.sprite = playerStats[charIndex].characterImage;
    }

    public void ShowItems()
    {
        GameManager.Instance().SortItems();

        for (int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].buttonValue = i;

            if (GameManager.Instance().itemsHeld[i] != "")
            {
                itemButtons[i].buttonImage.gameObject.SetActive(true);
                Item heldItem = GameManager.Instance().GetItemDetails(GameManager.Instance().itemsHeld[i]);

                itemButtons[i].buttonImage.sprite = heldItem.itemSprite;
                itemButtons[i].amountText.text = GameManager.Instance().numberOfItems[i].ToString();
            }
            else
            {
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].amountText.text = "";
            }
        }
    }

    public void SetActiveItem(Item InItem)
    {
        throw new NotImplementedException();
    }

    public void SelectItem(Item InItem)
    {
        activeItem = InItem;

        if (activeItem.isItem)
        {
            useButtonText.text = "Use";
        }

        if (activeItem.isWeapon || activeItem.isArmor)
        {
            useButtonText.text = "Equip";
        }

        itemName.text = activeItem.itemName;
        itemDescription.text = activeItem.description;
    }

    public void DiscardItem()
    {
        if (!activeItem)
            return;
        
        GameManager.Instance().RemoveItem(activeItem.itemName);
    }

    public void OpenItemCharacterChoice()
    {
        if (!activeItem)
            return;
        
        itemCharacterChoiceMenu.SetActive(true);

        for (int i = 0; i < itemCharacterChoiceNames.Length; i++)
        {
            itemCharacterChoiceNames[i].text = GameManager.Instance().playerStats[i].charName;
            itemCharacterChoiceNames[i].transform.parent.gameObject.SetActive(GameManager.Instance().playerStats[i].gameObject.activeInHierarchy);
        }
    }

    public void CloseItemCharacterChoice()
    {
        itemCharacterChoiceMenu.SetActive(false);
    }

    public void UseItem(int selectedCharacter)
    {
        activeItem.Use(selectedCharacter);
        CloseItemCharacterChoice();
    }
}