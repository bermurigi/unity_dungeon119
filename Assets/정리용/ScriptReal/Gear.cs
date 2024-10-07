using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;

    public void Init(ItemData data)
    {
        //Basic Set
        name = "Gear" + data.itemId;
        transform.parent = GameManager.instance.weaponGroup;
        transform.localPosition = Vector3.zero;
        //property Set
        type = data.itemType;
        rate = data.damages[0];
        ApplyGear();
    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }

    void ApplyGear()
    {
        switch (type)
        {
            case ItemData.ItemType.Glove:
                RateUp();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }

    void RateUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach (Weapon weapon in weapons)
        {
            switch(weapon.id)
            {
                case 0:
                    weapon.speed = 2 + (1 - rate);
                    break;
                case 1:
                    weapon.speed = 1 * (1 - rate);
                    break;
                case 2:
                    weapon.speed = 0.5f * (1 - rate);
                    break;
                case 3:
                    weapon.speed = 1.5f * (1 - rate);
                    break;
                case 4:
                    weapon.speed = 10 * (1-rate);
                    break;
                case 5:
                    weapon.speed = 20 - rate * 10;
                    break;
                default:
                    weapon.speed = 0.5f * (1f-rate);
                    break;
            }
            
        }
    }

    void SpeedUp()
    {
        float speed = 4;
        Debug.Log(speed + speed * rate);
        GameManager.instance.player.GetComponent<Player>().speed = speed + speed * rate;
        GameManager.instance.player.GetComponent<Player>().SpeedUp();
    }
}
