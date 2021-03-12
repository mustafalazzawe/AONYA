using UnityEngine;
using TMPro;

public class DialogController : MonoBehaviour {
    [Header("Dialog Controller params")]
    [SerializeField] private bool dialogActive = false;
    [SerializeField] private StringValue dialogString;
    [SerializeField] private GameEvent dialogGameEvent;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private GameObject dialogBox;

    public void ActivateDialog() {
        // toggle 
        dialogActive = !dialogActive;

        // if active, set, else deactivate 
        if(dialogActive) {
            SetDialog();
        } else {
            DeactivateDilog();
        }
    }

    public void SetDialog() {
        // show the box
        dialogBox.SetActive(true);

        // set the text
        dialogText.text = dialogString.value;
    }

    public void DeactivateDilog() {
        dialogBox.SetActive(false);
    }
}
