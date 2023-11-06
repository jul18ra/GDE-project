using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private bool teleporting;

    private float speed = 6f;
    private Vector2 direction;
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
        Debug.Log(teleporting);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Enemies move towards player
        direction = player.transform.position - transform.position;
        if(!teleporting)
        {
            enemyRb.MovePosition((Vector2)transform.position + (speed * Time.deltaTime * direction.normalized));
        }
    }
}
