using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PairsFeito : MonoBehaviour
{
    public Text text;
    public Text text1;
    public Text text2;

    public float num;
    public float num1;
    public float num2;

    public bool isPlayer1;

    private Animator an;

    // Start is called before the first frame update
    void Start()
    {
        an = GetComponent<Animator>();

        if (GameManager.instance.isSinglePLayer)
        {
            if (isPlayer1)
            {
                text = GetComponent<Text>();
            }

        }
        else
        {
            if (isPlayer1)
            {
                text1 = GetComponent<Text>();
            }
            else
            {
                text2 = GetComponent<Text>();
            }


        }

    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.instance.isSinglePLayer)
        {
            num = Mathf.RoundToInt(PairManagerSingle.instance.pontos1);
            text.text = num.ToString() + "/" + PairManagerSingle.instance.pontosToWin;
        }
        else
        {
            if (isPlayer1)
            {
                num1 = Mathf.RoundToInt(PairManager.instance.pontos1);
                text1.text = num1.ToString() + "/" + PairManager.instance.pontosToWin;
            }
            else
            {
                num2 = Mathf.RoundToInt(PairManager.instance.pontos2);
                text2.text = num2.ToString() + "/" + PairManager.instance.pontosToWin;
            }

        }

    }

    public void Ponto1Go()
    {
        if (isPlayer1)
        {
            an.SetTrigger("Go");
            
        }
        
    }

    public void Ponto1Back()
    {
        if (isPlayer1)
        {
            an.SetTrigger("Volta");
        }
    }

    public void Ponto2Go()
    {
        if (!isPlayer1)
        {
            an.SetTrigger("Go");

        }
    }

    public void Ponto2Back()
    {
        if (!isPlayer1)
        {
            an.SetTrigger("Volta");

        }

    }
}
