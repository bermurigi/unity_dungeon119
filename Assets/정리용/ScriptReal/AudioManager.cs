using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")] 
    public AudioClip[] bgmClips;
    public float bgmVolume;
    private AudioSource bgmPlayer;
    
    [Header("#SFX")] 
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    private AudioSource[] sfxPlayers;
    private int channelIndex;

    public enum Bgm
    {
        Play,
        Play2
    }

    public enum Sfx
    {
        Attack,
        Hit,
        Rescue,
        LevelUp,
        jail
    }
    void Awake()
    {
        instance = this;
        Init();
    }

    void Init()
    {
        //배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        //bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;

        PlayBgm(Bgm.Play);

        //효과음 플레이어 초기화
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume;
        }
        
        
        
    }
    
    //배경하나였을때 기준임

    public void PlayBgm(Bgm bgm)
    {
       
        
            bgmPlayer.clip = bgmClips[(int)bgm];
            bgmPlayer.Play();
            
        
    }


    // 효과음 재생 방법 다른 스크립트에서 AudiManager.instance.Playsfx(AudioManager.sfx.Select);사용
    public void PlaySfx(Sfx sfx)
    {
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }
        
    }

    public void SetBgmVolume(float volume)
    {
        bgmVolume = 0.2f * volume;
        bgmPlayer.volume = bgmVolume;
    }
    
    public void SetSfxVolume(float volume)
    {
        sfxVolume = 0.5f * volume;
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index].volume = sfxVolume;
        }
    }
}
