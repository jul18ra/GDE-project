using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool teleporting;
    private Vector3 previousPos;

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

    void Update()
    {
        teleporting = player.GetComponent<PlayerController>().getIsTeleporting();
        previousPos = player.GetComponent<PlayerController>().getPreviousPos();

        Debug.Log(teleporting);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerDirection = (player.transform.position - transform.position).normalized;
        currentDirection = (previousPos- transform.position).normalized;

        if (teleporting)
        {   
            // Enemies move where the player last was
            enemyRb.MovePosition((Vector2)transform.position + (speed * Time.deltaTime * currentDirection));
        }
        else
        {
            // Enemies move towards player
            enemyRb.MovePosition((Vector2)transform.position + (speed * Time.deltaTime * playerDirection));
        }
    }
}
