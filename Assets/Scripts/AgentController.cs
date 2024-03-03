using UnityEngine;

/// <summary>
/// Class that controls the agent's movement and animation.
/// </summary>
/// 

public class AgentController : MonoBehaviour
{
    [SerializeField] private float gravity = -9.82f;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private Sprite[] sprites;

    [SerializeField] private Vector3 velocity;
    private SpriteRenderer spriteRenderer;
    private int currentSprite = 0;

    public PipeSpawner pipeSpawner;
    public AgentBrain agentBrain;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        InvokeRepeating(nameof(AnimateAgent), 0, 0.1f);
    }

    private void Update()
    {
        velocity.y += gravity * Time.deltaTime;
        transform.localPosition += velocity * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, 0, velocity.y * 3f);

    }

    public void Jump()
    {
        velocity = Vector3.up * jumpForce;
    }

    public void ResetAgent()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        velocity = Vector3.zero;
    }

    public void AnimateAgent()
    {
        currentSprite = (currentSprite + 1) % sprites.Length;
        spriteRenderer.sprite = sprites[currentSprite];
    }

    public float getVelocity()
    {
        return velocity.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        agentBrain.AddReward(-1f);
        agentBrain.EndEpisode();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        agentBrain.AddReward(1f);
        pipeSpawner.increaseLastPipeIndex();
        Score.Instance.AddScore();
    }
}