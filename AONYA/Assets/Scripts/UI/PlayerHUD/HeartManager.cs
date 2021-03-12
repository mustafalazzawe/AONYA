using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour {
    [Header("Heart Manager params")]
    [SerializeField] private FloatValue maxPlayerHealth;
    [SerializeField] private FloatValue currPlayerHealth;
    [SerializeField] private Transform heartHolder;
    [Space]
    [SerializeField] private Image[] heartImages;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite emptyHeart;

    private void OnValidate() {
        if(heartHolder != null)
            heartImages = heartHolder.GetComponentsInChildren<Image>();
    }

    public void UpdateHearts() {
        float full = maxPlayerHealth.value / 2;
        for (int i = 0; i < heartImages.Length; i++) {
            // enable the first default 3, the rest are bonus
            if (i < full) {
                heartImages[i].enabled = true;
            } else {
                heartImages[i].enabled = false;
            }
        }

        float halves = currPlayerHealth.value;
        for (int i = 0; i < full; i++) {
            if (halves >= 2) {
                halves -= 2;
                heartImages[i].sprite = fullHeart;
            } else if (halves == 1) {
                halves -= 1;
                heartImages[i].sprite = halfHeart;
            } else {
                heartImages[i].sprite = emptyHeart;
            }
        }
    }
}
