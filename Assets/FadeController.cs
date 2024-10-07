using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isFadeIn;
    public GameObject panel;
    private Action onCompleteCallback;

    private void Start()
    {
        if(!panel)
        {
            Debug.LogError("panel 오브젝트를 찾을 수 없습니다.");
            throw new MissingComponentException();
        }

        if(isFadeIn)
        {
            panel.SetActive(true);
            StartCoroutine(CoFadeIn());
        }
        else
        {
            panel.SetActive(false);
        }
    }

    public void FadeOut()
    {
        panel.SetActive(true);
        Debug.Log("FadeCnvasController_ Fade Out 시작");
        StartCoroutine(CoFadeOut());
        Debug.Log("FadeCanvasController_ Fade Outg 끝");
    }

    IEnumerator CoFadeIn()
    {
        float elapsedTime = 0f;
        float fadedTime = 0.5f;

        while(elapsedTime <=fadedTime)
        {
            panel.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(1f, 0f, elapsedTime / fadedTime));
            elapsedTime += Time.deltaTime;
            Debug.Log("Fade In 중...");
            yield return null;
        }
        Debug.Log("Fade In 끝");
        panel.SetActive(false);
        onCompleteCallback?.Invoke();
        yield break;
    }

    IEnumerator CoFadeOut()
    {
        float elapsedTime = 0f; //누적경과시간
        float fadedTime = 0.5f; //총소요시간

        while (elapsedTime <= fadedTime)
        {
            panel.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(0f, 1f, elapsedTime / fadedTime));
            elapsedTime += Time.deltaTime;
            Debug.Log("Fade out 중...");
            yield return null;
        }
        Debug.Log("Fade out 끝");
        onCompleteCallback?.Invoke();
        yield break;
    }

    public void RegisterCallback(Action callback)
    {
        onCompleteCallback = callback;
    }
}
