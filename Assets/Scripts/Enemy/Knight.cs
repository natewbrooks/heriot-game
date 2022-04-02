using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Enemy
{
    // AI BEHAVIOR (STATES)
    private enum State {
        Attack,
        Block,
        Chase,
        Idle,
        Patrol,
        Walk
    }
    
    private State state = State.Idle;


    private void Update() {
        /* check for target in proximity
            path find and walk towards them
                if distance is small enough stop movement and engage attack stance
                sometimes attack, sometimes block; account for reaction time */

        /* if target get out of range, chase after him. If he successfully leaves the range then 
        patrol the area looking for him */
    }


    public override void TakeHit() {
        animator.SetTrigger("Got Hit?");
    }

    public override void OnDeath() {
        frozen = true;
        animator.SetTrigger("Got Killed?");
        // open the menu or do something
    }

}
