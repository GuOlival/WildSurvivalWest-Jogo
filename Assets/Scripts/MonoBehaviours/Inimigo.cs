using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    ///<summary>
    ///Comentado por Gustavo - 19/04 - 16:40
    ///</summary>
public class Inimigo : Caractere
{
    public float pontosVida;       //Equivalente � sa�de do inimigo
    public int forcaDano;   //Poder de dano
    public GameObject Enemy; //usa inimigo como enemy
    Coroutine danoCoroutine;
    
    string estadoAnimacao = "EstadoAnimacao";
    private Animator aimAnimator; //usa animação com essa variável
    
   
    private Rigidbody2D rb; //usa o rigidbody em rb
    void Start()
    {   
        
        aimAnimator = GetComponent<Animator>(); //pega componente de animação do Inimigo
    }
   
    public AudioSource somMorte;
    void Update()
    {
        if(pontosVida == 0){ //quando a vida do inimigo chegar a zero, executa
            
            
        }
    }
    void TocaSom(){
        somMorte.Play(0); //toca som de morte
    }

    void InimigoMorre(){
        
        Destroy(Enemy); //destroi inimigo
    }


    void InimigoParouAtaque(){
        aimAnimator.SetInteger(estadoAnimacao,0);   //quando termina a animação de ataque, executa essa função
    }


    private void OnCollisionEnter2D(Collision2D collision) //quando entrar no sensor de colisão 
    {
        if (collision.gameObject.CompareTag("Player")) //verifica se é o player 
        {
            Player player = collision.gameObject.GetComponent<Player>();
            
            if (danoCoroutine == null)
            {
                aimAnimator.SetInteger(estadoAnimacao,3);
                danoCoroutine = StartCoroutine(player.DanoCaractere(forcaDano, 1.03f)); //da dano no player
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision) //quando sair da área de colisão do player
    {
        if (collision.gameObject.CompareTag("Player")) //se foi o player que saiu
        {
            if (danoCoroutine != null)
            {
                StopCoroutine(danoCoroutine); //para de dar dano 
                danoCoroutine = null;
            }
        }
    }

    public override IEnumerator DanoCaractere(int dano, float intervalo) //função para dar dano no player
    {
        while (true)
        {
            pontosVida = pontosVida - dano; //diminui os valores de pontos de vida
            StartCoroutine(EfeitoFlicker());
            if (pontosVida <= float.Epsilon) //se for menor que zero
            {
                TocaSom();    
                aimAnimator.SetInteger(estadoAnimacao,5); //troca a animação para animação de morte
                yield return new WaitForSeconds(0.15f);
                KillCaractere(); //mata
                break;
            }
            if (intervalo > float.Epsilon)
            {
                yield return new WaitForSeconds(intervalo); // espera até dar o intervalo
            }
            else
            {
                break;
            }
        }
    }
    
    private void OnEnable()
    {
        ResetCaractere(); //reseta os pontos de vida
    }

    public override void ResetCaractere()
    {
        pontosVida = inicioPontosDano; //reseta os pontos de vida
    }
}
