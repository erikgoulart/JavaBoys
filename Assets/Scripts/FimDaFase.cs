using System.Collections;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FimDaFase : MonoBehaviour
{
    [SerializeField] private float tempoParaEscurecer;
    [SerializeField]private float tempoParaCarregarNovaFase;
    [SerializeField] private string nomeDaProximaFase;

    private IEnumerator EscurecerTela()
    {
        yield return new WaitForSeconds (tempoParaEscurecer);
        UIManager.instance.EscurecerImagemDeTransicao();

        yield return new WaitForSeconds(tempoParaCarregarNovaFase);
        SceneManager.LoadScene(nomeDaProximaFase);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<ControleDoJogador>() != null)
        {
            EnemyControl[] inimigosNaFase = Object.FindObjectsByType<EnemyControl>(FindObjectsSortMode.None);
            if (inimigosNaFase.Length == 0)
            {
                StartCoroutine(EscurecerTela());
            }
        }

    }
}
