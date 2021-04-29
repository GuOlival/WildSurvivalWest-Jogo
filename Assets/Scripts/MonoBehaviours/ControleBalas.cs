using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
    ///<summary>
    ///Comentado por Gustavo - 22/04 - 14:00
    ///</summary>
public class ControleBalas : MonoBehaviour
{
    public GameObject bala1, bala2, bala3, bala4, bala5, bala6; //pega objetos das balas na tela
    //public int qtdBalas;
    public GameObject player;
    private static int tirosRealizados; //será uma variável que pegará valores de outra função
    bool Recarregando = false;
    //Player scriptPlayer;
    void Awake(){
        
        
        //tirosRealizados = scriptPlayer.qtdTiros;
    }
    float novoTimer;
    void Start(){
        novoTimer = timer; //armazena o valor do timer 
        bala1.gameObject.SetActive (true); //deixa todas as balas na tela ativas no inicio
        bala2.gameObject.SetActive (true);
        bala3.gameObject.SetActive (true);
        bala4.gameObject.SetActive (true);
        bala5.gameObject.SetActive (true);
        bala6.gameObject.SetActive (true);

    }

    void Update(){
        switch(tirosRealizados){ // verifica a quantidade de tiros realizados
            case 6:             //caso forem 6, deixará todas as balas na tela desativadas 
                bala1.gameObject.SetActive (false);
                bala2.gameObject.SetActive (false);
                bala3.gameObject.SetActive (false);
                bala4.gameObject.SetActive (false);
                bala5.gameObject.SetActive (false);
                bala6.gameObject.SetActive (false);
                Recarregando = true;        //nesse momento, chegou a hora de recarregar
                break;
            case 5:
                bala1.gameObject.SetActive (true);//deixa apenas 1 bala na tela, com 5 balas disparadas
                bala2.gameObject.SetActive (false);
                bala3.gameObject.SetActive (false);
                bala4.gameObject.SetActive (false);
                bala5.gameObject.SetActive (false);
                bala6.gameObject.SetActive (false);
                break;
            case 4:
                bala1.gameObject.SetActive (true);//2 balas na tela, com 4 disparadas
                bala2.gameObject.SetActive (true);
                bala3.gameObject.SetActive (false);
                bala4.gameObject.SetActive (false);
                bala5.gameObject.SetActive (false);
                bala6.gameObject.SetActive (false);  
                break; 
            case 3:
                bala1.gameObject.SetActive (true);//3 balas na tela, com 3 disparadas
                bala2.gameObject.SetActive (true);
                bala3.gameObject.SetActive (true);
                bala4.gameObject.SetActive (false);
                bala5.gameObject.SetActive (false);
                bala6.gameObject.SetActive (false);  
                break;
            case 2:
                bala1.gameObject.SetActive (true);//4 balas na tela, com 2 disparadas
                bala2.gameObject.SetActive (true);
                bala3.gameObject.SetActive (true);
                bala4.gameObject.SetActive (true);
                bala5.gameObject.SetActive (false);
                bala6.gameObject.SetActive (false);  
                break;
            case 1:
                bala1.gameObject.SetActive (true);//5 balas na tela, com 1 disparada
                bala2.gameObject.SetActive (true);
                bala3.gameObject.SetActive (true);
                bala4.gameObject.SetActive (true);
                bala5.gameObject.SetActive (true);
                bala6.gameObject.SetActive (false);  
                break;
            case 0:
                bala1.gameObject.SetActive (true);//6 balas na tela quando não há bala disparada
                bala2.gameObject.SetActive (true);
                bala3.gameObject.SetActive (true);
                bala4.gameObject.SetActive (true);
                bala5.gameObject.SetActive (true);
                bala6.gameObject.SetActive (true);  
                Recarregando = false;             //nesse momento não é necessário recarregar
                break;
        }
        tirosRealizados = PlayerAimWeapon.qtdTiros; //pega a quantidade de tiros realizado no script PlayerAimweapon
        if(Recarregando){ //se estiver recarregando
           Recarregar(); //executa recarregar
            
        }
        if(!Recarregando){ //se não estiver recarregando
            timer = 2.0f; //mantém o valor do timer atualizado
        }
    }


    float timer = 2f;
    float timerFixo = 2f;
    void Recarregar(){
        
        timer -= Time.deltaTime;            //começa um temporizador
        /*enquanto o tempo vai passando, as balas vão aparecendo uma por uma na tela, indicando 
        que a arma está sendo recarregada*/
        if(timer < timerFixo && timer > timerFixo * 0.833){  //se estiver logo no inicio, deixará apenas a primeira bala ativa
            bala1.gameObject.SetActive (true);
        }
        else if(timer < timerFixo * 0.833 && timer > timerFixo * 0.666){
            bala2.gameObject.SetActive (true);
        }
        else if(timer < timerFixo * 0.666 && timer > timerFixo * 0.5){
            bala3.gameObject.SetActive (true);
        }
        else if(timer < timerFixo * 0.5 && timer > timerFixo * 0.333){
            bala4.gameObject.SetActive (true);
        }
        else if(timer < timerFixo * 0.333 && timer > timerFixo * 0.166){
            bala5.gameObject.SetActive (true);
        }
        else if(timer < timerFixo * 0.166){             //se estiver no final, deixará apenas a última bala ativa
            bala6.gameObject.SetActive (true);
            
        }        
        else if(timer < 0){
            Recarregando = false; //qunado timer acabar, não estará mais recarregando
        }

    
    }

}
