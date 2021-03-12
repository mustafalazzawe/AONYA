using UnityEngine;

public class ChangeSprite : MonoBehaviour {
    [Header("Change Sprite params")]
    [SerializeField] public bool isActive;
    [SerializeField] public SpriteRenderer objectSprite;
    [SerializeField] public SpriteValue newSprite;

    // Start is called before the first frame update
    void Start() {
        // set the players sprite render to false
        // objectSprite.enabled = false;
    }

    public virtual void ChangeObjectSprite() {
        // toggle 
        isActive = !isActive;

        if (isActive) {
            DisplaySprite();
        } else {
            DisableSprite();
        }
    }

    public virtual void DisplaySprite() {
        objectSprite.enabled = true;
        objectSprite.sprite = newSprite.value;
    }

    public virtual void DisableSprite() {
        objectSprite.enabled = false;
    }
}
