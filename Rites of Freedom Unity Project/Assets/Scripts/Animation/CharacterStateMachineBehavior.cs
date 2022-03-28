/******************************************************************************
 * 
 * File: CharacterStateMachineBehavior.cs
 * Author: Joseph Crump
 * Date: 3/14/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Animator StateMachineBehavior designed as a tool for interfacing with 
 *  the Character class.
 *  
 ******************************************************************************/

/// <summary>
/// Animator StateMachineBehavior designed as a tool for interfacing with 
/// the Character class.
/// </summary>
public class CharacterStateMachineBehavior : StateMachineBehavior<Character>
{
    public static class Boolean
    {
        public const string Attacking = nameof(Attacking);
        public const string Moving = nameof(Moving);
        public const string Blocking = nameof(Blocking);
        public const string Grounded = nameof(Grounded);
        public const string Dead = nameof(Dead);
    }
    public static class Integer
    {
        public const string AttackCount = nameof(AttackCount);
    }
    public static class Float
    {
        public const string AttackAnimSpeed = nameof(AttackAnimSpeed);
        public const string MoveAnimSpeed = nameof(MoveAnimSpeed);
        public const string StunAnimSpeed = nameof(StunAnimSpeed);
        public const string JumpDirection = nameof(JumpDirection);
        public const string StunDuration = nameof(StunDuration);
    }
    public static class Trigger
    {
        public const string Roll = nameof(Roll);
        public const string Flinch = nameof(Flinch);
        public const string StartAttack = nameof(StartAttack);
        public const string LightAttack = nameof(LightAttack);
        public const string HeavyAttack = nameof(HeavyAttack);
        public const string Block = nameof(Block);
    }

    protected Character character => context;
    protected VirtualInputHandler Input => context.Input;
}
