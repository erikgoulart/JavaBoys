using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class ControleDoJogador : MonoBehaviour
{
    [Header("Referencias Gerais")]
    private Rigidbody2D oRigidbody2D;
    private Animator oAnimator;

    [Header("Movimento do Jogador")]
    [SerializeField]private float velocidadeDoJogador;
    private Vector2 inputDeMovimento;
    private Vector2 direcaoDoMovimento;

    [Header ("Limites de Movimentação")]
    [SerializeField] private float xMinimo;
    [SerializeField] private float xMaximo;
    [SerializeField] private float yMinimo;
    [SerializeField] private float yMaximo;

    [Header ("Controle do Ataque")]
    [SerializeField] private float tempoMaximoEntreAtaques;
    private float tempoAtualEntreAtaques;
    private bool podeAtacar;

    [Header("Controle do Dano")]
    [SerializeField] private float tempoMaximoDoDano;
    private float tempoAtualDoDano;
    private bool levouDano;

    private void Start()
    {
       oRigidbody2D = GetComponent<Rigidbody2D>();
       oAnimator = GetComponent<Animator>();

       tempoAtualDoDano = tempoMaximoDoDano;
    }


    private void Update()
    {
        if(GetComponent<VidaDoJogador>().jogadorVivo)
        {
            RodarConotrometroDosAtaques();

            if(!levouDano)
            {
                ReceberInputs();
                RodarAnimacoesEAtaques();
                EspelharJogador();
                MovimentarJogador();
            }
            else
            {
                RodarCronomentroDoDano();
            }
        }
        else
        {
            RodarAnimacaoDeDerrota();
        }
    }

    private void RodarConotrometroDosAtaques()
    {
        //controla o tempo entre um ataque e outro
        tempoAtualEntreAtaques -= Time.deltaTime;
        if (tempoAtualEntreAtaques <= 0)
        {
            podeAtacar = true;
            tempoAtualEntreAtaques = tempoMaximoEntreAtaques;
        }
    }
    private void ReceberInputs()
    {
        //Amazena a direção que o Jogador quer seguir
        inputDeMovimento = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Testar Dano do Jogador
        if(Input.GetKeyDown(KeyCode.L))
        {
            RodarAnimacaoDeDano();
        }
    }

    private void RodarCronomentroDoDano()
    {
        // controla o congelar do jogador
        tempoAtualDoDano -= Time.deltaTime;
        RodarAnimacaoDeDano();

        if (tempoAtualDoDano <= 0)
        {
            levouDano = false;
            tempoAtualDoDano = tempoMaximoDoDano;
        }
    }

private void RodarAnimacoesEAtaques()
{
    //Rodam as animacoes de parado e andando
    if (inputDeMovimento.magnitude == 0)
    {
        oAnimator.SetTrigger("parado");
    }
    else if (inputDeMovimento.magnitude != 0)
    {
        oAnimator.SetTrigger("andando");
    }

    //Roda as animacoes de soco
    if (Input.GetKeyDown(KeyCode.J) && podeAtacar)
    {
        oAnimator.SetTrigger("socando");
        podeAtacar = false;
    }
}

private void EspelharJogador()
{
    // Faz o Jogador olhar para a direção correta
    if (inputDeMovimento.x == 1)
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
    else if (inputDeMovimento.x == -1)
    {
        transform.localScale = new Vector3(-1f, 1f, 1f);
    }

}
    private void MovimentarJogador()
    {
        if(levouDano)
        {
            oRigidbody2D.linearVelocity = Vector2.zero;
        }
        else
        {
        // Movimenta o Jogador com basse na sua direção
            direcaoDoMovimento = inputDeMovimento.normalized;
            oRigidbody2D.linearVelocity = direcaoDoMovimento * 
            velocidadeDoJogador;

            oRigidbody2D.position = new Vector2(Mathf.Clamp(oRigidbody2D.position.x, xMinimo, xMaximo), oRigidbody2D.position.y);
            oRigidbody2D.position = new Vector2(oRigidbody2D.position.x, Mathf.Clamp(oRigidbody2D.position.y, yMinimo, yMaximo));
                        
        }
    }

    public void RodarAnimacaoDeDano()
    {
        // roda a animação de dano do jogador e zera a velocidade
        oAnimator.SetTrigger("levandodano");
        levouDano = true;

        oRigidbody2D.linearVelocity = Vector2.zero;
    }

    public void RodarAnimacaoDeDerrota()
    {
        oAnimator.Play("jogadormorrendo");
    }
}