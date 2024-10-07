using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp,Level,Rescue,Time,Health, Blood, Kill}
    public InfoType type;

    TMP_Text myText;
    Slider mySlider;

    public Image myImage;

    private void Awake()
    {
        myText = GetComponent<TMP_Text>();
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                float curExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp[Mathf.Min(GameManager.instance.level, GameManager.instance.nextExp.Length - 1)];
                mySlider.value = curExp / maxExp;
                break;
            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}", GameManager.instance.level);
                break;

            case InfoType.Rescue:
                myText.text = string.Format("구조한 수: {0:F0} / 5", GameManager.instance.Rescue);
                break;

            case InfoType.Time:
                float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;

            case InfoType.Health:
                float curHealth = GameManager.instance.health;
                float maxHealth = GameManager.instance.maxHealth;
                mySlider.value = curHealth / maxHealth;
                break;
            case InfoType.Blood:
                if (myImage.color.a > 0)
                {
                    Color currentColor = myImage.color;
                    // 알파 값을 조정
                    currentColor.a -= Time.deltaTime;
                    // 컬러를 설정
                    myImage.color = currentColor;
                    Debug.Log("투명도 0보다큼");
                } 
                break;
            case InfoType.Kill:
                myText.text = string.Format("킬 수 : {0:F0}", GameManager.instance.kill);
                break;

        }
    }
}
