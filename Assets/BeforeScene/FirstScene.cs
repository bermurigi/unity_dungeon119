using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class FirstScene : MonoBehaviour, IPointerClickHandler
{
    public LoopType loopType;
    public TextMeshProUGUI text;


    public GameObject inputPanel;

    public bool isActive;
    private void Start()
    {
        text.DOFade(0.0f, 2).SetLoops(-1, loopType);
    }
    void Update()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        
            GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
        Debug.Log(clickedObject);
            if (clickedObject.CompareTag("Panel"))
            {
           
                inputPanel.SetActive(false);
                isActive = false;
            }
            else if (clickedObject.CompareTag("BPanel"))
            {

            inputPanel.SetActive(true);
            isActive = false;
            }





    }

    
}
