using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGGameManager : MonoBehaviour
{
    public static RPGGameManager InstanciaCompartilhada = null;
    public RPGCameraManager cameraManager;
    public Texture2D cursorArrow;
    public PontoSpawn playerPontoSpawn;
    public AudioSource musicaFundo;
    //
    private void Awake()
    {
        
        //Qualquer outra inst�ncia que queira ser carregada, vai ser destru�da
        if (InstanciaCompartilhada != null && InstanciaCompartilhada != this)
        {
            Destroy(gameObject);
        }
        else
        {
            InstanciaCompartilhada = this;
        }
    }
    // Start is called before the first frame update


    void Start()
    {
        musicaFundo.Play(); //quando iniciado, começa a tocar a musica 
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware); //muda o cursor
        SetupScene(); //começa  dar setup na cena
    }

    public void SetupScene()
    {
        SpawnPlayer(); //spawna o player
    }

    public void SpawnPlayer()
    {
        if (playerPontoSpawn != null)
        {
            GameObject player = playerPontoSpawn.SpawnO(); //spawna o player no spawn setado
            cameraManager.virtualCamera.Follow = player.transform; //camera vai para o player
        }
    }

    // Update is called once per frame
    
    void Update()
    {

    }

    
}
