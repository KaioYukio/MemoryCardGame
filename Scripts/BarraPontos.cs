using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraPontos : MonoBehaviour
{
    private Image image;

    public bool isPlayer1;

    

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isSinglePLayer)
        {
            if (isPlayer1)
            {
                image.fillAmount = PairManager.instance.pontos1 / PairManager.instance.pontosToWin;
            }
            else
            {
                image.fillAmount = PairManager.instance.pontos2 / PairManager.instance.pontosToWin;
            }
        }
        else
        {
            image.fillAmount = PairManagerSingle.instance.pontos1 / PairManagerSingle.instance.pontosToWin;
        }


    }
}
