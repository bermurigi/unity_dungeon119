using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NaviText : MonoBehaviour
{
    public LoopType loopType;
    public TextMeshProUGUI text;


   
    private void Start()
    {
        text.DOFade(0.1f, 2).SetLoops(-1, loopType);
    }
}
