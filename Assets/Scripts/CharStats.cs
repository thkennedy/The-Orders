using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class CharStats : MonoBehaviour
{
    public string charName;

    public int playerLevel = 1;
    
    public int currentExp;
    public int[] expToNextLevel;
    public int maxLevel = 100;
    public int baseExp = 1000;
    public float expExponent = 1.5f;
    

    public int currentHp;
    public int maxHp = 100;
    public int currentMp;
    public int maxMp = 30;
    public int[] mpLevelBonus;
    public int baseMp = 4;
    public float mpLevelBonusExponent = 1.1f;
    public int strength;
    public int defense;
    public int weaponPower;
    public int armorPower;
    public string equippedWeaponName;
    public string equippedArmor;
    public Sprite characterImage;
    
    
    // Start is called before the first frame update
    void Start()
    {
        expToNextLevel = new int[maxLevel];
        mpLevelBonus = new int[maxLevel];
        
        for (int i = 1; i < maxLevel; i++)
        {
            expToNextLevel[i] = Mathf.FloorToInt((baseExp * (math.pow(i, expExponent))));
            mpLevelBonus[i] = i;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddExp(100);
        }
        
    }

    public void AddExp(int expToAdd)
    {
        if (playerLevel == maxLevel)
        {
            return;
        }
        
        currentExp += expToAdd;

        if (currentExp > expToNextLevel[playerLevel])
        {
            currentExp -= expToNextLevel[playerLevel];
            playerLevel++;
            if (playerLevel == maxLevel)
            {
                currentExp = 0;
            }

            if (playerLevel % 2 == 0)
            {
                strength++;
            }
            else
            {
                defense++;
            }

            int hpChange = Mathf.FloorToInt(maxHp * 0.05f);
            maxHp += hpChange;
            currentHp += hpChange;

            currentMp += mpLevelBonus[playerLevel];
            maxMp += mpLevelBonus[playerLevel];

        }
    }
}
