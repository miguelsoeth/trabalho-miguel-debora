using System.Collections.Generic;
using UnityEngine;

public class VitoriaController : MonoBehaviour
{
    private HashSet<string> presentes = new HashSet<string>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("gato") || other.CompareTag("cachorro"))
        {
            presentes.Add(other.tag);
            ChecarVitoria();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("gato") || other.CompareTag("cachorro"))
        {
            presentes.Remove(other.tag);
        }
    }

    private void ChecarVitoria()
    {
        if (presentes.Contains("gato") && presentes.Contains("cachorro"))
        {
            Debug.Log("Todos dentro! Vitória!");
            PlayerController.GanharTodos();
        }
    }
}
