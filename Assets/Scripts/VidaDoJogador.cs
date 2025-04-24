using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class VidaDoJogador : MonoBehaviour
{
    [Header ("Verificações")]
    public bool jogadorVivo;

    [Header("Parametros")]
    [SerializeField] private float tempoParaGameOver;

    [Header ("Controle de Vida")]
    [SerializeField] private int vidaMaxima;
    private int vidaAtual;

    void Start()
    {
        //Configura a vida do Jogador
        jogadorVivo = true;
        vidaAtual = vidaMaxima;

        UIManager.instance.AtualizarBarraDevidaDoJogador(vidaMaxima, vidaAtual);
    }

    public void GanharVida(int vidaParaGanhar)
    {
        //roda se a vida atual somada com a vida para nao estrapolar o limite
        if(vidaAtual + vidaParaGanhar <= vidaMaxima)
        {
            vidaAtual += vidaParaGanhar;
        }
        else
        {
            //roda se a vida atual somada com a vida para ganhar superar o limite
            vidaAtual = vidaMaxima;
        }

        UIManager.instance.AtualizarBarraDevidaDoJogador(vidaMaxima,vidaAtual);    
    }

    public void LevarDano(int danoParaReceber)
    {
        // Aplica o dano no Jogador
        if(jogadorVivo)
        {
            vidaAtual -= danoParaReceber;

            GetComponent<ControleDoJogador>().RodarAnimacaoDeDano();
            UIManager.instance.AtualizarBarraDevidaDoJogador(vidaMaxima, vidaAtual);

            if(vidaAtual <= 0)
            {
                jogadorVivo = false;
                GetComponent<ControleDoJogador>().RodarAnimacaoDeDano();
                StartCoroutine(AtivarGameOver());
            }
        }
    }

    private IEnumerator AtivarGameOver()
    {
        yield return new WaitForSeconds(tempoParaGameOver);
        UnityEngine.Debug.Log("Mostrando Game Over!");
        UIManager.instance.AtivarPainelDeGameOver();
    }
}
