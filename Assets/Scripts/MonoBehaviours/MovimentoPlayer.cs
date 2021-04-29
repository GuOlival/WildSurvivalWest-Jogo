using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    ///<summary>
    ///Comentado por Bianca - 17/04 - 14:50
    ///</summary>
public class MovimentoPlayer : MonoBehaviour
{
    public float velocidadeMovimento = 3.0f; //Equivale ao momento (impulso) a ser dado ao player
    Vector2 Movimento = new Vector2();       //Detecta movimento pelo teclado

    string estadoAnimacao = "EstadoAnimacao";//Variavel que guarda o nome do parâmetro de Animação

    Animator animator;                       //Guarda a componente do controle de animação do Player
    Rigidbody2D rb2D;                        //Guarda o componente Corpo Rígido do Player

    //Dá nome aos inteiros (estados de animação)
    enum EstadosCaractere
    {
        andaLeste = 1,      //Seta o estado de animação para 1, anda para o leste
        andaOeste = 2,      //Seta o estado de animação para 2, anda para o oeste
        andaNorte = 3,      //Seta o estado de animação para 3, anda para o norte
        andaSul = 4,        //Seta o estado de animação para 4, anda para o sul
        idle = 5            //Seta o estado de animação para 5, volta para o idle (parado)
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); //Pega o componente Animator do Player
        rb2D = GetComponent<Rigidbody2D>();  //Pega o componente RigidBody do Player
    }

    // Atualização da tela por FPS (frame)
    void Update()
    {
        if(Player.podeMovimentar)
         {
             UpdateEstado();     //Função muda a animação do player de acordo com o input do usuário
         }
    }

    //Updates em intervalo de tempo fixo para eventos dinâmicos 
    private void FixedUpdate()
    {
        MoveCaractere();    //Função pega o input do usuário e move o personagem
    }
    private static bool podeMovimentar;
    //Pega o input do usuário e move o personagem
    private void MoveCaractere()
    {
        if(Player.podeMovimentar){      //pegando do script Player, identifica se pode se movimentar
            Movimento.x = Input.GetAxisRaw("Horizontal");       //Pega o input do usuário das setas "horizontais" do teclado. Se apertar a seta para a direta, retorna 1. Se apertar a seta para a esquerda, retorna -1.
            Movimento.y = Input.GetAxisRaw("Vertical");         //Pega o input do usuário das setas "verticais" do teclado. Se apertar a seta para a cima, retorna 1. Se apertar a seta para a baixo, retorna -1.
            Movimento.Normalize();                              //Se der algum outro número, normaliza para 1
            rb2D.velocity = Movimento * velocidadeMovimento;    //Velocidade do player, dado pelo impulso 
        }
    }

    //Função muda a animação do player de acordo com o input do usuário
    private void UpdateEstado()
    {
        //Se o input do usuário foi seta para a direta
        if(Movimento.x > 0)
        {
            animator.SetInteger(estadoAnimacao, (int)EstadosCaractere.andaLeste); //Muda o número da variável "EstadoAnimacao" do animator para 1 (andaLeste)
        }
        //Se o input do usuário foi seta para a esquerda
        else if (Movimento.x < 0) 
        {
            animator.SetInteger(estadoAnimacao, (int)EstadosCaractere.andaOeste); //Muda o número da variável "EstadoAnimacao" do animator para 2 (andaOeste)
        }
        //Se o input do usuário foi seta para cima
        else if (Movimento.y > 0)
        {
            animator.SetInteger(estadoAnimacao, (int)EstadosCaractere.andaNorte); //Muda o número da variável "EstadoAnimacao" do animator para 3 (andaNorte)
        }
        //Se o input do usuário foi seta para baixo
        else if (Movimento.y < 0)
        {
            animator.SetInteger(estadoAnimacao, (int)EstadosCaractere.andaSul);   //Muda o número da variável "EstadoAnimacao" do animator para 4 (andaSul)
        }
        //Se não tiver input do usuário
        else
        {
            if(Player.podeMovimentar){
                animator.SetInteger(estadoAnimacao, (int)EstadosCaractere.idle);      //Muda o número da variável "EstadoAnimacao" do animator para 5 (idle)
            }
        }
    }
}
