using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    public Item[] items;
    private void Awake()
    {
        rect=GetComponent<RectTransform>();
        items=GetComponentsInChildren<Item>(true);
    }
    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        Cursor.lockState = CursorLockMode.None;
        GameManager.instance.Stop();
       

    }
    public void Hide()
    {
        rect.localScale = Vector3.zero;
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.instance.Resume();
    }

    public void Select(int index)
    {
        items[index].OnClick();
    }

    void Next()
    {
        // 1. ��� ������ ��Ȱ��ȭ
        foreach (Item item in items) 
        {
            item.gameObject.SetActive(false);
        }

        // 2. ���߿��� ���� 3�� ������ Ȱ��ȭ
        int[] ran = new int[3];
        while(true)
        {
            ran[0] = Random.Range(0, items.Length);
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);
            if (ran[0] != ran[1] && ran[0]!=ran[2] && ran[1] != ran[2])
                break;
        }

        for(int index = 0; index<ran.Length;index++)
        {
            Item ranItem = items[ran[index]];

            // 3. ���� ������ �����鸮��
            if (ranItem.level==ranItem.data.damages.Length)
            {
                Next();
            }
            else
            {
                ranItem.gameObject.SetActive(true);
            }
            
        }
        


    }

}
