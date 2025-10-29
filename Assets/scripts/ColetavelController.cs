using Assets.enums;
using UnityEngine;
using static UnityEditor.Progress;

public class ColetavelController : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        ScoreEvent.Pontuar(TipoColetavel.Osso);
    }

}
