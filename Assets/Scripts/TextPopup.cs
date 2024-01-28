using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPopup : MonoBehaviour
{
    public GameObject text;
    public PopupSO popupText;
    CircleCollider2D circleCollider;
    // Start is called before the first frame update
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {

    }

    // Use Circle.Overlap
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //text.SetActive(true);
            // Add this object's popup to the list of text popups that are near the player
        }    
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //text.SetActive(false);
            // Remove this object's popup from the list of text popups that are near the player
        }
    }
}
