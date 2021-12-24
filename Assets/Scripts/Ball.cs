using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    Rigidbody2D body;
    Vector2 startPosition;
    float speed = 8.5f;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        LaunchBall();
    }

    void LaunchBall()
    {
        int x = Random.Range(0, 2) == 0 ? -1 : 1;
        int y = Random.Range(0, 2) == 0 ? -1 : 1;
        body.velocity = new Vector2(speed * x, speed * y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        gameManager.playerScored(collision.gameObject.tag);
    }

    public Vector2 GetVelocity()
    {
        return body.velocity;
    }

    public void Reset()
    {
        body.velocity = Vector2.zero;
        transform.position = startPosition;
        LaunchBall();
    }
}
