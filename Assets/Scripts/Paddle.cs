using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] AudioClip paddle1Boop;
    [SerializeField] AudioClip paddle2Boop;

    Rigidbody2D body;
    AudioSource audioSource;

    Vector2 movement;
    Vector2 startPosition;
    float speed = 7f;

    int paddleId;
    public AudioClip[] paddleBoops;
    public string[] inputAxisNames;

    Ball ball;

    bool isAI;
    float input;

    void Awake()
    {
        paddleBoops = new AudioClip[] { paddle1Boop, paddle2Boop };
        inputAxisNames = new string[] { "Vertical1", "Vertical2" };
    }

    void Start()
    {
        isAI = !StaticGameData.Instance.GetComponent<StaticGameData>().isLocalMulti && gameObject.CompareTag("PaddleRight");
        paddleId = gameObject.CompareTag("PaddleLeft") ? 0 : 1;
        audioSource = gameObject.GetComponent<AudioSource>();
        body = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        ball = FindObjectOfType<Ball>();
    }

    void Update()
    {
        input = isAI ? getAIInput() : Input.GetAxisRaw(inputAxisNames[paddleId]);
        movement = new Vector2(0, input * speed);
    }

    void FixedUpdate()
    {
        movePaddle(movement);
    }

    void movePaddle(Vector2 direction)
    {
        body.velocity = movement;
    }

    // TODO Turn this into an invincible AI with fancy math.
    int getAIInput()
    {
        // Return 1 for up -1 for down, 0 for nothing.
        bool ballApproaching = ball.GetVelocity().x > 0;
        float centerOffset = 0.1f;

        // Run back to the center.
        if (!ballApproaching)
        {
            // TODO Simplify these checks.
            if (gameObject.transform.position.y > (centerOffset * -1f) && gameObject.transform.position.y < centerOffset)
            {
                return 0;
            }
            if (gameObject.transform.position.y > centerOffset)
            {
                return -1;
            }
            if (gameObject.transform.position.y < centerOffset)
            {
                return 1;
            }

            return 0;
        }

        // Follow the ball y pos.
        else
        {
            if (gameObject.transform.position.y > ball.transform.position.y)
            {
                return -1;
            }
            else if (gameObject.transform.position.y < ball.transform.position.y)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            audioSource.PlayOneShot(paddleBoops[paddleId]);
        }
    }

    public void Reset()
    {
        body.velocity = Vector2.zero;
        transform.position = startPosition;
    }
}
