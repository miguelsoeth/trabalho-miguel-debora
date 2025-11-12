using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public static List<PlayerController> allPlayers = new List<PlayerController>();
    private Renderer[] renderers;

    public static float moveSpeed = 8f;
    public float tmpMoveSpeed = 0f;
    public float jumpForce = 6f; // for�a mais baixa
    public float tmpJumpForce = 6f;
    public float turnSpeed = 8f;
    public float fallMultiplier = 2.5f; // acelera descida
    public float lowJumpMultiplier = 2f; // reduz "flutua��o" quando solta o pulo

    private Vector3 posicaoInicial;

    protected Rigidbody rb;
    protected bool facingRight = true;
    protected bool isGrounded = true;
    protected static bool isDead = false;
    public static event Action OnTodosGanharam;
    public static event Action OnTodosMorreram;

    protected KeyCode botaoEsquerdo = KeyCode.A;
    protected KeyCode botaoDireito = KeyCode.D;
    protected KeyCode pular = KeyCode.W;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        tmpMoveSpeed = moveSpeed;
        tmpJumpForce = jumpForce;
        rb.constraints = RigidbodyConstraints.FreezeRotation; // evita tombar
        posicaoInicial = transform.position;
    }

    protected virtual void Update()
    {
        Mover();
        Pular();
        AjustarGravidade();
    }

    private void Awake()
    {
        allPlayers.Add(this);
        renderers = GetComponentsInChildren<Renderer>();
    }

    private void OnDestroy()
    {
        allPlayers.Remove(this);
    }

    private void SetVisibility(bool visible)
    {
        foreach (var rend in renderers)
            rend.enabled = visible;
    }

    public static void MorrerTodos()
    {
        foreach (var player in allPlayers)
        {
            player.Morrer();
        }

        OnTodosMorreram?.Invoke();
        GameTimer.Pausar();
    }
    
    public static void GanharTodos()
    {
        foreach (var player in allPlayers)
        {
            player.Ganhou();
        }

        GameTimer.Pausar();
        OnTodosGanharam?.Invoke();
    }

    public static bool Mortos()
    {
        if (!isDead) return false;
        return true;
    }

    public static void ReviverTodos()
    {
        foreach (var player in allPlayers)
        {
            player.Reviver();
        }

        ScoreEvent.Resetar();
        GameTimer.Resetar();
        GameTimer.Retomar();
    }

    public void Reviver()
    {
        isDead = false;
        moveSpeed = tmpMoveSpeed;
        jumpForce = tmpJumpForce;
        SetVisibility(true);
        transform.position = posicaoInicial;
        rb.linearVelocity = Vector3.zero;
    }
    
    public void Morrer()
    {
        isDead = true;
        moveSpeed = 0f;
        jumpForce = 0f;
        SetVisibility(false);
    }
    
    public void Ganhou()
    {
        moveSpeed = 0f;
        jumpForce = 0f;
    }

    protected void Mover()
    {
        float move = 0f;
        if (Input.GetKey(botaoEsquerdo)) move = -1f;
        else if (Input.GetKey(botaoDireito)) move = 1f;

        Vector3 velocity = rb.linearVelocity;
        velocity.x = move * moveSpeed;
        rb.linearVelocity = velocity;

        if (move > 0 && !facingRight) Flip();
        else if (move < 0 && facingRight) Flip();

        Quaternion targetRotation = facingRight ? Quaternion.Euler(0, 90, 0) : Quaternion.Euler(0, 270, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
    }

    protected virtual void Pular()
    {
        if (Input.GetKeyDown(pular) && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z); // zera impulso anterior
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void AjustarGravidade()
    {
        // Aumenta a gravidade quando o player est� caindo
        if (rb.linearVelocity.y < 0)
        {
            rb.AddForce(Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * rb.mass);
        }
        // Aumenta um pouco a gravidade se o jogador soltar o bot�o antes do topo
        else if (rb.linearVelocity.y > 0 && !Input.GetKey(pular))
        {
            rb.AddForce(Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * rb.mass);
        }
    }

    protected void Flip()
    {
        facingRight = !facingRight;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;

        if (collision.gameObject.CompareTag("morte"))
            MorrerTodos();
    }
}
