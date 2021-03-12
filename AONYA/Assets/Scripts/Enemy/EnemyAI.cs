using System;
using UnityEngine;

public class EnemyAI : Movement {
    [Header("Enemy AI params")]
    [SerializeField] protected StringValue targetTag;
    [Space]
    [SerializeField] protected float followRadius;
    [SerializeField] protected float attackRadius;
    [SerializeField] protected AnimatorController enemyAnim;
    [SerializeField] protected StateMachine enemyState;

    protected float targetDistance;
    protected Transform target;

    protected Vector2 enemyMovement;

    // Start is called before the first frame update
    void Start() {
        // set the transform to the transform of the target
        target = GameObject.FindGameObjectWithTag(targetTag.value).GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update() {
        SetAnimation();
    }

    void FixedUpdate() {
        if (!PlayerHealth.isPlayerDead) {
            CheckDistance();
        } 
    }

    protected virtual void SetAnimation() {
        // check if enemy is moving
        if (enemyMovement.magnitude > 0) {
            enemyAnim.SetAnimParameter("moveX", Mathf.Round(enemyMovement.x));
            enemyAnim.SetAnimParameter("moveY", Mathf.Round(enemyMovement.y));
            enemyAnim.SetAnimParameter("moving", true);
        } else {
            enemyAnim.SetAnimParameter("moving", false);
        }
    }

    protected virtual void CheckDistance() {
        // find the distance between the enemy and the target
        targetDistance = Vector3.Distance(transform.position, target.position);

        if (enemyState.currState != GeneralState.attack && enemyState.currState != GeneralState.dead) {
            // if they are within a certain distance move towards the targets position, else if within attack range, else stop moving
            if (targetDistance < followRadius) {
                Vector2 follow = (Vector2)(target.position - transform.position);

                // set the enemy movement vector to the follow vector
                enemyMovement.x = follow.x;
                enemyMovement.y = follow.y;

                Move(follow);
            } else if (targetDistance < attackRadius) {
                // handle any attack animations here
                enemyMovement = Vector2.zero;
                Move(enemyMovement);

            } else {
                enemyMovement = Vector2.zero;
                Move(enemyMovement);
            }
        }

    }

    // just an extra function so we can visually see the different radii
    private void OnDrawGizmos() {
        // attack radius
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, attackRadius);

        // follow radius
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, followRadius);
    }
}
