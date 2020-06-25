using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotaoSom : MonoBehaviour
{
    public Sprite comSom;
    public Sprite semSom;

    private Image thisSprite;

    public bool isMute;
    private bool play;
    // Start is called before the first frame update
    void Start()
    {
        thisSprite = GetComponent<Image>();
        thisSprite.sprite = semSom;
        isMute = false;
        play = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMute && play)
        {
            thisSprite.sprite = semSom;
            SoundManager.instance.MuteAudio();
            play = false;
        }
        else if (play)
        {
            thisSprite.sprite = comSom;
            SoundManager.instance.DeMuteAudio();
            play = false;
        }
    }

    public void MuteDesmute()
    {
        isMute = !isMute;
        play = true;
    }

    //public void SemSom()
    //{
    //    if (thisSprite == comSom)
    //    {
    //        thisSprite.sprite = semSom;
    //        SoundManager.instance.MuteAudio();
    //    }
    //    else
    //    {
    //        thisSprite.sprite = comSom;
    //        SoundManager.instance.DeMuteAudio();
    //    }
    //}
}
