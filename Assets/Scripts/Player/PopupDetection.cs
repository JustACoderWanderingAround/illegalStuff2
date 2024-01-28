using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupDetection : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> popupsInRange = new List<GameObject>();
    Collider2D[] collidersInRange;
    List<TextPopup> popups = new List<TextPopup>();

    [SerializeField] GameObject dialogueMenu;
    [SerializeField] TextMeshProUGUI textObject;

    [SerializeField] float detectionRadius = 2f;
    PopupSO currentPopup = null;

    int dialogueIndex = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        // Check if the dialogue menu is open and if there is more text to display
        if (dialogueMenu.activeInHierarchy && dialogueIndex <= currentPopup.popupLines.Length - 1)
        {
            //DisplayText();
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                dialogueIndex++;
                if (dialogueIndex > currentPopup.popupLines.Length - 1)
                {
                    CloseMenu();
                    return;
                }
                textObject.SetText(currentPopup.popupLines[dialogueIndex]);
                textObject.SetVerticesDirty();
                textObject.SetLayoutDirty();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out TextPopup popup))
        {
            popupsInRange.Add(popup.gameObject);

            DisplayText();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out TextPopup popup))
        {
            popupsInRange.Remove(popup.gameObject);

            if (popupsInRange.Count == 0) // If there are no popups in range now, close the dialogue menu
            {
                dialogueIndex = 0;
                dialogueMenu.SetActive(false);
            }
        }
    }

    public void DisplayText()
    {
        if (popupsInRange.Count == 1) // If there is exactly one popup that is in range, just use that by default without calculating what is nearest
        {
            currentPopup = popupsInRange[0].GetComponent<TextPopup>().popupText;

            // Open dialogue menu
        }
        else
        {
            // If there is more than one, calculate which object is closest
            GameObject closestPopup = null;
            float closestDistanceSqr = Mathf.Infinity; // The default closest value is infinity
            Vector3 currentTransform = transform.position;
            foreach (GameObject go in popupsInRange)
            {
                Vector3 distanceToTarget = go.transform.position - currentTransform;
                float dSqr = distanceToTarget.sqrMagnitude;

                // Check if the value calculated is the lower than our current closestDistance
                // If it is, set this popup as the current closest and change the value of closestDistSqr to use the distance between cat and closest popup
                if (dSqr < closestDistanceSqr)
                {
                    closestPopup = go;
                    closestDistanceSqr = dSqr;
                }
            }

            currentPopup = closestPopup.GetComponent<TextPopup>().popupText;
        }

        // Open dialogue menu if its not already open
        OpenMenu();
    }

    public void OpenMenu()
    {
        if (!dialogueMenu.activeInHierarchy) dialogueMenu.SetActive(true);

        // Set the text to display the first string in the PopupSO
        //dialogueIndex = 0;
        textObject.text = currentPopup.popupLines[0];
    }

    public void CloseMenu()
    {
        dialogueIndex = 0;
        dialogueMenu.SetActive(false);
    }
}
