using System.Collections;
using UnityEngine;

public class PlayerMovement : Movement {
    [Header("Player Movement params")]
    [SerializeField] private float attackDuration = 0.3f;
    [SerializeField] private AnimatorController playerAnim;
    [SerializeField] private StateMachine playerState;
    [SerializeField] private ReceiveItem receiveThisItem;
    [SerializeField] private AudioSource attackSound;

    [Header("Freeze Movement")]
    [SerializeField] private BoolValue freezeValue;

    public static bool freezeMovement;

    // initialze the temporary movement to down (idle down, attack down, etc)
    private Vector2 playerMovement;

    // Start is called before the first frame update
    void Start() {
        freezeMovement = freezeValue.value;

        // set the initial state to idle
        SetPlayerState(GeneralState.idle);

        // set the inital idle to be down (default idle)
        IdleDown();
    }

    // Update is called once per frame
    void Update() {
        // checks if were in the received item state
        if (playerState.currState == GeneralState.receiveItem) {
            // if we press the interact key again, we revert the state back to idle
            if (Input.GetButtonDown("Interact")) {
                SetPlayerState(GeneralState.idle);
                playerAnim.SetAnimParameter("receiveItem", false);
                receiveThisItem.ChangeObjectSprite();

                // reset to idle down
                IdleDown();
            }
            return;
        }

        if (PlayerHealth.isPlayerDead) {
            SetPlayerState(GeneralState.dead);

            objectRigidbody.Sleep();
            objectCollider.enabled = false;
        } else {
            ProcessInputs();
            SetAnimation();
        }

        if (DeathScreen.pressedRetry) {
            SetPlayerState(GeneralState.idle);

            PlayerHealth.isPlayerDead = false;

            objectRigidbody.WakeUp();
            objectCollider.enabled = true;

            DeathScreen.pressedRetry = false;
        }
    }

    void IdleDown() {
        playerMovement = Vector2.down;

        playerAnim.SetAnimParameter("moveX", Mathf.Round(playerMovement.x));
        playerAnim.SetAnimParameter("moveY", Mathf.Round(playerMovement.y));
    }

    // setting player state, mainly for cleaner code
    void SetPlayerState(GeneralState newState) {
        playerState.ChangeState(newState);
    }

    void ProcessInputs() {
        if (!freezeMovement) {
            if (playerState.currState != GeneralState.receiveItem) {
                // check if input is attack button (space bar)
                if (Input.GetButtonDown("SwordAttack")) {
                    StartCoroutine(AttackCoroutine());

                    // set the movement to zero so the player isnt moving during the 
                    // duration of the attack
                    playerMovement = Vector2.zero;
                    Move(playerMovement);
                }

                // check if the current state isnt attack, we use this to prevent the
                // player from being in both attack and walk states 
                else if (playerState.currState != GeneralState.attack) {
                    // process directional keys (wasd, arrows) -- use getaxisraw, 
                    // to prevent floaty movement, emulating old school NES games
                    playerMovement.x = Input.GetAxisRaw("Horizontal");
                    playerMovement.y = Input.GetAxisRaw("Vertical");
                    Move(playerMovement);
                }

                // else the player wont be moving (idle in any direction)
                else {
                    playerMovement = Vector2.zero;
                    Move(playerMovement);
                }
            }
        }
    }

    void SetAnimation() {
        // check if player is moving
        if (playerMovement.magnitude > 0) {
            SetPlayerState(GeneralState.walk);
            playerAnim.SetAnimParameter("moveX", Mathf.Round(playerMovement.x));
            playerAnim.SetAnimParameter("moveY", Mathf.Round(playerMovement.y));
            playerAnim.SetAnimParameter("moving", true);
        } else {
            playerAnim.SetAnimParameter("moving", false);

            // since player is not moving while in attack state and dead state, check state
            if (playerState.currState != GeneralState.attack && playerState.currState != GeneralState.dead) {
                SetPlayerState(GeneralState.idle);
            }
        }
    }

    IEnumerator AttackCoroutine() {
        // change players state
        SetPlayerState(GeneralState.attack);
        playerAnim.SetAnimParameter("attacking", true);

        attackSound.Play();
        yield return null;

        yield return new WaitForSeconds(attackDuration);

        // after waiting change back to idle state
        SetPlayerState(GeneralState.idle);
        playerAnim.SetAnimParameter("attacking", false);
    }
}
