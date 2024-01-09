using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Checks if mouse is hovering over shop screen
public class ShopMouseCheck : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        playerController.CanShoot = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        playerController.CanShoot = true;
    }
}
