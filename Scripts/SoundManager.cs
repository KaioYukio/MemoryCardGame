using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioMixer audioMixer;

    public AudioSource audioSource;
    public AudioClip menu;
    public AudioClip gameplay;

    public bool play;
    public string sceneName;

    Scene actualScene;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject);

        //if (play)
        //{
        //    audioSource.PlayOneShot(menu);
        //    play = false;
        //}

    }

    // Update is called once per frame
    void Update()
    {
        actualScene = SceneManager.GetActiveScene();
        sceneName = actualScene.name;
        

        if (play && (sceneName == "SinglePlayerLevel" || sceneName == "MultiplayerLevel"))
        {
            audioSource.Stop();
            audioSource.PlayOneShot(gameplay);
            play = false;
        }
        else if (play && (sceneName == "Inicio"))
        {
            audioSource.Stop();
            audioSource.PlayOneShot(menu);
            play = false;
        }
    }

    public void MuteAudio()
    {
        audioMixer.SetFloat("Principal", -80);
    }

    public void DeMuteAudio()
    {
        audioMixer.SetFloat("Principal", 20);
    }
}
