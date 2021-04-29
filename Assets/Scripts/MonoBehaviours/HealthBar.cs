using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
        ///<summary>
    ///Comentado por Bianca - 20/04 - 20:00
    ///</summary>
    public PontosDano pontosDano;   //Objeto de leitura dos dados de quantos pontos tem o Player
    public Player caractere;        //Receber� o objeto do Player
    public Image medidorImagem;     //Recebe a barra de medi��o de vida
    float maxPontosDano;            //Armazena a quantidade limite de sa�de do Player

    // Start is called before the first frame update
    void Start()
    {
        maxPontosDano = caractere.MaxPontosDano; //quantidade de vida será o valor MaxPontos dano em caractere
    }

    // Update is called once per frame
    void Update()
    {
        if (caractere != null)
        {
            medidorImagem.fillAmount = pontosDano.valor / maxPontosDano;    //Rela��o percentual entre os pontos de dano e o m�ximo de pontos de dano, alterando a barra de vida vista pelo Player
            

        }
    }
}
