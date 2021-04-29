using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    ///<summary>
    ///Comentado por Bianca - 21/04 - 15:00
    ///</summary>
public class EnemyFollow : MonoBehaviour
{
    private List<Rigidbody2D> EnemyRBs;

    public float speed;

    private Transform playerPos;
    private Rigidbody2D rb;

    private float repelRange = .5f;

    void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform; //pega a posição do player
        rb = GetComponent<Rigidbody2D>(); //pega a componente de rigid body

        if(EnemyRBs == null) //se a lista de rigid bodies for vazio
        { 
            EnemyRBs = new List<Rigidbody2D>(); //cria uma nova lista 
        }

        EnemyRBs.Add(rb); //adiciona o rigid body na lsita
    }

    private void OnDestroy() //quando for destruido
    {
        EnemyRBs.Remove(rb); //remove o rigid body da lista
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, playerPos.position) > 0.25f) //se a distancia entre o player e o inimigo for maior que 0
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime); //começa a se deslocar
                                                                                                                      //atras do player
        }
    }

    void FixedUpdate()
    {
        Vector2 repelForce = Vector2.zero;
        foreach(Rigidbody2D enemy in EnemyRBs) //para cada rigidbody de inimigo presente na lista 
        {
            if(enemy == rb)
            {
                continue;
            }
            if(Vector2.Distance(enemy.position, rb.position) <= repelRange) //começam a se repelir
            {
                Vector2 repelDir = (rb.position - enemy.position).normalized;
                repelForce += repelDir;
            } 
        }
    }
}
