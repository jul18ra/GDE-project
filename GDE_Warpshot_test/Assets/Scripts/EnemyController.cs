using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private PlayerController PlayerController;
    private float speed = 8f;
    private Vector2 playerDirection;
    private Vector2 currentDirection;
    private Rigidbody2D enemyRb;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
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
