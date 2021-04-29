using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ArredondaPosCamera : CinemachineExtension
{
        ///<summary>
    ///Comentado por Gustavo - 22/04 - 09:00
    ///</summary>
    public float PixelsPerUnit = 32; // variável que represneta a proporção utilizada em pixels no jogo todo

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,                      //variável que vai representar a camera
        CinemachineCore.Stage stage, ref CameraState state,     //cria variavel stage que representa o stage e o state da camera 
        float deltaTime)                

    {
        if (stage == CinemachineCore.Stage.Body)
        {
            Vector3 pos = state.FinalPosition;          //define uma variavel posição que vai ser a posição final do estado
            Vector3 pos2 = new Vector3(Round(pos.x), Round(pos.y), pos.z);      //arredonda as posições da variável anterior em um outro vetor 
            state.PositionCorrection += pos2 - pos;         //e corrige com o arredondamento
        }
    }

    float Round (float x)
    {
        return Mathf.Round(x * PixelsPerUnit) / PixelsPerUnit;  //retorna o arredondamento utilizando função do Mathf
    }
}
