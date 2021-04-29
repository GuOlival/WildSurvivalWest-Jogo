using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
    ///<summary>
    ///Comentado por Bianca - 18/04 - 18:30
    ///</summary>
public class MostraWave: MonoBehaviour
{
    string ultimaWave;                       // Guarda o record da quantidade de waves jogadas
    AudioSource somVitoria;                  // Som de novo record
    
    // Start is called before the first frame update
    void Start()
    {
        somVitoria = GetComponent<AudioSource>();               // Pega o GameObject do som de novo record
        somVitoria.Play();                                      // Toca o som de novo record

        string nomeDaCena = SceneManager.GetActiveScene().name; // Pega o nome da cena que est�
        if (nomeDaCena == "Final_Scene")
        {
            ultimaWave = PlayerPrefs.GetString("WaveStop");              // Vai pegar a �ltima wave jogada
            GameObject.Find("WaveStop").GetComponent<Text>().text = ultimaWave;    // Mostra a �ltima wave que jogou at� morrer
        }
    }
}
