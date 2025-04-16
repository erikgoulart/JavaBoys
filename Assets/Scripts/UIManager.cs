using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private Animator oAnimatorDaTransicao;

    [Header ("UI de GameOver")]
    [SerializeField] private GameObject painelDeGameOver;

    [Header ("UI Jogador")]
    [SerializeField] private Slider barraDeVidaDoJogador;

    [Header ("UI Inimigo")]
    [SerializeField] private GameObject painelDoInimigo;
    [SerializeField] private Slider barrDeVidaDoInimigoAtual;
    [SerializeField] private TMP_Text texoDoNomeDoInimigoAtual;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DesativarPainelDoInimigo();
        ClarearImagemDeTransicao();
    }

    private void ClarearImagemDeTransicao()
    {
        oAnimatorDaTransicao.Play("Imagem-de-transicao-clareando");
    }

    public void EscurecerImagemDeTransicao()
    {
        oAnimatorDaTransicao.Play("Imagem-de-transicao-escurecendo");
    }

    public void AtualizarBarraDevidaDoJogador(int valorMaximo, int valorAtual)
    {
        barraDeVidaDoJogador.maxValue = valorMaximo;
        barraDeVidaDoJogador.value = valorAtual;
    }

    public void AtivarPainelDoInimigo()
    {
        painelDoInimigo.SetActive(true);
    }

    public void DesativarPainelDoInimigo()
    {
        painelDoInimigo.SetActive(false);
    }

    public void AtualizarBarraDeVidaInimigoAtual(int valorMaximo,int valorAtual, string nomeDoInimigo)
    {
        barrDeVidaDoInimigoAtual.maxValue = valorMaximo;
        barrDeVidaDoInimigoAtual.value = valorAtual;

        texoDoNomeDoInimigoAtual.text = nomeDoInimigo; 

        AtivarPainelDoInimigo(); 
    }

    public void AtivarPainelDeGameOver()
    {
        painelDeGameOver.SetActive(true);
    }

}
