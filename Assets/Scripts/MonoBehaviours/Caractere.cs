using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    ///<summary>
    ///Comentado por Gustavo - 22/04 - 09:40
    ///</summary>
// Abstract indica que a classe n�o pode ser instanciada, e sim herdada
public abstract class Caractere : MonoBehaviour
{
    //public int PontosDano;              //Vers�o anterior do valor de "dano" 
    //public PontosDano pontosDano;         //Novo tipo que tem o valor do objeto script (Ponto de Contato)
    public float inicioPontosDano;        //Valor m�nimo inicial de sa�de do Player
    //public int MaxPontosDano;           //Vers�o anterior do valor m�ximo de dano
    public float MaxPontosDano;           //Valor m�ximo permitido de sa�de do Player

    public abstract void ResetCaractere();

    public abstract IEnumerator DanoCaractere(int dano, float intervalo);
    public virtual IEnumerator EfeitoFlicker()
    {
        GetComponent<SpriteRenderer>().color = Color.red; //pega a cor da sprite e muda pra vermelho
        yield return new WaitForSeconds(0.1f);          //espera 0.1 segundos
        GetComponent<SpriteRenderer>().color = Color.white; //muda pra branco
    }
    //Quando o personagem morre
    public virtual void KillCaractere()
    {
        Destroy(gameObject); //Destr�i o Game Object do personagem
    }
}
