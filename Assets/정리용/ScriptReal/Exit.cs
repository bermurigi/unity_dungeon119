using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class Exit : MonoBehaviour
{
    public Rank rank;
    
    private void OnTriggerEnter(Collider other)
    {
        LeaderboardManager.Instance.SaveScore(LeaderboardManager.Instance.currentName, (int)GameManager.instance.gameTime,GameManager.instance.scoreTime,GameManager.instance.CNum,GameManager.instance.kill);
        
       
        SceneManager.LoadScene("Clear");
       
    }
}
