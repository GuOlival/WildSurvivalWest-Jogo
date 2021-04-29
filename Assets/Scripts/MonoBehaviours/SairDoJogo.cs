using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
    ///<summary>
    ///Comentado por Bianca - 22/04 - 14:20
    ///</summary>
public class SairDoJogo : MonoBehaviour
{
    public float timerFechaJogo = 5f;   //Vari�vel com o tempo de fechamento do jogo em segundos

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timerFechaJogo -= Time.deltaTime;   //A cada update, retira o tempo passado do tempo restante
        //Se o contador chegar a 0, fecha o jogo
        if (timerFechaJogo <= 0)
        {
            Application.Quit(); //Fecha o jogo
        }
        //Se o jogador clicar com o bot�o esquerdo do mouse ou pressionar a tecla esc, sai do jogo
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit(); //Fecha o jogo
        }
    }
}


