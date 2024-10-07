using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
public class ResultNameInput : MonoBehaviour
{
    public TMP_InputField playerNameInput;

    private string playerName = null;


    public FadeController fader;
    private void Awake()
    {
       
    }

    private void Update()
    {
       
        //키보드
        if (playerNameInput.text.Length > 0 && Input.GetKeyDown(KeyCode.Return))
        {
            InputName();
        }
    }

    //마우스
    public void InputName()
    {
        Debug.Log(".클릭");
        playerName = playerNameInput.text;
        LeaderboardManager.Instance.currentName = playerName;
        LeaderboardManager.Instance.fisrtScene = false;
        StartCoroutine(Activate());
        

    }

    IEnumerator Activate()
    {
        fader.FadeOut();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
    }
}
