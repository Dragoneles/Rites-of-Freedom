/******************************************************************************
 * 
 * File: AIInputHandler.cs
 * Author: Joseph Crump
 * Date: 3/22/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Virtual input behavior for the AI, counterpart to PlayerInput.
 *  
 ******************************************************************************/

/// <summary>
/// Virtual input behavior for the AI, counterpart to PlayerInput.
/// </summary>
public class AIInputHandler : VirtualInputHandler
{
    private const float PressLength = 0.1f;

    /// <summary>
    /// Simulate a move input.
    /// </summary>
    /// <param name="direction">
    /// The direction of the move axis input.
    /// </param>
    public void PerformMove(float direction)
    {
        MovementAxis.SetAxisValue(direction);
    }

    /// <summary>
    /// Simulate pressing the jump button.
    /// </summary>
    public void PerformJump()
    {
        Jump.SimulatePress(this, PressLength);
    }

    /// <summary>
    /// Simulate holding the block button.
    /// </summary>
    /// <param name="holdDuration">
    /// Duration of the hold.
    /// </param>
    public void PerformBlock(float holdDuration)
    {
        Block.SimulatePress(this, holdDuration);
    }

    /// <summary>
    /// Simulate pressing the roll button.
    /// </summary>
    public void PerformRoll()
    {
        Roll.SimulatePress(this, PressLength);
    }

    /// <summary>
    /// Simulate pressing the attack button.
    /// </summary>
    public void PerformAttack()
    {
        Attack.SimulatePress(this, PressLength);
    }

    /// <summary>
    /// Simulate pressing the interact button.
    /// </summary>
    public void PerformInteract()
    {
        Interact.SimulatePress(this, PressLength);
    }
}
