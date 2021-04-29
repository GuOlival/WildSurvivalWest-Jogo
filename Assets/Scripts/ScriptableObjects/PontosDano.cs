using UnityEngine;

[CreateAssetMenu(menuName = "PontosDano")] //Dentro do menu asset, do project asset cria um menu a mais chamado PontosDano

//Cria objetos de programa��o sem herdar de MonoBehaviour
public class PontosDano : ScriptableObject
{
    public float valor;     //Armazena quanto vale o objeto script

}
