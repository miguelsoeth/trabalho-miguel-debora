using UnityEngine;
using UnityEngine.UI;
using Assets.enums;
using TMPro;

public class DisplayScript : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI ganhouText;
    public TextMeshProUGUI timeCounter;
    public Button restartButton;

    private void Start()
    {
        restartButton.gameObject.SetActive(false); // começa escondido
        restartButton.onClick.AddListener(OnRestartClicked);
        ganhouText.gameObject.SetActive(false);
    }

    private void Update()
    {
        GameTimer.AtualizarTempo();
        timeCounter.text = GameTimer.GetTempoFormatado();
    }

    private void OnEnable()
    {
        ScoreEvent.OnScoreChanged += AtualizarScore;
        PlayerController.OnTodosGanharam += MostrarRestartVitoria;
        PlayerController.OnTodosMorreram += MostrarRestart;
    }

    private void OnDisable()
    {
        ScoreEvent.OnScoreChanged -= AtualizarScore;
        PlayerController.OnTodosGanharam -= MostrarRestartVitoria;
        PlayerController.OnTodosMorreram -= MostrarRestart;
    }

    private void MostrarRestartVitoria()
    {
        restartButton.gameObject.SetActive(true);
        ganhouText.gameObject.SetActive(true);
    }


    private void MostrarRestart()
    {
        restartButton.gameObject.SetActive(true);
    }

    private void AtualizarScore(TipoColetavel tipo)
    {
        scoreText.text = $"Ossos: {ScoreEvent.ScoreOsso}  |  Peixes: {ScoreEvent.ScorePeixe}";
    }

    private void OnRestartClicked()
    {
        PlayerController.ReviverTodos();
        restartButton.gameObject.SetActive(false);
        ganhouText.gameObject.SetActive(false);
    }

}
