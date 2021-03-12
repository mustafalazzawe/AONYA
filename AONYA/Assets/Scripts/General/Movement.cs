using UnityEngine;

public class Movement : MonoBehaviour {
    [Header("Movement params")]
    [SerializeField] protected float movementSpeed = 3f;
    [SerializeField] protected Rigidbody2D objectRigidbody;
    [SerializeField] protected Collider2D objectCollider;

    // this function moves the gameobject in the direction assigned
    public void Move(Vector2 direction) {
        // normalized prevents diagnol speed increase 
        direction = direction.normalized;
        objectRigidbody.velocity = direction * movementSpeed;
    }
}
