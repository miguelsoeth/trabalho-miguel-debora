using Assets.enums;
using UnityEngine;
using static UnityEditor.Progress;

public class ColetavelController : MonoBehaviour
{
    public TipoColetavel tipoColetavel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("cachorro"))
        {
            if (tipoColetavel == TipoColetavel.Osso)
            {
                ScoreEvent.Pontuar(tipoColetavel);
                Destroy(gameObject);
            }
        }
        
        else if (other.CompareTag("gato"))
        {
            if (tipoColetavel == TipoColetavel.Peixe)
            {
                ScoreEvent.Pontuar(tipoColetavel);
                Destroy(gameObject);
            }
        }
    }
}
