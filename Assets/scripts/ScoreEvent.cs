using UnityEngine;
using System;
using Assets.enums;
using UnityEngine.SocialPlatforms.Impl;

public static class ScoreEvent
{
    public static int ScoreOsso = 0;
    public static int ScorePeixe = 0;

    public delegate void ScoreChangedHandler(TipoColetavel tipo);

    public static event ScoreChangedHandler OnScoreChanged;

    public static void Pontuar(TipoColetavel tipo)
    {
        switch (tipo)
        {
            case TipoColetavel.Osso:
                ScoreOsso++;
                Debug.Log($"ScoreEvent osso disparado: {ScoreOsso}");
                break;
            case TipoColetavel.Peixe:
                ScorePeixe++;
                Debug.Log($"ScoreEvent peixe disparado: {ScorePeixe}");
                break;
        }

        OnScoreChanged?.Invoke(tipo);
    }

    public static void Resetar()
    {
        ScoreOsso = 0;
        ScorePeixe = 0;
        Debug.Log("ScoreEvent resetado.");

        OnScoreChanged?.Invoke(TipoColetavel.Osso);
        OnScoreChanged?.Invoke(TipoColetavel.Peixe);
    }
}
