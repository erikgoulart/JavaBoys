using UnityEngine;

public class AreaDeLuta : MonoBehaviour
{
    [Header ("Vrerificaçoes")]
    private bool podeVerificarJogador;
    private bool podeSpawnar;

    [Header ("Cronometro do Spawn")]
    [SerializeField] private float tempoMaximoEntreSpawns;
    private float tempoAtualEntreSpawns;

    [Header ("Controle de Spawn dos inimigos")]
    [SerializeField]private Transform[] pontosDeSpawn;
    [SerializeField]private GameObject[] inimigosParaSpawnar;
    private int inimigosSpawnados;
    private int inimigoAtual;

    void Start()
    {
        podeVerificarJogador = true;
        podeSpawnar = false;

        inimigosSpawnados = 0;
        inimigoAtual = 0;
    }

    void Update()
    {
        if (podeSpawnar == true && inimigosSpawnados < inimigosParaSpawnar.Length)
        {
            RodarCronometroDoSpawn();
        }
    }

    private void RodarCronometroDoSpawn()
    {
        //controla a quantidade de inimigos spawnados por segundos
        tempoAtualEntreSpawns -= Time.deltaTime;
        if (tempoAtualEntreSpawns <= 0)
        {
            SpawnarInimigo();
            tempoAtualEntreSpawns = tempoMaximoEntreSpawns;
        }
    }

    private void SpawnarInimigo()
    {
        //escolhe um novo lodal d espawn e um novo inimigo
        Transform pontoAleatorio = pontosDeSpawn[Random.Range(0, pontosDeSpawn.Length)];
        GameObject novoInimigo = inimigosParaSpawnar[inimigoAtual];

        // spawna o novo inimigo no local escolhido anteriromente
        Instantiate(novoInimigo, pontoAleatorio.position, pontoAleatorio.rotation);
        inimigoAtual += 1;
        inimigosSpawnados += 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(podeVerificarJogador == true)
        {
            if(other.gameObject.GetComponent<ControleDoJogador>() != null)
            {
                podeVerificarJogador = false;
                podeSpawnar = true;
            }
        }
    }
}
