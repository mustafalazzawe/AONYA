using UnityEngine;

// this destory a gameobject after a set delay time
public class DestroyAfterDelay : MonoBehaviour {
    [Header("Destroy Delay params")]
    [SerializeField] private float destroyDelay;

    void Update() {        
        // timer
        destroyDelay -= Time.deltaTime;

        // once the delay is over, destroy
        if(destroyDelay <= 0) {
            Destroy(this.gameObject);
        }
    }
}
