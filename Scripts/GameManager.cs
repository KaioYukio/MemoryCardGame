using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isSinglePLayer;

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

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSinglePlayer()
    {


        Invoke("LoadSingle",0.5f);

    }

    public void LoadSingle()
    {
        isSinglePLayer = true;
        SoundManager.instance.audioSource.Pause();
        SoundManager.instance.play = true;
        MyLoad.LoadLevel("SinglePlayerLevel");
    }

    public void LoadMultiplayer()
    {

        Invoke("LoadMulti", 0.5f);
    }

    public void LoadMulti()
    {
        isSinglePLayer = false;
        SoundManager.instance.audioSource.Pause();
        SoundManager.instance.play = true;
        MyLoad.LoadLevel("MultiPlayerLevel");
    }

    public void Recomeçar()
    {
        SoundManager.instance.audioSource.Pause();
        SoundManager.instance.play = true;
        MyLoad.LoadLevel("Inicio");
    }

    public void Sair()
    {
        Application.Quit();
    }
}
