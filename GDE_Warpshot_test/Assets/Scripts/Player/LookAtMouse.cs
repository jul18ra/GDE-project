using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    public GameObject player;
    public GameObject head;

    private float headMaxAngle = 360f;
    private float headMinAngle = -360f;
    private float headRotation;

    private float armsMaxAngle = 360f;
    private float armsMinAngle = -360f;
    private float armsRotation;

    private Vector3 defScale;

    private Vector2 lookDir;
    private Vector3 mousePos;

    private void Start()
    {
        defScale = player.transform.localScale;
    }
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Rotates arms towards mouse
        armsRotation = CalculateRotation(gameObject, armsMinAngle, armsMaxAngle);
        transform.rotation = Quaternion.Euler(0, 0, armsRotation);

        // Rotates head towards mouse
        headRotation = CalculateRotation(head, headMinAngle, headMaxAngle);
        head.transform.rotation = Quaternion.Euler(0, 0, headRotation);

        // Makes the player face left if the cursor is on the left side of the player
        if (armsRotation < -90 | armsRotation > 90)
        {
            // Flips player
            player.transform.localScale = new Vector3(defScale.x * -1, defScale.y, defScale.z);

            // Flips the gun
            transform.localRotation = Quaternion.Euler(180, 180, -armsRotation);

            // Flips player head
            head.transform.rotation = Quaternion.Euler(180, 180, headRotation);

        }
        else
        {
            player.transform.localScale = new Vector3(defScale.x, defScale.y, defScale.z);
        }

    }

    // Calculates rotation for a part so that the part faces the mouse
    private float CalculateRotation(GameObject part, float minAngle, float maxAngle)
    {
        float rotation;
        lookDir = mousePos - part.transform.position; 
        rotation = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rotation = Mathf.Clamp(rotation, minAngle, maxAngle);
        return rotation;
    }

}
