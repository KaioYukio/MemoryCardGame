using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaIconeUI : MonoBehaviour
{
    public Animator an1;
    public Animator an2;

    public bool isPlayer1;

    // Start is called before the first frame update
    void Start()
    {
        if (isPlayer1)
        {
            an1 = GetComponent<Animator>();

        }
        else
        {
            an2 = GetComponent<Animator>();
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoUp1()
    {
        if (isPlayer1)
        {
            an1.SetTrigger("GoUp");
            Debug.Log("1 subindo");
        }

    }

    public void GoUp2()
    {
        if (!isPlayer1)
        {
            an2.SetTrigger("GoUp");
            Debug.Log("2 subindo");
        }

    }


    public void GoDown1()
    {
        if (isPlayer1)
        {
            Debug.Log("1 Descendo");
            an1.SetTrigger("GoDown");
        }

    }

    public void GoDown2()
    {
        if (!isPlayer1)
        {

            an2.SetTrigger("GoDown");
            Debug.Log("2 Descendo");
        }

    }
}
