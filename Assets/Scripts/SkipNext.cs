using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SkipNext : MonoBehaviour
{
    [SerializeField] private string nomeProximaEtapa;
    [SerializeField] private VideoPlayer videoPlayer;

    void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Play();
            videoPlayer.loopPointReached += OnVideoFinished;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IrParaProximaCena();
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        IrParaProximaCena();
    }

    private void IrParaProximaCena()
    {
        SceneManager.LoadScene(nomeProximaEtapa);
    }
}