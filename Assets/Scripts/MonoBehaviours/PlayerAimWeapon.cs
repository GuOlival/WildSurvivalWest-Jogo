using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
    ///<summary>
    ///Comentado por Gustavo - 22/04 - 13:00
    ///</summary>
public class PlayerAimWeapon : MonoBehaviour
{

    private Transform aimTransform; //posição da mira
    private Animator aimAnimator; //animação na variavel
    
    public Transform firePoint; //posição de onde sai o tiro
    public GameObject bulletPrefab; //prefab da bala que vai ser instanciada
    private Transform aimGunEndPointTransform; //ponto de onde sai a bala
    string estadoAnimacao = "EstadoAnimacao"; 
    float angle; //angulo da mira
    public float reload; //tempo de recarregamento
    float oldReload; //tempo de recarregamento
    bool podeAtirar = true; //variavel que via verificar se pode atirar ou não
    
    public Transform pointWeapon; //posição da ponta da arma
    public float weaponSpeed = 300; //velocidade da blaa

    public int municao; //quantiadade de munição
    public static int qtdTiros; //variável que vai ser utilizada em controle Balas


    private void Awake()
    {   
        oldReload = reload; //armazena o valor do tempo de reload
       
        aimAnimator =GetComponent<Animator>(); //põe na variável o componente de animação
        aimGunEndPointTransform = transform.Find("GunEndPointPosition"); //põe na variavel e põe o gameobject que serve para indicar de onde sai o tiro
    }
    
    private void Update(){
        HandleAiming();
        HandleShooting();
        if(podeAtirar == false){
            reload -= Time.deltaTime; // se ele ainda ta em reload, vai diminuindo do temporizador
        }
        if(reload < 0){ //se acabou o tempo de reload
            podeAtirar = true; //pode atirar
            reload = oldReload; //reload recebe o valor antigo
            qtdTiros = 0; //quantidade de tiros feitas volta a ser 0
        }
        if(qtdTiros == municao){ //se a quantidade de tiros realizados for igual a munição, chegou hora de recarregar
            podeAtirar = false; //e portanto não pode atirar
            
        }

    }

    private void HandleAiming(){
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition(); //armazena a posição do mouse
        Vector3 aimDirection = (mousePosition - transform.position).normalized; //armazena a direção para onde quer atirar
        angle = Mathf.Atan2(aimDirection.y, aimDirection.x)*Mathf.Rad2Deg; //pega o angulo dessa direção
        
    }
    public AudioSource som;
    private void HandleShooting(){
        /*nessa seção, se for pressionado o botão do mouse e utilizando o angulo que foi armazenado em angle
        poderá saber qual animação executar. Por Ex.: se tiver mirando pra cima levemente pra direita
        executa a animação de atirar-cima-direita. Além disso, quando disparado, adiciona mais 1 na quantidade de tiros feitas. */
        if(Input.GetMouseButtonDown(0) && (angle >60 && angle < 120) && podeAtirar){ 
            aimAnimator.SetInteger(estadoAnimacao, 10);                               
            Shoot();
            qtdTiros++;
        }
        if(Input.GetMouseButtonDown(0) && (angle <=30 && angle > -30) && podeAtirar){ //atira pra direita    
            aimAnimator.SetInteger(estadoAnimacao, 13);
            Shoot();
            qtdTiros++;
        }
        if(Input.GetMouseButtonDown(0) && (angle < (-60) && angle > -120) && podeAtirar){ //atira pra baixo
            aimAnimator.SetInteger(estadoAnimacao, 11);
            Shoot();
            qtdTiros++;
        }
       if(Input.GetMouseButtonDown(0) && (angle > 150 || angle <= -150) && podeAtirar){ //atira pra esquerda
            aimAnimator.SetInteger(estadoAnimacao, 12);
            
            Shoot();
            qtdTiros++;
        } 
        if(Input.GetMouseButtonDown(0) && (angle < (60) && angle > 30) && podeAtirar){ //atira pra cima direita
            aimAnimator.SetInteger(estadoAnimacao, 16);
            Shoot();
            qtdTiros++;
        }
        if(Input.GetMouseButtonDown(0) && (angle < (150) && angle > 120) && podeAtirar){ //atira pra cima esquerda
            aimAnimator.SetInteger(estadoAnimacao, 17);
            Shoot();
            qtdTiros++;
        }        
        if(Input.GetMouseButtonDown(0) && (angle < (-30) && angle > -60) && podeAtirar){ //atira pra baixo direita
            aimAnimator.SetInteger(estadoAnimacao, 14);
            Shoot();
            qtdTiros++;
        }
        if(Input.GetMouseButtonDown(0) && (angle < (-120) && angle > -150) && podeAtirar){ //atira pra baixo esquerda
            aimAnimator.SetInteger(estadoAnimacao, 15);
            Shoot();
            qtdTiros++;
        }        
    }
    void Shoot(){
        Instantiate(bulletPrefab,firePoint.position,firePoint.rotation); //será instanciado o objeto de bala na posição da saida de tiro
        som = GetComponent<AudioSource>();                              //e será executado em seguida o som de saida.
        som.Play(0);
    }
    
}
