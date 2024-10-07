using Ilumisoft.RadarSystem;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    [Header("# Game Control")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime;

    [Header("# Player Info")]
    public int level;
    public int Rescue;
    public float exp;
    public float[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

    [Header("# Game Object")]
    public static GameManager instance;
    public PoolManager pool;
    public Transform player;
    public Transform weaponGroup;
    public LevelUp uiLevelUp;

    public Transform[] Chracters;
    public int CNum;

    public bool noHit;
    public int health;
    public int maxHealth;

    public TMP_Text hText;
    public Radar radar;
    public GameObject lvUpEf;

    public Spawner spawner;

    public GameObject exit;
    public bool isExit;

    public Image bloodImage;
    public Color bloodColor;
    //�̰� �����ٳ�����ɵ�

    public bool isBoss;
    public GameObject mainCamera;


    public string scoreTime;

    public GameObject pathObject;

    public Camera fpsCamera;
    public float angry = 1;

    public GameObject test;
    public int kill;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("ĳ����"))
        {
            CNum = PlayerPrefs.GetInt("ĳ����");
        }
       
        
        Debug.Log(CNum);
        instance = this;
        
        player = Chracters[CNum];
        player.gameObject.SetActive(true);
        radar.Player = mainCamera;
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        health = maxHealth;
      
        switch (CNum)
        {
            case 0://��� �ż��ѳ���
                break;
            case 1://������
                maxHealth = 125;
                health = 125;
                break;
            case 2://�߰��� �ֵθ���   
             
                break;
            case 3://���� �þ߹��
                RenderSettings.fogDensity = 0.05f;
                break;
            case 4://������ 
                break;
            case 5://������ 
                
                break;
            case 6://����ŷ 
                break;
            case 7://��� ȸ��Į
                maxHealth = 300;
                health = 300;
                break;
            case 8://������
                maxHealth = 200;
                health = 200;
                break;
        }
        //�ӽ� ��ũ��Ʈ (ù��° ĳ���� ����)


      


        


    }
    float expTime;
    float hpTime;
    public GameObject NaviText;
    private void Update()
    {
        if(CNum==2)
        {
            hpTime += Time.deltaTime;
            if(hpTime>1 && health<maxHealth)
            {
                hpTime = 0;
                health += 1;
            }
        }
        if (CNum == 6)
        {
            angry = 1f + 0.5f * (1f - health / maxHealth);

        }
        expTime += Time.deltaTime;
        if(CNum==5 && expTime>1)
        {
            expTime = 0;
            GetExp(1);
        }

        if (health<0&&isLive)
        {
            
            isLive = false;
            //���Ѿ��

            SceneManager.LoadScene("Retry");
           

            
        }
        
        if(Rescue>=5 && !isExit)
        {
            
            exit.SetActive(true);
            isExit = true;
            pathObject.SetActive(true);
            pathObject.transform.parent = player;
            NaviText.SetActive(true);
            pathObject.GetComponent<NaviManager>().InitNaviManager(player, exit.transform.position, 0.01f);
        }
        
       
        if (!isLive)
            return;
        if (isBoss)
            return;
        gameTime += Time.deltaTime;
        if (gameTime > maxGameTime)
        {
            AudioManager.instance.PlayBgm(AudioManager.Bgm.Play2);
            AudioManager.instance.SetBgmVolume(1);
            gameTime = maxGameTime;
            for (int i = 0; i< 20; i++)
            {
                spawner.Boss();
            }
            
            isBoss = true;
            
            
        }
    }
    private void LateUpdate()
    {
        hText.text = health.ToString() + "/" + maxHealth.ToString();


        
        int min = Mathf.FloorToInt(gameTime / 60);
        int sec = Mathf.FloorToInt(gameTime % 60);
        scoreTime = string.Format("{0:D2}:{1:D2}", min, sec);
    }

    public void GetExp(float value=1f)
    {
       
        if(CNum==1)
        {
            exp += value*0.75f;
        }
        else
        {
            exp += value;
        }
        
        if (exp >= nextExp[Mathf.Min(level, nextExp.Length-1)])
        {
            lvUpEf.gameObject.SetActive(false);
            level++;
            exp = 0;
            uiLevelUp.Show();
            lvUpEf.gameObject.SetActive(true);
            lvUpEf.transform.parent = player;
            lvUpEf.transform.localPosition = Vector3.zero;
            AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
        }
            
        
    }

    
    

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}
