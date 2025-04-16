using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    [SerializeField] private string nomeDaPrimeiraFase;
    private void Start()
    {
        
    }

    public void iniciarJogo()
    {
        SceneManager.LoadScene(nomeDaPrimeiraFase);
    }

    public void SairDoJogo()
    {
        Debug.Log("saiu do jogo");
        Application.Quit();
    }

}