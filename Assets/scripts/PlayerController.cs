using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float jumpForce = 6f; // força mais baixa
    public float turnSpeed = 8f;
    public float fallMultiplier = 2.5f; // acelera descida
    public float lowJumpMultiplier = 2f; // reduz "flutuação" quando solta o pulo

    protected Rigidbody rb;
    protected bool facingRight = true;
    protected bool isGrounded = true;

    protected KeyCode botaoEsquerdo = KeyCode.A;
    protected KeyCode botaoDireito = KeyCode.D;
    protected KeyCode pular = KeyCode.W;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation; // evita tombar
    }

    protected virtual void Update()
    {
        Mover();
        Pular();
        AjustarGravidade();
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
        // Aumenta a gravidade quando o player está caindo
        if (rb.linearVelocity.y < 0)
        {
            rb.AddForce(Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * rb.mass);
        }
        // Aumenta um pouco a gravidade se o jogador soltar o botão antes do topo
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
    }
}
