using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private SpriteRenderer enemySr;
    private PlayerController PlayerController;
    private float speed;
    private Vector2 playerDirection;
    private Vector2 currentDirection;
    private Rigidbody2D enemyRb;
    private GameObject player;

    void Start()
    {
        enemySr = GetComponent<SpriteRenderer>();
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");

        if (gameObject.name == "RobotSpider(Clone)")
        {
            speed = 8f;
        }

        if (gameObject.name == "RobotGiant(Clone)")
        {
            speed = 4f;
        }
    }

    private void Update()
    {
        if (player.transform.position.x > gameObject.transform.position.x)
        {
            enemySr.flipX = true;
        }
        else
        {
            enemySr.flipX = false;
        }
    }

    void FixedUpdate()
    {
        PlayerController = player.GetComponent<PlayerController>();

        playerDirection = player.transform.position - transform.position;
        currentDirection = PlayerController.PreviousPos - transform.position;

        if (PlayerController.Teleporting)
        {
            // Enemies move where the player last was
            enemyRb.MovePosition((Vector2)transform.position + (speed * Time.deltaTime * currentDirection.normalized));
        }

        else
        {
            // Enemies move towards player
            enemyRb.MovePosition((Vector2)transform.position + (speed * Time.deltaTime * playerDirection.normalized));
        }

    }
}
