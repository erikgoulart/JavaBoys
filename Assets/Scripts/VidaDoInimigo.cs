using System;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Rendering;

public class VidaDoInimigo : MonoBehaviour
{

    [Header ("Verificações")]
    public bool inimigoVivo;

    [Header ("Parametros")]
    [SerializeField] private string nomeDoInimigo;

    [Header ("Controle de Vida")]
    [SerializeField] private int vidaMaxima;
    private int vidaAtual;
    [SerializeField] private float tempoParaSumir;

    [SerializeField] private int chanceDeDroparComida;
    [SerializeField] private GameObject[] comidasParaDropar;

    [Header ("Som do Inimigo")]
    [SerializeField] public AudioSource inimigoLevandoDano;

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
        UIManager.instance.AtualizarBarraDeVidaInimigoAtual(vidaMaxima, vidaAtual,nomeDoInimigo);
        inimigoLevandoDano.Play();

        if (vidaAtual <= 0)
        {
            inimigoVivo = false;
            GetComponent<EnemyControl>().RodarAnimacaoDeDerrota();
            SpawnarComida();
            UIManager.instance.DesativarPainelDoInimigo();

            Destroy(gameObject, tempoParaSumir);
        }

    }

    private void SpawnarComida()
    {
    int numeroAleatorio = UnityEngine.Random.Range(0, 101); // de 0 a 100
    Debug.Log(numeroAleatorio);

    if (numeroAleatorio < chanceDeDroparComida)
    {
        GameObject comidaEscolhida = comidasParaDropar[UnityEngine.Random.Range(0, comidasParaDropar.Length)];
        Instantiate(comidaEscolhida, transform.position, transform.rotation);
    }
    }

}
