using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageButton : MonoBehaviour
{
    //Bot�o inicia o jogo 
    public void StartJogo()
    {
        SceneManager.LoadScene("Game_Scene"); //muda a cena para a do jogo principal
        Time.timeScale = 1f;
    }
    
    //Bot�o abre a tela inicial
    public void MainMenu()
    {
        SceneManager.LoadScene("Start_Scene"); //muda para cena de inicio do menu
    }
    
    //Bot�o abre a cena de cr�ditos para sair do jogo
    public void SairdoJogo()
    {
        SceneManager.LoadScene("Creditos_Scene"); //muda para a cena de credito 
    }
    //Bot�o abre as instru��es do jogo
    public void Instrucoes()
    {
        SceneManager.LoadScene("Instrucao_Scene"); //muda para  cena de instruções
    }
}
