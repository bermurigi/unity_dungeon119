using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public FadeController fader;
    public void GoFirstScene()
    {
        if(LeaderboardManager.Instance != null)
        {
            Destroy(LeaderboardManager.Instance.gameObject);
        }
        
        StartCoroutine(Activate("FirstScene"));
        

    }
    public void GoConverSationScene()
    {
        StartCoroutine(Activate("Conversation"));
    }
    public void GoMainScene()
    {
        StartCoroutine(Activate("MainScene"));
    }
    public void GoRetryScene()
    {
        
        StartCoroutine(Activate("Retry"));
    }
    
    public void GoClearScene()
    {
        StartCoroutine(Activate("Clear"));
        
    }


    IEnumerator Activate(string SceneName)
    {
        fader.FadeOut();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneName);
    }

}
