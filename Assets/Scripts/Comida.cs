using UnityEngine;

public class Comida : MonoBehaviour
{
    [SerializeField] private int vidaParaDar;
    [SerializeField] private AudioSource somDeColetaPrefab; // Deve ser um prefab com o AudioSource

    private void OnTriggerEnter2D(Collider2D other)
    {
        VidaDoJogador jogador = other.gameObject.GetComponent<VidaDoJogador>();

        if (jogador != null)
        {
            jogador.GanharVida(vidaParaDar);

            if (somDeColetaPrefab != null && somDeColetaPrefab.clip != null)
            {
                // Instancia um clone TEMPORÁRIO do som
                AudioSource somTemp = Instantiate(somDeColetaPrefab, transform.position, Quaternion.identity);
                somTemp.Play();
                Destroy(somTemp.gameObject, somTemp.clip.length);
            }

            Destroy(gameObject); // Destroi a comida depois
        }
    }
}