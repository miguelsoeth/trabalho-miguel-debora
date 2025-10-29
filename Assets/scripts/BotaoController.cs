using System.Collections;
using UnityEngine;

public class BotaoController : MonoBehaviour
{
    public GameObject item;
    public float altura;
    public float duracao;

    private bool emMovimento = false;
    private Vector3 posInicial;
    private Vector3 posFinal;

    void Start()
    {
        posInicial = item.transform.position;
        posFinal = posInicial + new Vector3(0, altura, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!emMovimento)
            StartCoroutine(MoverItem(item, posFinal, duracao));
    }

    void OnCollisionExit(Collision collision)
    {
        if (!emMovimento)
            StartCoroutine(MoverItem(item, posInicial, duracao));
    }

    private IEnumerator MoverItem(GameObject obj, Vector3 destino, float duracao)
    {
        emMovimento = true;
        Vector3 origem = obj.transform.position;
        float tempo = 0f;

        while (tempo < duracao)
        {
            obj.transform.position = Vector3.Lerp(origem, destino, tempo / duracao);
            tempo += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = destino;
        emMovimento = false;
    }
}
