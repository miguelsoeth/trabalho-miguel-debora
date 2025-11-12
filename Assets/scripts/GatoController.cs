using UnityEngine;

public class GatoController : PlayerController
{
    protected override void Start()
    {
        base.Start();

        jumpForce = jumpForce * 1.5f; // gato pula mais alto
        tmpJumpForce = jumpForce;
        botaoEsquerdo = KeyCode.LeftArrow;
        botaoDireito = KeyCode.RightArrow;
        pular = KeyCode.UpArrow;
    }

    protected override void Update()
    {
        base.Update();

        // ação específica do gato
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            Miar();
        }
    }

    void Miar()
    {
        Debug.Log("Gato miou!");
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (collision.gameObject.CompareTag("Mud"))
            PlayerController.MorrerTodos();
    }
}
