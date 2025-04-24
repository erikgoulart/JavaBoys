using UnityEngine;

public class Golpes : MonoBehaviour
{
    [SerializeField] private int danoDoGolpe;
    [SerializeField] public GameObject impactoSocoPrefab; // Agora é um GameObject com AudioSource

    void OnTriggerEnter2D(Collider2D other)
    {
        bool acertouAlgo = false;

        if (other.TryGetComponent<VidaDoJogador>(out var jogador))
        {
            jogador.LevarDano(danoDoGolpe);
            acertouAlgo = true;
        }
        else if (other.TryGetComponent<VidaDoInimigo>(out var inimigo))
        {
            inimigo.LevarDano(danoDoGolpe);
            acertouAlgo = true;
        }

        if (acertouAlgo && impactoSocoPrefab != null)
        {
            // Instanciando o prefab no local de colisão
            GameObject somInstanciado = Instantiate(impactoSocoPrefab, transform.position, Quaternion.identity);
            
            // Verificando se o AudioSource existe no prefab instanciado
            AudioSource som = somInstanciado.GetComponent<AudioSource>();
            if (som != null && som.clip != null)
            {
                som.Play();
                // Após o som tocar, destruímos o GameObject do som após a duração do áudio
                Destroy(somInstanciado, som.clip.length);
            }
        }
    }
}
