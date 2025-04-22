using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private Image imagemCutscene;
    [SerializeField] private TextMeshProUGUI textoCutscene;

    [Header("Configuração")]
    [SerializeField] private Sprite[] imagens;
    [SerializeField] private string[] falas;
    [SerializeField] private string proximaCena;
    [SerializeField] private float velocidadeDigitacao = 0.04f;

    private int index = 0;
    private bool digitando = false;

    void Start()
    {
        textoCutscene.text = "";
        imagemCutscene.sprite = imagens[index];
        StartCoroutine(DigitarFala());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (digitando)
            {
                StopAllCoroutines();
                textoCutscene.text = falas[index];
                digitando = false;
            }
            else
            {
                index++;
                if (index < imagens.Length && index < falas.Length)
                {
                    imagemCutscene.sprite = imagens[index];
                    StartCoroutine(DigitarFala());
                }
                else
                {
                    SceneManager.LoadScene(proximaCena);
                }
            }
        }
    }

    IEnumerator DigitarFala()
    {
        digitando = true;
        textoCutscene.text = "";
        foreach (char letra in falas[index].ToCharArray())
        {
            textoCutscene.text += letra;
            yield return new WaitForSeconds(velocidadeDigitacao);
        }
        digitando = false;
    }
}
