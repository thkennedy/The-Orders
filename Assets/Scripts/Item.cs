using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item Type")]
    public bool isItem;
    public bool isWeapon;
    public bool isArmor;

    [Header("Item Details")]
    public string itemName;
    public string description;
    public int value;
    public Sprite itemSprite;

    [Header("Item Effects")]
    public int amountToChange;

    public bool affectsHp, affectsMp, affectsStrength, affectsDefense;

    [Header("Weapon/Armor Settings")]
    public int weaponPower;

    public int armorPower;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use(int characterToUseOn)
    {
        CharStats selectedCharacter = GameManager.Instance().playerStats[characterToUseOn];

        if (isItem)
        {
            if (affectsHp)
            {
                selectedCharacter.currentHp += amountToChange;
                if (selectedCharacter.currentHp > selectedCharacter.maxHp)
                {
                    selectedCharacter.currentHp = selectedCharacter.maxHp;
                }
            }
            
            if (affectsMp)
            {
                selectedCharacter.currentMp += amountToChange;
                if (selectedCharacter.currentMp > selectedCharacter.maxMp)
                {
                    selectedCharacter.currentMp = selectedCharacter.maxMp;
                }
            }

            if (affectsStrength)
            {
                selectedCharacter.strength += amountToChange;
            }
            
            if (affectsDefense)
            {
                selectedCharacter.defense += amountToChange;
            }
            
        }

        if (isWeapon)
        {
            if (selectedCharacter.equippedWeaponName != "")
            {
                GameManager.Instance().AddItem(selectedCharacter.equippedWeaponName);
            }

            selectedCharacter.equippedWeaponName = itemName;
            selectedCharacter.weaponPower = weaponPower;
        }
        
        if (isArmor)
        {
            if (selectedCharacter.equippedArmor != "")
            {
                GameManager.Instance().AddItem(selectedCharacter.equippedArmor);
            }

            selectedCharacter.equippedArmor = itemName;
            selectedCharacter.armorPower = armorPower;
        }
        
        GameManager.Instance().RemoveItem(itemName);
    }
}
