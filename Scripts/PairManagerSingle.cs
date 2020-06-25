using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PairManagerSingle : MonoBehaviour
{
    public static PairManagerSingle instance;


    //public List<GameObject> carta = new List<GameObject>();

    public GameObject[] carta;
    //public GameObject[] newCartas;
    public GameObject highlightPlayer1;

    public GameObject[] cartaNova;

    public GameObject telaWin1;

    //Determina qual pack de carta nova vai ser ligado
    public int whatPackCarta;
    public int indexPack;

    //Controla quando as será checado as cartas É MUITO IMPORTANTE PARA MANTER O JOGO CORRETO, ISSO É RESPONSAVEL PELO ESTADO QUE O TURNO SE PASSA (PRIMEIRA CARTA ABERTA, SEGUNDA CARTA ABERTA)
    public int cartasAtivas;

    public int valor1;
    public int valor2;

    private int notI;
    public int cartasReveladas;

    //Permite que a carta mude o estado DO turno MUITO IMPORTANTE. TODA VEZ QUE UM TURNO ACABA O "ALLOW" RETORNA TRUE
    public bool allow;

    public float pairsFeitos;
    public float pairsParaResetar;

    //Pontos dos jogadores
    public float pontos1;
    public float pontosToWin;

    //Audio
    private AudioSource audioSource;
    public AudioClip[] acerto;
    public AudioClip erro;
    public bool play;

    //Estados do turno
    public enum Turno { jogador1, jogador2 };
    public Turno turno;

    //Aniamtor da barra
    public Animator CardBarraAnimatorSingleP;

    public GameObject EstrelasParticles;
    public GameObject ReachPoint1, ReachPointFather;

    public Animator MemorizeAnimator;

    public GameObject firstIcon;
    public GameObject secondIcon;
    public int canShowTutorial;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        notI = -1;
        allow = true;
        
        highlightPlayer1.SetActive(true);

        turno = Turno.jogador1;

        whatPackCarta = 0;

        //cartasToSpawn = GameObject.FindGameObjectWithTag("CartaParent");


        for (int i = 0; i < carta.Length; i++)
        {
            if (whatPackCarta == 0)
            {
                MemorizeAnimator.SetTrigger("TurnChange");
                cartaNova[0].SetActive(true);
            }

            if (carta[i] == null)
            {
                carta[i] = GameObject.Find("Carta " + i);
            }

        }

        cartaNova[0].SetActive(true);
        indexPack = 0;
        audioSource = GetComponent<AudioSource>();


        canShowTutorial = 0;
        firstIcon.SetActive(true);
        secondIcon.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {

        //if (Help.instance.erros / 2 >= Help.instance.errorsToHelp)
        //{
        //    Help.instance.transform.gameObject.SetActive(true);
        //    Help.instance.erros = 0;
        //}

        //CheckCartas();
        NextPack();
        CheckWin();


        //Permite uma proxima açao do jogador quando a primeira carta é clicada
        if (cartasAtivas == 1)
        {
            allow = true;
        }


        //Checa, se no turno, possui a primeira carta com o valor revelado 
        if (cartasAtivas == 1)
        {


            for (int i = 0; i < carta.Length; i++)
            {
                if (carta[i] != null && notI != i)
                {

                    if (carta[i].GetComponent<Carta>().valorRevelado)
                    {
                        //Debug.Log("ATIVO");
                        //Cancela a procura do primeiro valor da carta
                        notI = i;
                        //Pega o primero valor da carta revelada
                        valor1 = carta[i].GetComponent<Carta>().valor;

                        //Debug.Log("valor");
                    }
                }

            }
        }
        //Checa, se no turno, possui a segunda carta com o valor revelado 
        else if (cartasAtivas == 2)
        {
            for (int i = 0; i < carta.Length; i++)
            {
                if (carta[i] != null && i != notI)
                {
                    if (carta[i].GetComponent<Carta>().valorRevelado)
                    {
                        //Pega o segundo valor da carta revelada
                        valor2 = carta[i].GetComponent<Carta>().valor;
                    }
                }

            }
        }

        //Checa se as cartas reveladas sao iguais
        //Se os valores revelados forem iguais isso acontece:
        if (valor1 == valor2 && cartasAtivas == 2)
        {
            for (int i = 0; i < carta.Length; i++)
            {
                if (carta[i] != null)
                {
                    if (carta[i].GetComponent<Carta>().valorRevelado)
                    {
                        if (canShowTutorial < 3)
                        {
                            canShowTutorial += 1;
                        }

                        
                        if (canShowTutorial == 1)
                        {
                            firstIcon.SetActive(false);
                            secondIcon.SetActive(true);
                        }
                        else if (canShowTutorial > 2)
                        {
                            secondIcon.SetActive(false);

                        }

                        Help.instance.erros = 0;

                        //Permite uma nova açao do jogador
                        Invoke("Permission", 1);
                        cartasAtivas = 0;

                        //Permite a movimentação das cartas
                        carta[i].GetComponent<Carta>().Invoke("CanMoveCarta", 1);

                        if (play)
                        {
                            int rand = Random.Range(0, 1);
                            audioSource.PlayOneShot(acerto[rand]);
                            play = false;
                        }

                    }
                }

            }



        }
        //Se os valores revelados não forem iguais isso acontece:
        else if (valor1 != valor2 && cartasAtivas == 2)
        {

            for (int i = 0; i < carta.Length; i++)
            {
                if (carta[i] != null)
                {
                    if (carta[i].GetComponent<Carta>().valorRevelado)
                    {
                        Help.instance.erros += 1;

                        //Permite uma nova açao do jogador
                        Invoke("Permission", 1);
                        cartasAtivas = 0;

                        //Vira as cartas com a face para baixo
                        carta[i].GetComponent<Carta>().FlipCartaBack();
                        if (play)
                        {

                            audioSource.PlayOneShot(erro);
                            play = false;
                        }

                    }
                }
            }

        }

    }


    //Permite uma nova açao do jogador
    public void Permission()
    {
        allow = true;
        play = true;
        //Debug.Log("ALLOW");


    }



    public void PontuarJogador()
    {
        if (turno == Turno.jogador1)
        {
            CardBarraAnimatorSingleP.SetTrigger("Grow");
            pontos1 += 0.5f;
            Instantiate(EstrelasParticles, ReachPoint1.transform.position, Quaternion.identity);


        }

    }


    public void NextPack()
    {
        if (pairsFeitos >= pairsParaResetar)
        {
            pairsFeitos = 0;

            indexPack += 1;

            if (indexPack < cartaNova.Length)
            {
                if (PairManagerSingle.instance.pontos1 < pontosToWin)
                {
                    MemorizeAnimator.SetTrigger("TurnChange");

                    cartaNova[indexPack].SetActive(true);
                }

            }


        }


        for (int i = 0; i < carta.Length; i++)
        {


            if (carta[i] == null)
            {
                carta[i] = GameObject.Find("Carta " + i);
            }

        }

    }

    public void CheckCartas()
    {

        //Checa se os forem abertos todos os pares
        if (pairsFeitos >= pairsParaResetar)
        {

            int rand = Random.Range(0, carta.Length);
            whatPackCarta = rand;

            if (whatPackCarta == 0)
            {
                if (cartaNova[0].activeSelf)
                {
                    pairsFeitos = pairsParaResetar;
                }
                else
                {
                    cartaNova[0].SetActive(true);
                    pairsFeitos = 0;
                }

            }
            else if (whatPackCarta == 1)
            {

                if (cartaNova[1].activeSelf)
                {
                    pairsFeitos = pairsParaResetar;
                }
                else
                {
                    cartaNova[1].SetActive(true);
                    pairsFeitos = 0;
                }

            }
            else if (whatPackCarta == 2)
            {

                if (cartaNova[2].activeSelf)
                {
                    pairsFeitos = pairsParaResetar;
                }
                else
                {
                    cartaNova[2].SetActive(true);
                    pairsFeitos = 0;
                }

            }
            else if (whatPackCarta == 3)
            {

                if (cartaNova[3].activeSelf)
                {
                    pairsFeitos = pairsParaResetar;
                }
                else
                {
                    cartaNova[3].SetActive(true);
                    pairsFeitos = 0;
                }

            }
            for (int i = 0; i < carta.Length; i++)
            {


                if (carta[i] == null)
                {
                    carta[i] = GameObject.Find("Carta " + i);
                }

            }


        }
    }

    public void CheckWin()
    {
        if (pontos1 >= pontosToWin)
        {
            telaWin1.SetActive(true);
        }
    }


    public void ResetCartasReveladas()
    {
        cartasReveladas = 0;
    }


}
