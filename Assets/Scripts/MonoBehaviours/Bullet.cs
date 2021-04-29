using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
    ///<summary>
    ///Comentado por Gustavo - 22/04 - 10:10
    ///</summary>
public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public GameObject objeto;
    public int danoBala;
    Coroutine danoCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        rb.velocity = aimDirection * speed;
    }

    public AudioSource somHit;
    private void OnTriggerEnter2D(Collider2D col){
        
        if(col.gameObject.CompareTag("Inimigo") && col.GetType() == typeof(BoxCollider2D))   //Se a bala atingir uma parede de colisão do tipo
        {                                                                                     // Box, e tiver a tag inimigo, executa a função
            GameObject Enemy = col.gameObject;
            Inimigo inimigoScript = Enemy.GetComponent<Inimigo>();                          //nessa execução, pega a quantidade de vida do inimigo 
            //inimigoScript.pontosVida -= danoBala;                                           //atingido e tira dele com o valor do dano da Bala
            danoCoroutine = StartCoroutine(inimigoScript.DanoCaractere(danoBala,0.2f));
            somHit.Play();                                                                  //em seguida é se destroi.
            Destroy(objeto);
        }
    }


}
