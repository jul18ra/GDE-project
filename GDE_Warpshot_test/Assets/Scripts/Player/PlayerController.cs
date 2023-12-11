using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private ItemTracker itemTracker;

    private Animator playerAnim;

    private AudioSource audioSource;
    public AudioClip tpItemSound;
    public AudioClip hpItemSound;
    public AudioClip roboPartSound;

    public Rigidbody2D playerRb;
    private float moveSpeed = 10f;

    private Vector3 previousPos;
    public Vector3 PreviousPos { get { return previousPos; } }

    private Vector2 movement;
    private Vector3 mousePos;

    private bool teleporting;
    public bool Teleporting { get { return teleporting; } set { teleporting = value; } }

    private bool canShoot = true;
    public bool CanShoot { get { return canShoot; } set { canShoot = value; } }

    private void Start()
    {
        playerHealth = gameObject.GetComponent<PlayerHealth>();
        itemTracker = gameObject.GetComponent<ItemTracker>();
        playerAnim = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!teleporting)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            playerAnim.SetFloat("speed", Mathf.Abs(movement.x) + Mathf.Abs(movement.y));

        }

        // Teleports player when right click 
        if (Input.GetMouseButtonDown(1) & itemTracker.CanTeleport)
        {
            playerRb.constraints = RigidbodyConstraints2D.FreezePosition;
            playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
            previousPos = transform.position;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            itemTracker.CurrentTeleports--;
            itemTracker.UpdateTeleportCount();
            teleporting = true;
            playerAnim.SetBool("isTeleporting", true);

        }
        else
        {
            playerAnim.SetBool("reachedDestination", true);
        }

    }

    void FixedUpdate()
    {
        // Standard player movement
        if(!teleporting)
        {
            playerRb.MovePosition(playerRb.position + moveSpeed * Time.fixedDeltaTime * movement.normalized);

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!teleporting)
        {

            if (other.CompareTag("HP") & playerHealth.CurrentHealth < playerHealth.MaxHealth)
            {
                audioSource.PlayOneShot(hpItemSound);
                playerHealth.CurrentHealth++;
                playerHealth.UpdateHealthBar();
                Destroy(other.gameObject);
            }

            if (other.CompareTag("TeleportItem") & itemTracker.CurrentTeleports < itemTracker.MaxTeleports)
            {
                audioSource.PlayOneShot(tpItemSound);
                itemTracker.CurrentTeleports++;
                itemTracker.UpdateTeleportCount();
                Destroy(other.gameObject);
            }

            if (other.CompareTag("EnemyPart"))
            {
                audioSource.PlayOneShot(roboPartSound);
                itemTracker.PartAmount++;
                itemTracker.UpdatePartCount();
                Destroy(other.gameObject);
            }
        }
       
    }

    public void Teleport()
    {
        transform.position = mousePos;
    }

    private void OnMouseOver()
    {
        canShoot = false;
    }

    private void OnMouseExit()
    {
        canShoot = true;
    }

}