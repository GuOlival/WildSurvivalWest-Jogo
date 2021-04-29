using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    ///<summary>
    ///Comentado por Bianca - 22/04 - 14:05
    ///</summary>
public class PontoSpawn : MonoBehaviour
{
    public GameObject prefabParaSpawn;  //

    //public Transform[] spawnPoints; //Armazena os m�ltiplos pontos de spawn 
    public float intervaloRepeticao;    //Armazena o tempo para spawnar

    void Start()
    {    
        if (intervaloRepeticao > 0) //se houver um intervalo para repetir
        {
            SpawnO(); //executa função de spawn
            InvokeRepeating("SpawnO", 0f, intervaloRepeticao); //faz a instanciação repetidamente de acordo com o intervalo dado
        }    
    }

    public GameObject SpawnO()
    {
        //Se o GameObject n�o for nulo, vai instanciar
        if (prefabParaSpawn != null)
        {
                                      //Pega um spawn point aleat�rio
            return Instantiate(prefabParaSpawn, transform.position, Quaternion.identity); //retorna o gamebobject a ser respawnado
        }

        return null;
    }
    // Update is called once per frame
    void Update()
    {
         
    }
}
