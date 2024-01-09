using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullItems : MonoBehaviour
{
    private Renderer objRenderer;
    private float timer = 0;
    private float blinkInterval = 0.21f;
    private bool blinking;

    // Start is called before the first frame update
    void Start()
    {
        objRenderer = gameObject.GetComponent<Renderer>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // If an item has been alive for over 10 seconds, start blinking
        if (timer > 10 & !blinking)
        {
            StartCoroutine(Blink());
        }

        // Destroy item when it is blinking very fast
        if (blinkInterval <= 0)
        {
            Destroy(gameObject);
        }

    }

    private IEnumerator Blink()
    {
        while (blinkInterval > 0)
        {
            blinking = true;
            blinkInterval -= 0.01f;
            objRenderer.enabled = false;
            yield return new WaitForSeconds(blinkInterval);
            objRenderer.enabled = true;
            yield return new WaitForSeconds(blinkInterval);

        }
    }

}
