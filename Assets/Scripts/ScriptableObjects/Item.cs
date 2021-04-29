using UnityEngine;

[CreateAssetMenu(menuName = "Item")] //Dentro do menu asset, do project asset cria um menu a mais chamado Item

//Cria objetos de programação sem herdar de MonoBehaviour
public class Item : ScriptableObject
{
    public string NomeObjeto;
    public Sprite sprite;
    public int quantidade;
    public bool empilhavel;

    //Nome de inteiros
    public enum TipoItem
    {
        MOEDA, // 0
        HEALTH // 1
    }

    public TipoItem tipoItem;

    
}
