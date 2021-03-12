using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : EnemyAI {
    [Header("Boss AI params")]
    [SerializeField] private float attackDuration = 0.3f;

    void Start() {
        // set the transform to the transform of the target
        target = GameObject.FindGameObjectWithTag(targetTag.value).GetComponent<Transform>();

        // set the initial state to idle
        SetEnemyState(GeneralState.idle);
    }

    void Update() {
        if (BossHealth.isDemonDead) {
            SetEnemyState(GeneralState.dead);

            objectRigidbody.Sleep();
            objectCollider.enabled = false;
        } else {
            SetAnimation();
        }
    }

    void FixedUpdate() {
        if (!PlayerHealth.isPlayerDead) {
            if (!BossHealth.isDemonDead) {
                CheckDistance();
            }
        }
    }

    protected override void SetAnimation() {
        // check if enemy is moving
        if (enemyMovement.magnitude > 0) {
            SetEnemyState(GeneralState.walk);
            enemyAnim.SetAnimParameter("moveX", Mathf.Round(enemyMovement.x));
            enemyAnim.SetAnimParameter("moveY", Mathf.Round(enemyMovement.y));
            enemyAnim.SetAnimParameter("moving", true);
        } else {
            enemyAnim.SetAnimParameter("moving", false);

            // since player is not moving while in attack state and dead state, check state
            if (enemyState.currState != GeneralState.attack && enemyState.currState != GeneralState.dead) {
                SetEnemyState(GeneralState.idle);
            }
        }
    }

    protected override void CheckDistance() {
        // find the distance between the enemy and the target
        targetDistance = Vector3.Distance(transform.position, target.position);

        // if they are within a certain distance move towards the targets position, else if within attack range, else stop moving
        if (targetDistance < followRadius && enemyState.currState != GeneralState.attack && enemyState.currState != GeneralState.dead) {
            Vector2 follow = (Vector2)(target.position - transform.position);

            // set the enemy movement vector to the follow vector
            enemyMovement.x = follow.x;
            enemyMovement.y = follow.y;

            Move(follow);
        } else if (targetDistance < attackRadius) {
            // handle any attack animations here
            enemyMovement = Vector2.zero;
            Move(enemyMovement);

            StartCoroutine(AttackCoroutine());
        } else {
            SetEnemyState(GeneralState.idle);

            enemyMovement = Vector2.zero;
            Move(enemyMovement);
        }
    }

    protected void SetEnemyState(GeneralState newState) {
        enemyState.ChangeState(newState);
    }

    IEnumerator AttackCoroutine() {
        // change players state
        SetEnemyState(GeneralState.attack);
        enemyAnim.SetAnimParameter("attacking", true);

        yield return new WaitForSeconds(attackDuration);

        // after waiting change back to idle state
        SetEnemyState(GeneralState.idle);
        enemyAnim.SetAnimParameter("attacking", false);
    }
}
