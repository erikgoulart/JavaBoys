using Unity.Mathematics;
using UnityEngine;

public class CameraQueSegue : MonoBehaviour
{
    [Header ("Referencia ao Jogador")]
    private GameObject oJogador;
    private Vector3 posicaoDoJogador;

    [Header ("Limites de movimentação")]
    [SerializeField]private float xMinimo;
    [SerializeField]private float xMaximo;

   private void Start()
    {
        oJogador = FindAnyObjectByType<ControleDoJogador>().gameObject;
    }

    private void Update()
    {
        SeguirJogador();
        Camera.main.clearFlags = CameraClearFlags.Skybox; // ou .SolidColor
        Camera.main.backgroundColor = Color.black; // ou qualquer outra
    }

    private void SeguirJogador()
    {   
        //armazena a posição do JOgador
        posicaoDoJogador = oJogador.transform.position;

        // deixa sua posição x igual a do Jogador e impede que saia da tela
        transform.position = new Vector3 (posicaoDoJogador.x, transform.position.y, transform.position.z);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMinimo, xMaximo), transform.position.y, transform.position.z);
    }
}



