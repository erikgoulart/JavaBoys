using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Efeitos Sonoros")]
    public AudioSource pegarComida;
    public AudioSource jogadorLevandoDano;

    [Header("Músicas")]
    [SerializeField] private AudioSource musicaDoMenu;
    [SerializeField] private AudioSource musicaDaCutscene;
    [SerializeField] private AudioSource musicaDaCutscene2;
    [SerializeField] private AudioSource musicaDaCutscene3;
    [SerializeField] private AudioSource musicaDaCutscene4;
    [SerializeField] private AudioSource musicaDeFundo1;
    [SerializeField] private AudioSource musicaDeFundo2;
    [SerializeField] private AudioSource musicaDeFundo3;
    [SerializeField] private AudioSource musicaDeGameOver;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Se já existe um SoundManager, destrói este
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        TocarMusicaDaFaseAtual();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene cena, LoadSceneMode modo)
    {
        TocarMusicaDaFaseAtual();
    }

    private void PararTodasAsMusicas()
    {
        musicaDoMenu.Stop();
        musicaDaCutscene.Stop();
        musicaDaCutscene2.Stop();
        musicaDaCutscene3.Stop();
        musicaDaCutscene4.Stop();
        musicaDeFundo1.Stop();
        musicaDeFundo2.Stop();
        musicaDeFundo3.Stop();
        musicaDeGameOver.Stop();
    }

    private void TocarMusicaDaFaseAtual() 
    {
        string nomeCena = SceneManager.GetActiveScene().name;

        PararTodasAsMusicas();

        if (nomeCena == "Menu")
        {
            musicaDoMenu.Play();
        }
        else if (nomeCena == "Cutscene")
        {
            musicaDaCutscene.Play();
        }
        else if (nomeCena == "Fase01")
        {
            musicaDeFundo1.Play();
        }
        else if (nomeCena == "Fase02")
        {
            musicaDeFundo2.Play();
        }
        else if (nomeCena == "Cutscene2")
        {
            musicaDaCutscene2.Play();
        }
        else if (nomeCena == "Cutscene3")
        {
            musicaDaCutscene3.Play();
        }
        else if (nomeCena == "Fase03")
        {
            musicaDeFundo3.Play();
        }
        else if (nomeCena == "Cutscene4")
        {
            musicaDaCutscene4.Play();
        }
        // else opcional para casos não tratados
        else
        {
            // Nenhuma música tocada
        }
    }

    public void TocarMusicaDeGameOver()
    {
        PararTodasAsMusicas();
        musicaDeGameOver.Play();
    }
}
