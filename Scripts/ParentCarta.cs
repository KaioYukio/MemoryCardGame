using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentCarta : MonoBehaviour
{
    public GameObject[] newCartas;
    public GameObject[] pos;
    public Animator[] CartasAnimator;
    public float CardWaitTime = 3f;
    public float SlowCardWaittime = 0.1f;

    public int pairToReset;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < newCartas.Length; i++)
        {
                GameObject obj = newCartas[i];
                Animator card = CartasAnimator[i];
                int rand = Random.Range(0, newCartas.Length);
                newCartas[i] = newCartas[rand];
                CartasAnimator[i] = CartasAnimator[rand];
                CartasAnimator[rand] = card;
                newCartas[rand] = obj;
        }
        for (int i = 0; i < newCartas.Length; i++)
        {
            newCartas[i].transform.position = pos[i].transform.position;
            CartasAnimator[i].SetFloat("CardWaitTime", CardWaitTime);
            CardWaitTime -= SlowCardWaittime;
        }

        if (GameManager.instance.isSinglePLayer)
        {
            PairManagerSingle.instance.pairsParaResetar = pairToReset;
        }
        else
        {
            PairManager.instance.pairsParaResetar = pairToReset;
        }

    }

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }
}
