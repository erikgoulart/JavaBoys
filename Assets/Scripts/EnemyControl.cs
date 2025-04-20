using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    [Header ("Refencias Gerais")]
    private Rigidbody2D oRigidbody2D;
    private Animator oAnimator;
    private GameObject oJogador;

    [Header ("Movimento do Inimigo")]
    [SerializeField]private float velocidadeDoInimigo;
    private Vector2 direcaoDoMovimento;
    private bool inimigoMorto = false;

    [Header ("Ataque do Inimigo")]
    [SerializeField] private float tempoMaximoEntreAtaques;
    private float tempoAtualEntreAtaques;
    private bool podeAtacar;

    [SerializeField] private float distanciaParaAtacar;
    [SerializeField] private int quantidadeDeAtaquesDoInimigo;
    private int ataqueAtualDoInimigo;
        private void Start()
    {

        oRigidbody2D = GetComponent<Rigidbody2D>();
        oAnimator = GetComponent<Animator>();
        oJogador = FindAnyObjectByType<ControleDoJogador>().gameObject;
    }

    private void Update()
    {
        if (inimigoMorto) return; // <- Impede que "parado", "apanhando" ou "voadora" sejam chamadas

        if (GetComponent<VidaDoInimigo>().inimigoVivo)
        {
            RodarCronometroDosAtaques();
            SeguirJogador();
            EspelharInimigo();
        }
    }

    private void RodarCronometroDosAtaques()
    {
        tempoAtualEntreAtaques -= Time.deltaTime;
        if(tempoAtualEntreAtaques <= 0)
        {
            podeAtacar = true;
            tempoAtualEntreAtaques = tempoMaximoEntreAtaques;
        }
    }

    private void EspelharInimigo()
    {
            // Faz o Inimigo olhar para a direção correta
        if (oJogador.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (oJogador.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    private void SeguirJogador()
    {
        //Armazena a posicao do Jogador e movimenta a ele
        if(Vector2.Distance(transform.position, oJogador.transform.position) > distanciaParaAtacar)
        {
            direcaoDoMovimento = (oJogador.transform.position - transform.position).normalized;
            oRigidbody2D.linearVelocity = direcaoDoMovimento * velocidadeDoInimigo;

            oAnimator.SetTrigger("Andando");
        }
        //Deixa de se movimentar e ataca o Jogador
        else
        {
            oRigidbody2D.linearVelocity = Vector2.zero;
            oAnimator.SetTrigger("parado");

            SortearAtaque();
        }
    }
    private void SortearAtaque()
    {
        // Sorteia os ataques e inicia
        ataqueAtualDoInimigo = Random.Range(0, quantidadeDeAtaquesDoInimigo);

        if (podeAtacar)
        {
            IniciarAtaque();
        }
    }

    public void RodarAnimacaoDeDano()
    {
        oAnimator.SetTrigger("apanhando");
    }
    private void IniciarAtaque()
    {
        // Muda o seu ataque para o ataque sorteado
        if (ataqueAtualDoInimigo == 0)
        {
            oAnimator.SetTrigger("socando");
        }
        else if (ataqueAtualDoInimigo != 0)
        {
            oAnimator.SetTrigger("voadora");
        }
        
    }

 public void RodarAnimacaoDeDerrota()
    {
        if (inimigoMorto) return; // impede chamadas múltiplas

        inimigoMorto = true;

        oRigidbody2D.linearVelocity = Vector2.zero;
        oRigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        oRigidbody2D.simulated = false;

        oAnimator.ResetTrigger("parado");
        oAnimator.ResetTrigger("socando");
        oAnimator.ResetTrigger("apanhando");
        oAnimator.ResetTrigger("Andando");
        oAnimator.ResetTrigger("voadora");

        oAnimator.Play("morte");

        this.enabled = false;
    }

}

