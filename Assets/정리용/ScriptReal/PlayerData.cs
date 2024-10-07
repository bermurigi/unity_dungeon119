using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    public FadeController fader;
    public GameObject[] chracters;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PlayerPrefs.GetString("CurrentPlayerName"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectChracter(int chracter)
    {
        chracters[chracter].SetActive(true);
        PlayerPrefs.SetInt("Ä³¸¯ÅÍ", chracter);
    }

    public void GoMainScene()
    {

        StartCoroutine(Activate());
        
    }

    IEnumerator Activate()
    {
        fader.FadeOut();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(2);
    }
}
