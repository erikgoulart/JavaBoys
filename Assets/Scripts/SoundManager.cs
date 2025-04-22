using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Efeitos Sonoros")]
    public AudioSource impactoSoco;
    public AudioSource pegarComida;
    public AudioSource inimigoNerdLevandoDano;
    public AudioSource inimigoEmoLevandoDano;
    public AudioSource jogadorLevandoDano;

    [Header("Músicas")]
    [SerializeField] private AudioSource musicaDoMenu;
    [SerializeField] private AudioSource musicaDaCutscene;
    [SerializeField] private AudioSource musicaDeFundo1;
    [SerializeField] private AudioSource musicaDeFundo2;
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
        musicaDeFundo1.Stop();
        musicaDeFundo2.Stop();
        musicaDeGameOver.Stop();
    }

    private void TocarMusicaDaFaseAtual()
    {
        string nomeCena = SceneManager.GetActiveScene().name;

        PararTodasAsMusicas();

        switch (nomeCena)
        {
            case "Menu": // <-- substitua pelo nome exato da sua cena do menu
                musicaDoMenu.Play();
                break;

            case "Cutscene":
                musicaDaCutscene.Play();
                break;

            case "Fase01":
                musicaDeFundo1.Play();
                break;

            case "Fase02":
                musicaDeFundo2.Play();
                break;

            default:
                musicaDeFundo1.Play(); // Música padrão
                break;
        }
    }

    public void TocarMusicaDeGameOver()
    {
        PararTodasAsMusicas();
        musicaDeGameOver.Play();
    }
}
