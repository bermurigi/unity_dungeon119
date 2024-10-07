using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{

    public ItemData data;
    public int level;

    public Image icon;
    public TMP_Text textLevel;
    public TMP_Text textName;
    public TMP_Text textDesc;

    public GameObject weapon;
    public GameObject gear;

    
    

    
    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];
        textName.text = data.itemName;
        textDesc.text = data.itemDesc;
    }
    private void OnEnable()
    {
        textLevel.text = "Lv." + (level + 1);

        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                if (level == 0)
                {

                    textDesc.text = data.itemName + " ¹«±â È¹µæ";
                }
                else
                {
                    textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100, data.counts[level]);
                }
                
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100);
                break;
            default:
                textDesc.text = string.Format(data.itemDesc);
                break;
        }
    }
    private void LateUpdate()
    {
        
    }


    public void OnClick()
    {
        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                if (level ==0)
                {
                    weapon.SetActive(true);
                    weapon.GetComponent<Weapon>().Init(data);  
                }
                else
                {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage += data.baseDamage * data.damages[level] ;
                    nextCount += data.counts[level];
                    weapon.GetComponent<Weapon>().LevelUp(nextDamage, nextCount);
                    
                    //unitFSM.LevelUp(nextDamage);
                }
            
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                if (level == 0)
                {
                    gear.SetActive(true);
                    gear.GetComponent<Gear>().Init(data);
                }
                else
                {
                    float nextRate = data.damages[level];
                    gear.GetComponent<Gear>().LevelUp(nextRate);
                }
                break;
            case ItemData.ItemType.Heal:
                GameManager.instance.maxHealth += 10;
                GameManager.instance.health = GameManager.instance.maxHealth;
                break;


        }

        level++;
        if(level ==data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
      
    }
  
}
