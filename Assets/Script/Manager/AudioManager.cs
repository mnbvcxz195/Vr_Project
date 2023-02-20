using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Sound
{
    public string name;
    public AudioClip audioclip;
}
public class AudioManager : MonoBehaviour
{
    #region instance

    private static AudioManager instance = null;

    public static AudioManager GetInstance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject("@AudioManager");
            instance = go.AddComponent<AudioManager>();

            DontDestroyOnLoad(go);
        }
        return instance;

    }
    #endregion

    public List<AudioClip> bgList = new List<AudioClip>();
    public List<AudioClip> sfxList = new List<AudioClip>();
    public List<AudioClip> monAudioList = new List<AudioClip>();
    public List<AudioClip> playerAudioList = new List<AudioClip>();

    private void Awake()
    {
        InitSounds();
    }
    public void BgmPlay(AudioSource bgm, int a)
    {
        bgm.clip = bgList[a];
        bgm.Play();
        bgm.loop = true;
    }
    public void SfxPlay(AudioSource sfx, int a)
    {
        sfx.clip = sfxList[a];
        sfx.Play();
        sfx.pitch = 2;
    }
    public void MonSfxPlay(AudioSource monsfx, int a,bool loop)
    {
        monsfx.clip = monAudioList[a];
        monsfx.Play();
        monsfx.loop = loop;

    }

    public void InitSounds()
    {
        AudioClip[] bgm = Resources.LoadAll<AudioClip>($"Audio/Bgm");

        AudioClip[] sfx = Resources.LoadAll<AudioClip>($"Audio/Sfx");

        AudioClip[] Monsfx = Resources.LoadAll<AudioClip>($"Audio/Monster");



        for (int i = 0; i < bgm.Length; i++)
        {
            bgList.Add(bgm[i]);
        }
        for (int i = 0; i < sfx.Length; i++)
        {
            sfxList.Add(sfx[i]);
        }
        for (int i = 0; i < Monsfx.Length; i++)
        {
            monAudioList.Add(Monsfx[i]);
        }

    }

}
