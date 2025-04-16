using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PainelDeGameOver : MonoBehaviour
{
    [SerializeField] private string nomeDoMenu;

    private void Start()
    {
        SoundManager.instance.TocarMusicaDeGameOver();

        DestruirTodosOsInimigos();
    }

    private void DestruirTodosOsInimigos()
    {
        EnemyControl[] inimigos = UnityEngine.Object.FindObjectsByType<EnemyControl>(FindObjectsSortMode.None); 
        foreach (EnemyControl inimigo in inimigos)
        {
            Destroy(inimigo.gameObject);
        }

        Debug.Log("Todos os inimigos foram destruídos.");
    }

    private void Update()
    {
        receberInputs();
    }

    private void receberInputs()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReiniciarPartida();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            VoltarAoMenu();
        }
    }

    private void ReiniciarPartida()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void VoltarAoMenu()
    {
        SceneManager.LoadScene(nomeDoMenu);
    }

}
