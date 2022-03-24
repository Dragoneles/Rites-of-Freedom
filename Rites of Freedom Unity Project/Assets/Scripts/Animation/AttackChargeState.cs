/******************************************************************************
 * 
 * File: AttackState.cs
 * Author: Joseph Crump
 * Date: 3/14/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Behavior driving a state for a character's attack.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behavior driving a state for a character's attack.
/// </summary>
public class AttackChargeState : CharacterStateMachineBehavior
{
    [SerializeField]
    [Tooltip(
        "The curve determining how charge time translates to " +
        "animation speed.")]
    private AnimationCurve animationSpeedOverDuration;

    [SerializeField]
    [Tooltip(
        "The minimum time the button must be held to perform a " +
        "charge attack.")]
    private float minChargeTime = 0.4f;

    [SerializeField]
    [Tooltip("The maximum time an attack can be charged.")]
    private float maxChargeTime = 1.5f;

    private float timeHeld { get; set; } = 0f;
    private bool attackFinished { get; set; } = false;

    protected override void OnStateEntered(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeHeld = 0f;
        attackFinished = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (attackFinished)
            return;

        if (Input.Attack.WasReleased)
        {
            ExecuteAttack();
            return;
        }

        if (Input.Attack.IsDown)
            timeHeld += Time.deltaTime;

        if (timeHeld >= maxChargeTime)
        {
            ExecuteAttack();
            return;
        }
    }

    private void ExecuteAttack()
    {
        attackFinished = true;

        if (timeHeld >= minChargeTime)
        {
            SetTrigger(Trigger.HeavyAttack);
            return;
        }

        SetTrigger(Trigger.LightAttack);
    }
}
