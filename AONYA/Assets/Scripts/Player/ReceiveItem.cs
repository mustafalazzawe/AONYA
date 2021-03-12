using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveItem : ChangeSprite {
    [Header("Received Item params")]
    [SerializeField] private GameEvent dialogGameEvent;
    [SerializeField] public AnimatorController spriteAnim;
    [SerializeField] public StateMachine objectState;

    public override void DisplaySprite() {
        // set the new state
        objectState.ChangeState(GeneralState.receiveItem);

        // display new sprite
        base.DisplaySprite();

        // play new sprites animation
        spriteAnim.SetAnimParameter("receiveItem", true);

        // send a dialog game event alert
        dialogGameEvent.Raise();
    }

    public override void DisableSprite() {
        // set the new state
        objectState.ChangeState(GeneralState.idle);

        // disable new sprite
        base.DisableSprite();

        // send a dialog game event alert
        dialogGameEvent.Raise();
    }
}
