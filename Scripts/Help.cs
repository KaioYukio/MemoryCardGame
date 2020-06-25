using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Help : MonoBehaviour
{
    public static Help instance;

    public GameObject[] carta;


    public bool canTouch;
    public int erros;
    public int errorsToHelp;

    public Image image;



    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        canTouch = true;

        errorsToHelp = 5;
        
        //transform.gameObject.SetActive(false);

        //Invoke("MakeGOTrue", 10f);

        image = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {

       

        if (GameManager.instance.isSinglePLayer)
        {
            if (PairManagerSingle.instance.cartasReveladas == 0)
            {
                //Disponivel               
                image.color = new Color(1, 1, 1, 1);
                
            }
            else
            {
                //Desabilita Botao
                image.color = new Color(1, 1, 1, 0.50f);

            }

        }
        else if (!GameManager.instance.isSinglePLayer)
        {
            if (PairManager.instance.cartasReveladas == 0)
            {
                //Disponivel

                image.color = new Color(1, 1, 1, 1);
                
            }
            else
            {
                //Desabilita Botao
                image.color = new Color(1, 1, 1, 0.50f);
            }

        }
    }


    public void GetActiveCards()
    {

        if (GameManager.instance.isSinglePLayer)
        {
            if (canTouch && PairManagerSingle.instance.cartasReveladas == 0)
            {

                Invoke("GetAndFlipCards", 0f);
                //transform.gameObject.SetActive(false);

                canTouch = false;

                //Nao mudar o valor de tempo
                Invoke("ResetCanTouch", 4f);
            }
        }
        else if (!GameManager.instance.isSinglePLayer)
        {
            if (canTouch && PairManager.instance.cartasReveladas == 0)
            {

                Invoke("GetAndFlipCards", 0f);
                //transform.gameObject.SetActive(false);

                canTouch = false;
                //Nao mudar o valor de tempo
                Invoke("ResetCanTouch", 4f);
            }
        }


        
    }

    public void ResetCanTouch()
    {

        canTouch = true;
    }


    //Pega a cartas ativas e muda o png
    public void GetAndFlipCards()
    {
        for (int i = 0; i < carta.Length; i++)
        {
            if (carta[i] == null)
            {
                carta[i] = GameObject.Find("Carta " + i);


            }

            if (carta[i] != null)
            {
                //Faz mudar o png
                carta[i].GetComponent<Carta>().isHelping = true;
            }
        }
    }

    public void MakeGOTrue()
    {

        transform.gameObject.SetActive(true);
    }
}
