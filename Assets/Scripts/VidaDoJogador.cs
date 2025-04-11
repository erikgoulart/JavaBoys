using System.Diagnostics;
using UnityEngine;

public class VidaDoJogador : MonoBehaviour
{
    [Header ("Verificações")]
    public bool jogadorVivo;

    [Header ("Controle de Vida")]
    [SerializeField] private int vidaMaxima;
    private int vidaAtual;

    void Start()
    {
        //Configura a vida do Jogador
        jogadorVivo = true;
        vidaAtual = vidaMaxima;
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
    }

    public void LevarDano(int danoParaReceber)
    {
        // Aplica o dano no Jogador
        if(jogadorVivo)
        {
            vidaAtual -= danoParaReceber;

            GetComponent<ControleDoJogador>().RodarAnimacaoDeDano();

            if(vidaAtual <= 0)
            {
                jogadorVivo = false;
                UnityEngine.Debug.Log("Game Over");
            }
        }
    }
}
