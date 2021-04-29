using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaInimigo : MonoBehaviour
{
    ///<summary>
    ///Comentado por Gustavo - 22/04 - 10:05
    ///</summary>
    public float speed = 20f; //velocidade da blaa
    public Rigidbody2D rb; //rigid body
    public GameObject objeto; //pega como referencia a bala inimigo
    public int danoBala; //indica o valor do dano da bala
    Coroutine danoCoroutine;
    //public bool Atirador;
    Player target; //target pega player
    void Start()
    {   
        target = GameObject.FindObjectOfType<Player>(); //armazena o gmaeobject player
        Vector2 moveDirection = (target.transform.position - transform.position); //direção de movimento é igual a do player 
        Vector2 aimDirection = (moveDirection).normalized;                        //menos a a posição de onde a bala foi instanciada
        rb.velocity = aimDirection * speed;                                     //põe velocidade na bala
    }
    
    private void OnTriggerEnter2D(Collider2D col){ //se entrar na área
        
        if(col.gameObject.CompareTag("Player")) //se essa área for a do player
        {
            GameObject Enemy = col.gameObject; //põe o gameobject em enemy
            //if(Atirador)
            //{
                danoCoroutine = StartCoroutine(target.DanoCaractere(danoBala, 1.0f)); //executa a coroutine e da dano no player com dano da bala
                Destroy(objeto); //destroi a bala
           // }
        }
    }

}
