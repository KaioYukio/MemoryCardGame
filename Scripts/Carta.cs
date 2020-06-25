using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carta : MonoBehaviour
{


    public int valor;

    public bool valorRevelado;

    private Image image;

    public Sprite imageRevelada;
    public Sprite imageEscondida;
    public float cartaSpeed;
    public float distancia;
    public float distRelativa;

    public bool reveal;
    public float revealTime;
    private float revealTimer;

    public GameObject reachPoint1;
    public GameObject reachPoint2;
    
    //Audio
    private AudioSource audioSource;
    public AudioClip[] selectionCarta;
    public AudioClip cartaAudio;

    public bool play;

    public GameObject CartasFeitas;

    //Mostra as Cartas como forma de ajuda
    public bool canShowHelpCards;
    private float showTime;
    private float showTimer;
    public bool isHelping;

    //Impede de clicar 2 vezes na mesma carta
    public bool jaCliclou;
    public bool canMove;
    public bool canTouch;
    private float timer;


    //Animator
    public Animator CardAnimator;
    public bool canFlip;


    // Start is called before the first frame update
    void Start()
    {
        CartasFeitas = GameObject.Find("CartasFeitas");
        CardAnimator = GetComponent<Animator>();
        
        valorRevelado = false;

        image = GetComponent<Image>();

        jaCliclou = false;

        revealTime = 5;
        reveal = true;

        reachPoint1 = GameObject.Find("ReachPoint1");
        reachPoint2 = GameObject.Find("ReachPoint2");

        canShowHelpCards = false;
        isHelping = false;
        showTime = 4f;

        canMove = false;
        canTouch = true;
        play = true;

        showTime = 3;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {


        MoveCarta();

        if (jaCliclou && image.sprite != imageRevelada)
        {
            timer += Time.deltaTime;

            if (timer >= 1)
            {
                jaCliclou = false;
                timer = 0;
            }

        }



        if (isHelping)
        {
            image.sprite = imageRevelada;

            showTimer += Time.deltaTime;
            if (showTimer > showTime)
            {
                isHelping = false;
                showTimer = 0;

            }

        }
        else
        {
            image.sprite = imageEscondida;
        }


        //Se a carta for revelada, muda o png de acordo
        if (valorRevelado && !isHelping)
        {
            image.sprite = imageRevelada;
        }
        else if (!valorRevelado && !isHelping)
        {
            image.sprite = imageEscondida;

        }


        //Revela e desrevela as cartas antes da rodada comecar
        if (reveal)
        {
            revealTimer += Time.deltaTime;
            valorRevelado = true;
            jaCliclou = true;

            if (revealTimer >= revealTime)
            {
                CardAnimator.SetTrigger("FlipReveal");
                Invoke("TrueReveal", 0.25f);

            }
        }


    }

    //Quando o jogador aperta em alguma carta
    public void AtivaCarta()
    {

        if (Input.touchCount <= 1)
        {
            if (!isHelping)
            {
                if (GameManager.instance.isSinglePLayer)
                {
                    if (PairManagerSingle.instance.cartasReveladas < 2 && !jaCliclou && PairManagerSingle.instance.allow)
                    {
                        CardAnimator.SetTrigger("Flip");
                        Invoke("TrueAtivarCarta", 0.25f);
                        if (!valorRevelado)
                        {
                            PairManagerSingle.instance.cartasReveladas += 1;
                        }
                        jaCliclou = true;
                    }
                }
                else
                {
                    if (PairManager.instance.cartasReveladas < 2 && !jaCliclou && PairManager.instance.allow)
                    {
                        CardAnimator.SetTrigger("Flip");
                        Invoke("TrueAtivarCarta", 0.25f);

                        if (!valorRevelado)
                        {
                            PairManager.instance.cartasReveladas += 1;
                        }

                        jaCliclou = true;
                    }
                }
            }
            
        }

    }

    public void TrueAtivarCarta()
    {

        if (!GameManager.instance.isSinglePLayer)
        {
            if (PairManager.instance.allow)
            {
                int rand = Random.Range(0, selectionCarta.Length);
                audioSource.PlayOneShot(selectionCarta[rand]);
 
                PairManager.instance.cartasAtivas += 1;
                PairManager.instance.allow = false;
                valorRevelado = true;
            }
        }
        else
        {
            if (PairManagerSingle.instance.allow)
            {
                int rand = Random.Range(0, selectionCarta.Length);
                audioSource.PlayOneShot(selectionCarta[rand]);

                PairManagerSingle.instance.cartasAtivas += 1;
                PairManagerSingle.instance.allow = false;
                valorRevelado = true;
            }
        }

    }

    public void CanMoveCarta()
    {
        canMove = true;
        if (play && cartaAudio != null)
        {
            audioSource.PlayOneShot(cartaAudio);
            
            play = false;
        }
    }

    public void MoveCarta()
    {
        if (canMove)
        {
            Invoke("AnimatorGoingToCard", 0f);
            transform.SetParent(CartasFeitas.transform);

            //Checa se é single player
            transform.localScale = Vector2.MoveTowards(transform.localScale, new Vector2(-0.4f,0.4f), 0.025f);
            if (!GameManager.instance.isSinglePLayer)
            {

                if (PairManager.instance.turno == PairManager.Turno.jogador1)
                {
                    distancia = Vector2.Distance(transform.position.normalized, GameObject.Find("ReachPoint1").transform.position);

                    if (distancia >= distRelativa)
                    {
                        cartaSpeed += 0.5f;

                    }

                    //Leva as cartas para o jogador 1
                    transform.position = Vector2.MoveTowards(transform.position, reachPoint1.transform.position, cartaSpeed *  Time.deltaTime);
                }
                else
                {
                    distancia = Vector2.Distance(transform.position.normalized, GameObject.Find("ReachPoint2").transform.position);

                    if (distancia >= distRelativa)
                    {
                        cartaSpeed += 0.5f;

                    }

                    //Leva as cartas para o jogador 2
                    transform.position = Vector2.MoveTowards(transform.position, reachPoint2.transform.position, cartaSpeed * Time.deltaTime);
                }

                Vector2 cartaPos = transform.position;
                Vector2 pos1 = reachPoint1.transform.position;          
                Vector2 pos2 = reachPoint2.transform.position;

                //Ao chegar no destino pontua o jogador e destroi a carta
                if (cartaPos.normalized == pos1.normalized)
                {
                    //Debug.Log("Chegou");
                    PairManager.instance.PontuarJogador();
                    PairManager.instance.ResetCartasReveladas();
                    ReachPoint0.instance.index += 1;
                    TrueDestroy();
                }
                else if (cartaPos.normalized == pos2.normalized)
                {
                    PairManager.instance.PontuarJogador();
                    PairManager.instance.ResetCartasReveladas();
                    ReachPoint0.instance.index += 1;
                    TrueDestroy();
                }

            }
            else //se for single player:
            {

                distancia = Vector2.Distance(transform.position.normalized, GameObject.Find("ReachPoint1").transform.position);

                if (distancia >= distRelativa)
                {
                    cartaSpeed += 0.5f;

                }

                //Leva as cartas para o player
                transform.position = Vector2.MoveTowards(transform.position, reachPoint1.transform.position, cartaSpeed * Time.deltaTime);

                Vector2 cartaPos = transform.position;
                Vector2 pos = reachPoint1.transform.position;

                //Ao chegar no destino pontua o jogador e destroi a carta
                if (cartaPos.normalized == pos.normalized)
                {
                    PairManagerSingle.instance.PontuarJogador();
                    PairManagerSingle.instance.ResetCartasReveladas();
                    ReachPoint0.instance.index += 1;
                    TrueDestroy();
                }
            }


        }    

    }


    public void DestroyCarta()
    {
        Invoke("TrueDestroy", 1);
    }

    public void TrueDestroy()
    {
        Destroy(gameObject);
        if (!GameManager.instance.isSinglePLayer)
        {
            PairManager.instance.pairsFeitos += 0.5f;
        }
        else
        {
            PairManagerSingle.instance.pairsFeitos += 0.5f;
        }

        //Debug.Log("Destroy");
    }

    public void FlipCartaBack()
    {
        Invoke("AnimatorFlipCartaBack", 0.75f);
        Invoke("TrueFlipCartaBack", 1);

    }

    public void TrueFlipCartaBack()
    {
        jaCliclou = false;
        valorRevelado = false;
        if (GameManager.instance.isSinglePLayer)
        {
            PairManagerSingle.instance.ResetCartasReveladas();
        }
        else
        {
            PairManager.instance.ResetCartasReveladas();
        }


        //PairManager.instance.TrocaTurno();
    }

    

    public void AnimatorFlipCartaBack()
    {
        CardAnimator.SetTrigger("Flip");
    }

    public void TrueReveal()
    {
        jaCliclou = false;
        revealTimer = 0;
        reveal = false;

        valorRevelado = false;

    }

    public void AnimatorGoingToCard()
    {
        CardAnimator.enabled = false;
    }

}
