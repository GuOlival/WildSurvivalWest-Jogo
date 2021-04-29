using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
    ///<summary>
    ///Comentado por Gustavo - 22/04 - 08:15
    ///</summary>
public class Perambular : MonoBehaviour
{
    /*public float velocidadePerseguicao;    //Velocidade de persegui��o do inimigo em rela��o ao player quando o player entra na �rea de spot 
    public float velocidadePerambular;     //Velocidade que o inimigo circula pelo mapa
    float velocidadeCorrente;              //Velocidade do inimigo atribu�da*/

    public float intervaloMudancaDirecao; //Tempo para alternar a dire��o que o inimigo anda
    public bool perseguePlayer;           //Indicador de perseguidor ou n�o

    private List<Rigidbody2D> EnemyRBs;

    public float speed;

    private Transform playerPos;
    private Rigidbody2D rb;

    private float repelRange = .5f;

    Coroutine moverCoroutine;
    
    Rigidbody2D rb2D;                     //Armazena o componente RigidBody2D
    Animator animator;                    //Armazena o componente Animator

    Transform alvoTransform = null;       //Armazena o componente Transform do Alvo (player ou outro)

    Vector3 posicaoFinal;                 //Posi��o final 
    float anguloAtual = 0;                //Angulo que o inimigo usa para andar
    private Animator aimAnimator;
    CircleCollider2D circleCollider;      //Armazena o componente de Spot
    string estadoAnimacao = "EstadoAnimacao";
    public Transform firePoint;
    public float reload;
    bool podeAtirar=true;
    float oldReload;

    public bool Atirador;
    // Start is called before the first frame update
    void Start()
    {   
        oldReload = reload;       //armazena o valor de reload  
        animator = GetComponent<Animator>();    //armazena na variável o componente de naimação
        //velocidadeCorrente = velocidadePerambular;
        rb2D = GetComponent<Rigidbody2D>();     //armazena o rigid body em rb
        StartCoroutine(RotinaPerambular());   //inicia a rotina perambular
        circleCollider = GetComponent<CircleCollider2D>(); //armazena o collider circular
    }

    private void OnDrawGizmos() //função apenas para desenhar a linha de direção do movimento do inimigo
    {
        if (circleCollider != null)
        {
            Gizmos.DrawWireSphere(transform.position, circleCollider.radius);
        }
    }

    public IEnumerator RotinaPerambular()
    {
        while (true)
        {
            EscolheNovoPontoFinal(); //escolhe um ponto final
            if (moverCoroutine != null)
            {
                StopCoroutine(moverCoroutine);
            }
            moverCoroutine = StartCoroutine(Mover(rb2D, speed)); //move o rigid body com a velocidade speed
            //moverCoroutine = StartCoroutine(Mover(rb2D, velocidadeCorrente));
            yield return new WaitForSeconds(intervaloMudancaDirecao); //espera até que o intervalo acabe
        }
    }

    void EscolheNovoPontoFinal()
    {
        anguloAtual += Random.Range(0, 360);    //Define o angulo da dire��o que o inimigo andar� 
        anguloAtual = Mathf.Repeat(anguloAtual, 360); 
        posicaoFinal += Vector3ParaAngulo(anguloAtual); //adiciona na posição final o vetor com angulo gerado
    }

    //Transforma de graus para radiano
    Vector3 Vector3ParaAngulo(float anguloEntradaGraus)
    {
        float anguloEntradaRadianos = anguloEntradaGraus + Mathf.Deg2Rad; //converte em radianos o angulo
        return new Vector3(Mathf.Cos(anguloEntradaRadianos), Mathf.Sin(anguloEntradaRadianos), 0);  //Nova posi��o que o inimigo ir� andar em x (cosseno), y (seno) e z
    }

    public IEnumerator Mover(Rigidbody2D rbParaMover, float velocidade)
    {
        float distanciaFaltante = (transform.position - posicaoFinal).sqrMagnitude;  //Posi��o final - posi��o inicial, e arredondamento pelo sqrMagnitude (acerta resolu��o e dist�ncia de jogo)
        
        while (distanciaFaltante > float.Epsilon)
        {
            if (alvoTransform != null)
            {
                posicaoFinal = alvoTransform.position;
            }
            if (rbParaMover != null) 
            {
                animator.SetBool("Caminhando", true); //aciona a animação de se mover
                Vector3 novaPosicao = Vector3.MoveTowards(rbParaMover.position, posicaoFinal, velocidade * Time.deltaTime);
                rb2D.MovePosition(novaPosicao); //se move para a posição gerada
                distanciaFaltante = (transform.position - posicaoFinal).sqrMagnitude;
            }
            yield return new WaitForFixedUpdate();  //Quando tem corpo r�gido na Coroutine, tem que usar o Fixed Update
        }
        animator.SetBool("Caminhando", false);  //Se inimigo chegou ao ponto final, para de caminhar
    }

    void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform; //pega a posição do player
        rb = GetComponent<Rigidbody2D>();
        aimAnimator =/* aimTransform.*/GetComponent<Animator>();
        if (EnemyRBs == null)
        {
            EnemyRBs = new List<Rigidbody2D>();
        }

        EnemyRBs.Add(rb);
    }

    private void OnDestroy()
    {
        EnemyRBs.Remove(rb);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(rb2D.position, posicaoFinal, Color.red);
        if (Vector2.Distance(transform.position, playerPos.position) > 0.25f)
        {
            
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
        }
        if(podeAtirar == false){ //verifica se pode atacar, se não continua a contar no contador
            reload -= Time.deltaTime;
        }   
        if(reload < 0){ //se o contador terminou
            podeAtirar =true; //permite atirar
            reload = oldReload; //e armazena o valor de reload novamente
        }
    }
    public GameObject bulletPrefab;
    void Shoot()
    {
        Instantiate(bulletPrefab,firePoint.position,firePoint.rotation); //atira um projetil
        aimAnimator.SetInteger(estadoAnimacao, 3); //muda a animação para ataque
        podeAtirar = false; //e não poderá atirar, até que o temporizador finalize
    }

    void FixedUpdate()
    {
        Vector2 repelForce = Vector2.zero;
        foreach (Rigidbody2D enemy in EnemyRBs)
        {
            if (enemy == rb)
            {
                continue;
            }
            if (Vector2.Distance(enemy.position, rb.position) <= repelRange)
            {
                Vector2 repelDir = (rb.position - enemy.position).normalized;
                repelForce += repelDir;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && perseguePlayer) //caso entre em uma área e for o player
        {
            //if(podeAtirar == true){ //e se puder atacar
           // Shoot(); //ataca
           // }
            //velocidadeCorrente = velocidadePerseguicao;
            alvoTransform = collision.gameObject.transform; //Pega onde os colliders se tocaram
            
            if (moverCoroutine != null)
            {
                StopCoroutine(moverCoroutine);
            }
            moverCoroutine = StartCoroutine(Mover(rb2D, speed));
            //moverCoroutine = StartCoroutine(Mover(rb2D, velocidadeCorrente));
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(podeAtirar && Atirador && collision.gameObject.CompareTag("Player"))
        {
            Shoot();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //quando sair da área
    {
        if (collision.gameObject.CompareTag("Player")) //e essa área for do player
        {
            animator.SetBool("Caminhando", false);  //Se a colis�o acabou (player fugiu), para de perseguir e volta a perambular
            //velocidadeCorrente = velocidadePerambular;
            if (moverCoroutine != null)
            {
                StopCoroutine(moverCoroutine);
            }
            alvoTransform = null;
        }
    }
}
