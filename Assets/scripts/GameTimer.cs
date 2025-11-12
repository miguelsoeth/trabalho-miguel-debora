using UnityEngine;

public static class GameTimer
{
    private static float tempo = 0f;       // tempo em segundos
    private static bool pausado = false;    // flag de pausa

    // Atualizar tempo (chamado a cada frame)
    public static void AtualizarTempo()
    {
        if (!pausado)
            tempo += Time.deltaTime;
    }

    // Retornar minutos e segundos formatados
    public static string GetTempoFormatado()
    {
        int minutos = Mathf.FloorToInt(tempo / 60);
        int segundos = Mathf.FloorToInt(tempo % 60);
        return string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    // Pausar o tempo
    public static void Pausar()
    {
        pausado = true;
    }

    // Retomar o tempo
    public static void Retomar()
    {
        pausado = false;
    }

    // Resetar tempo
    public static void Resetar()
    {
        tempo = 0f;
    }
}
