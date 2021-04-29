using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
    ///<summary>
    ///Comentado por Gustavo - 22/04 - 08:40
    ///</summary>
public class Player : Caractere
{
    public HealthBar healthBarPrefab;   //Refer�ncia ao objeto prefab criado da HealthBar
    HealthBar healthBar;

    public PontosDano pontosDano;         //Tem o valor da "sa�de" do objeto 
    private Animator aimAnimator;
    string estadoAnimacao = "EstadoAnimacao";
    private void Start()
    {
        pontosDano.valor = inicioPontosDano;
        healthBar = Instantiate(healthBarPrefab);
        healthBar.caractere = this;
    }

    // Atualiza��o da tela por FPS (frame)
    void Update()
    {
 
    }

    public override IEnumerator DanoCaractere (int dano, float intervalo)
    {
        while (true)
        {
            pontosDano.valor = pontosDano.valor - dano; //tira vida com base no dano
            StartCoroutine(EfeitoFlicker());
            if (pontosDano.valor <= float.Epsilon) //se a vida for menor que 0
            {
                KillCaractere(); //morre
                break;
            }
            if (intervalo > float.Epsilon)
            {
                yield return new WaitForSeconds(intervalo); //espera intervalo acabar
            }
            else
            {
                break;
            }
        }
    }

    public override void ResetCaractere() //reseta personagem
    {
        healthBar = Instantiate(healthBarPrefab); //instancia healthbar
        healthBar.caractere = this; 
        pontosDano.valor = inicioPontosDano; //pontos de vida é igual ao inicial
    }
    Rigidbody2D rb;
    //Personagem morre
    public static bool podeMovimentar =true; //variavel usada na movimentação do player, quando o player morre, não o permite mais andar
    public AudioSource somMorte;
    public override void KillCaractere() //mata personagem
    {       
        podeMovimentar = false; //não pode mais se movimentar
        aimAnimator = GetComponent<Animator>(); //pega componente de animação
        somMorte.Play(0); //toca som da morte
        aimAnimator.SetInteger(estadoAnimacao, 18); //muda a animação do player para a de morte
        //base.KillCaractere();           //Mata o personagem
          //Destr�i a Health Bar
    }
    void GameOver(){ //função executa quando acaba animação de morte do player
        //base.KillCaractere();
        Destroy(healthBar.gameObject); //destroi a healthbar
        podeMovimentar = true; //pode se movimentar novamente, já que caso o jogador queira tentar mais uma vez possa se movimentar
        SceneManager.LoadScene("GameOver_Scene");//Vai para a tela de Fim de Jogo
        
    }

    private void OnTriggerEnter2D (Collider2D collision) //entrando em uma área
    {
        //Se o Game Object que colidiu for um Coletavel
        if (collision.gameObject.CompareTag("Coletavel"))
        {
            Item DanoObjeto = collision.gameObject.GetComponent<Consumivel>().item; //
            if (DanoObjeto != null)
            {
                bool DeveDesaparecer = false;
                //print("Acertou: " + DanoObjeto.NomeObjeto); //Se o personagem colidir com o colet�vel, printa no console "Acertou: objeto"
                switch (DanoObjeto.tipoItem)
                {
                    case Item.TipoItem.HEALTH: //se for um coração
                        DeveDesaparecer = AjustePontosDano(DanoObjeto.quantidade); //ajusta os pontos de vida 
                        break;
                    default:
                        break;
                }
                if (DeveDesaparecer)
                {
                    collision.gameObject.SetActive(false); //desativa gameobject
                }
                collision.gameObject.SetActive(false);  //Desativa o GameObject que coletou da tela
            }
        }
    }
    
    public bool AjustePontosDano(int quantidade) //função para ajustar pontos de dano
    {
        if (pontosDano.valor < MaxPontosDano) //executa apenas se o valor atual for menor que o valor maximo de pontos
        {
            pontosDano.valor = pontosDano.valor + quantidade; // adiciona a quantidade de vida que o coração da
            print("Ajustando PD por: " + quantidade + ". Novo Valor = " + pontosDano.valor);
            return true;
        }
        else return false;
    }

}
