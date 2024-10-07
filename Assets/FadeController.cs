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
            Debug.LogError("panel ������Ʈ�� ã�� �� �����ϴ�.");
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
        Debug.Log("FadeCnvasController_ Fade Out ����");
        StartCoroutine(CoFadeOut());
        Debug.Log("FadeCanvasController_ Fade Outg ��");
    }

    IEnumerator CoFadeIn()
    {
        float elapsedTime = 0f;
        float fadedTime = 0.5f;

        while(elapsedTime <=fadedTime)
        {
            panel.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(1f, 0f, elapsedTime / fadedTime));
            elapsedTime += Time.deltaTime;
            Debug.Log("Fade In ��...");
            yield return null;
        }
        Debug.Log("Fade In ��");
        panel.SetActive(false);
        onCompleteCallback?.Invoke();
        yield break;
    }

    IEnumerator CoFadeOut()
    {
        float elapsedTime = 0f; //��������ð�
        float fadedTime = 0.5f; //�Ѽҿ�ð�

        while (elapsedTime <= fadedTime)
        {
            panel.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(0f, 1f, elapsedTime / fadedTime));
            elapsedTime += Time.deltaTime;
            Debug.Log("Fade out ��...");
            yield return null;
        }
        Debug.Log("Fade out ��");
        onCompleteCallback?.Invoke();
        yield break;
    }

    public void RegisterCallback(Action callback)
    {
        onCompleteCallback = callback;
    }
}
