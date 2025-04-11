using System;
using UnityEngine;
using UnityEngine.Rendering;

public class VidaDoInimigo : MonoBehaviour
{

    [Header ("Verificações")]
    public bool inimigoVivo;

    [Header ("Controle de Vida")]
    [SerializeField] private int vidaMaxima;
    private int vidaAtual;
    [SerializeField] private float tempoParaSumir;

    [SerializeField] private int chanceDeDroparComida;
    [SerializeField] private GameObject[] comidasParaDropar;

    private void Start()
    {
        //Configura a vida do Inimigo
        inimigoVivo = true;
        vidaAtual =  vidaMaxima;
    }



public void LevarDano(int danoParaReceber)
{

    vidaAtual -= danoParaReceber;
    GetComponent<EnemyControl>().RodarAnimacaoDeDano();

    if (vidaAtual <= 0)
    {
        inimigoVivo = false;
        GetComponent<EnemyControl>().RodarAnimacaoDeDerrota();
        SpawnarComida();
        Destroy(gameObject, tempoParaSumir);
    }

/*if (vidaAtual <= 0)
{
    inimigoVivo = false;
    // Apenas sinaliza que morreu
    Destroy(gameObject, tempoParaSumir);
}*/
}

    private void SpawnarComida()
    {
    int numeroAleatorio = UnityEngine.Random.Range(0, 101); // de 0 a 100
    Debug.Log(numeroAleatorio);

    if (numeroAleatorio < chanceDeDroparComida)
    {
        GameObject comidaEscolhida = comidasParaDropar[UnityEngine.Random.Range(0, comidasParaDropar.Length)];
        Instantiate(comidaEscolhida, transform.position, transform.rotation);
        Debug.Log("Comida instanciada: " + comidaEscolhida.name);
    }
    }

}
