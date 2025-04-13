using UnityEngine;

public class CameraQueSegue : MonoBehaviour
{
    [Header ("Referencia ao Jogador")]
    private GameObject oJogador;
    private Vector3 posicaoDoJogador;

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
        posicaoDoJogador = oJogador.transform.position;
        transform.position = new Vector3 (posicaoDoJogador.x, transform.position.y, transform.position.z);
    }
}



