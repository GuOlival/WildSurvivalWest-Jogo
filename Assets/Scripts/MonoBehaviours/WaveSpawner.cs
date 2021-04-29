using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
    ///<summary>
    ///Comentado por Bianca - 21/04 - 13:05
    ///</summary>
public class Wave
{
    public string waveName; //Nome da "onda" de inimigos
    public int noOfEnemies; //Quantos inimigos quero spawnar por onda
    public GameObject[] typeOfEnemies; //Armazena os Game Objects dos diferentes tipos de inimigo
    public float spawnInterval; //Armazena o intervalo para spawnar cada inimigo
}

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;  //Armazena os m�ltiplos nomes das ondas
    public Transform[] spawnPoints; //Armazena os m�ltiplos pontos de spawn do inimigo
    public Animator animator;   //Armazena o item Animator
    public Text WaveName;       //Armazena o item Texto

    private Wave currentWave;   //Armazena a onda de inimigos atual
    private int currentWaveNumber; //Onda de inimigos atual
    private float nextSpawnTime;   //Armazena o tempo para spawnar outro inimigo

    private bool canSpawn = true; //Armazena se pode ou n�o spawnar inimigos
    private bool canAnimate = false; //Armazena se pode ou n�o mostrar o texto da pr�xima onda na tela

    private void Update()
    {
        currentWave = waves[currentWaveNumber]; //Onda de inimigos atual
        SpawnWave();                            //Spawna os inimigos aleatoriamente conforme quantidade de inimigos na onda
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Inimigo");    //Quantos inimigos foram spawnados (localizados pela tag "Inimigos")

        //Se o n�mero total de inimigos for 0, ou seja, se todos os inimigos foram mortos
        if(totalEnemies.Length == 0)
        {
            //Se tiver acabado de spawnar todos os inimigos da onda
            //if(!canSpawn)
            //{
                //Se n�o ultrapassar o n�mero total de ondas
                if (currentWaveNumber + 1 != waves.Length)
                {
                    //Se puder mostrar o texto de pr�xima onda, vai para a pr�xima onda
                    if (canAnimate)
                    {
                        WaveName.text = waves[currentWaveNumber + 1].waveName;    //Pega o nome da pr�xima onda 
                        //PlayerPrefs.SetString("WaveStop", WaveName);              //Guarda o nome da �ltima wave
                        animator.SetTrigger("WaveComplete");                      //Chama o texto que indica o come�o da pr�xima onda
                        canAnimate = false;                                       //Permite mostrar o texto do final da onda
                    }
                }
                //Se n�o, � o final do jogo
                else
                {
                    //Debug.Log("Fim de Jogo");
                    SceneManager.LoadScene("Final_Scene");  //Vai para a tela de finaliza��o
                }
            //}
        }

    }

    //Indica que pode ir para a pr�xima onda
    void SpawnNextWave()
    {
        currentWaveNumber++; //Atualiza para a pr�xima onda
        canSpawn = true;     //Permite spawnar outros inimigos
    }

    //Pega um inimigo aleat�rio em uma posi��o aleat�ria para gerar uma onda 
    void SpawnWave()
    {
        //Se tiver ondas de inimigos restantes e tiver dado o tempo para spawnar outro inimigo 
        if (canSpawn && nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];  //Pega um Game Object de um tipo de inimigo aleat�rio
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];                               //Pega um spawn point aleat�rio

            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            currentWave.noOfEnemies--; //Cada vez que spawna 1 inimigo, diminui no contador
            nextSpawnTime = Time.time + currentWave.spawnInterval;  //O pr�ximo spawn de inimigo ser� de acordo com o tempo determinado para spawnar outro inimigo e o tempo que se passou

            //Se a quantidade de inimigos da onda acabar, para de spawnar
            if(currentWave.noOfEnemies == 0)
            {
                canSpawn = false;   //Para de spawnar inimigos
                canAnimate = true;  //Mostra o texto da pr�xima onda na tela
            }
        }
    }
}
