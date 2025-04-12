using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PainelDeGameOver : MonoBehaviour
{
    [SerializeField] private string nomeDoMenu;

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
