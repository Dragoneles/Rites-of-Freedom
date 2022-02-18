/******************************************************************************
 * 
 * File: Character.cs
 * Author: Joseph Crump
 * Date: 2/04/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Base class for characters that can animate and receive attacks.
 *  
 ******************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for characters that can animate and receive attacks.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour, IAttackable
{
    public class AnimatorTriggers
    {
        public const string Idle = nameof(Idle);
        public const string Attack = nameof(Attack);
        public const string Block = nameof(Block);
        public const string BlockStart = nameof(BlockStart);
    }
    public class AnimatorBooleans
    {
        public const string Attacking = nameof(Attacking);
        public const string Moving = nameof(Moving);
        public const string Jumping = nameof(Jumping);
        public const string Falling = nameof(Falling);
        public const string Blocking = nameof(Blocking);
        public const string Dead = nameof(Dead);
    }
    public class AnimatorIntegers
    {
        public const string AttackCount = nameof(AttackCount);
    }

    [Header("Events")]
    /// <summary>
    /// Event invoked when the character's health value changes.
    /// </summary>
    public SmartUnityEvent<VitalChangedEventArgs> HealthChanged;

    /// <summary>
    /// Event invoked when the y velocity of the character changes.
    /// Used to handle jump/fall animator states.
    /// </summary>
    public SmartUnityEvent<FloatEventArgs> YVelocityChanged;

    /// <summary>
    /// Event invoked when the character touches or leaves the ground.
    /// "True" EventArgs indicates the character touched the ground.
    /// </summary>
    public SmartUnityEvent<BoolEventArgs> GroundStateChanged;

    /// <summary>
    /// Event invoked when the character attacks another character.
    /// TriggeringCharacter is the sender.
    /// </summary>
    public SmartUnityEvent<AttackEventArgs> LaunchedAttack;

    /// <summary>
    /// Event invoked when the character receives an attack.
    /// TriggeringCharacter is the sender.
    /// </summary>
    public SmartUnityEvent<AttackEventArgs> ReceivedAttack;

    /// <summary>
    /// Event invoked when the character flinches from an attack.
    /// </summary>
    public SmartUnityEvent<EventArgs> Flinched;

    /// <summary>
    /// Event invoked when the character blocks an attack.
    /// </summary>
    public SmartUnityEvent<AttackEventArgs> Blocked;

    /// <summary>
    /// Event invoked when the character rolls.
    /// </summary>
    public SmartUnityEvent<EventArgs> Rolled;

    /// <summary>
    /// Event invoked when the character finishes rolling.
    /// </summary>
    public SmartUnityEvent<EventArgs> RollFinished;

    /// <summary>
    /// Event invoked when a character dies.
    /// </summary>
    public SmartUnityEvent<AttackEventArgs> Died;

    [Header("Stats")]
    public Vital Health = new Vital(25);
    public Vital Stamina = new Vital(25);

    [Tooltip("Movement speed (measured in 1/10 unit per point).")]
    public Stat MoveSpeed = new Stat(35);

    [Tooltip("Jump force (measured in 1/10 unit per point).")]
    public Stat JumpVelocity = new Stat(50);

    [Tooltip("Speed of the roll (measured in 1/10 unit per point).")]
    public Stat RollSpeed = new Stat(40);

    [Tooltip("Length of a roll in milliseconds.")]
    public Stat RollDuration = new Stat(70);

    [Header("Weapon")]
    [SerializeField]
    private AttackSequence AttackSequence = new AttackSequence();

    public bool IsDead { get; private set; } = false;
    public bool IsBlocking { get; private set; } = false;
    private float YVelocity { get; set; } = 0f;
    public Vector2 Position { get => transform.position; }

    public FeetCollider Feet { get; private set; }
    private Rigidbody2D Rigidbody { get; set; }
    private Animator Animator { get; set; }

    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Feet = GetComponentInChildren<FeetCollider>();

        Feet.GroundStateChanged += OnFeetGroundStateChanged;
        Health.ValueChanged += OnHealthValueChanged;
    }

    private void Update()
    {
        CheckYVelocity();
    }

    /// <summary>
    /// Set the character's x velocity based on an input direction.
    /// </summary>
    public void Move(float direction)
    {
        Vector2 directionVector = new Vector2(direction, 0f);
        Vector2 velocity = directionVector * (MoveSpeed / 10);
        velocity.y = Rigidbody.velocity.y;

        Rigidbody.velocity = velocity;
    }

    /// <summary>
    /// Set the character's x velocity based on an input direction.
    /// </summary>
    public void Roll(float direction)
    {
        if (direction.IsNegative())
            FaceLeft();
        else if (direction.IsPositive())
            FaceRight();

        float speed = RollSpeed / 10f;
        StartCoroutine(RollCoroutine(direction * speed));
    }

    /// <summary>
    /// Halt the character's x velocity.
    /// </summary>
    public void Stop()
    {
        if (Rigidbody == null)
            return;

        if (!Feet.IsGrounded)
            return;

        Vector2 velocity = Rigidbody.velocity;
        velocity.x = 0f;

        Rigidbody.velocity = velocity;
    }

    /// <summary>
    /// Apply an upwards y-velocity to the player.
    /// </summary>
    public void Jump()
    {
        Vector2 velocity = Rigidbody.velocity;
        float x = velocity.x;
        Rigidbody.velocity = new Vector2(x, (JumpVelocity / 10));
    }

    /// <summary>
    /// Event receiver for a weapon collision event (dispatched via animation).
    /// </summary>
    public virtual void OnWeaponHit(object sender, CollisionEventArgs e)
    {
        // First try the GameObject itself
        IAttackable target = e.GameObject.GetComponent<IAttackable>();

        // then try its parent
        if (target == null)
            target = e.GameObject.GetComponentInParent<IAttackable>();

        // then try its children
        if (target == null)
            target = e.GameObject.GetComponentInChildren<IAttackable>();

        if (target == null)
            return;

        Attack(target);
    }

    /// <summary>
    /// Flip this character's transform in the X-dimension.
    /// </summary>
    public void Flip()
    {
        float x = transform.localScale.x;
        float y = transform.localScale.y;
        float z = transform.localScale.z;
        transform.localScale = new Vector3(-x, y, z);
    }

    /// <summary>
    /// Set this character's local X scale to face left.
    /// </summary>
    public void FaceLeft()
    {
        float x = Mathf.Abs(transform.localScale.x);
        float y = transform.localScale.y;
        float z = transform.localScale.z;
        transform.localScale = new Vector3(-x, y, z);
    }

    /// <summary>
    /// Set this character's local X scale to face right.
    /// </summary>
    public void FaceRight()
    {
        float x = Mathf.Abs(transform.localScale.x);
        float y = transform.localScale.y;
        float z = transform.localScale.z;
        transform.localScale = new Vector3(x, y, z);
    }

    /// <summary>
    /// Handle an incoming attack instance, either blocking the attack or
    /// taking damage.
    /// </summary>
    public void ReceiveAttack(AttackInstance attack)
    {
        if (IsDead)
            return;

        if (IsBlocking && IsFacingPoint(attack.Attacker.Position))
        {
            BlockAttack(attack);
            return;
        }

        TakeDamage(attack.Damage);

        ReceivedAttack?.Invoke(this, new AttackEventArgs(attack, this));

        if (Health <= 0 && !IsDead)
        {
            Died?.Invoke(this, new AttackEventArgs(attack, this));
            IsDead = true;
            return;
        }

        if (!IsDead)
            Flinch();
    }

    /// <summary>
    /// Play the "Hurt" animation and lock the character's controls.
    /// </summary>
    public void Flinch()
    {
        Flinched?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Set whether or not the character is considered to be blocking.
    /// </summary>
    public void SetBlockState(bool value)
    {
        IsBlocking = value;
    }

    /// <summary>
    /// Returns true if the character is left of the specified point.
    /// </summary>
    public bool IsLeftOfPoint(Vector2 point)
    {
        return IsLeftOfPoint(point.x);
    }

    /// <summary>
    /// Returns true if the character is left of the specified point.
    /// </summary>
    public bool IsLeftOfPoint(float point)
    {
        return transform.position.x < point;
    }

    /// <summary>
    /// Returns true if the character is left of the specified point.
    /// </summary>
    public bool IsRightOfPoint(Vector2 point)
    {
        return IsRightOfPoint(point.x);
    }

    /// <summary>
    /// Returns true if the character is left of the specified point.
    /// </summary>
    public bool IsRightOfPoint(float point)
    {
        return transform.position.x > point;
    }

    /// <summary>
    /// Returns true if the character is facing the specified point.
    /// </summary>
    public bool IsFacingPoint(Vector2 point)
    {
        return IsFacingPoint(point.x);
    }

    /// <summary>
    /// Returns true if the character is facing the specified point.
    /// </summary>
    public bool IsFacingPoint(float point)
    {
        return IsLeftOfPoint(point) == transform.localScale.x.IsPositive();
    }

    /// <summary>
    /// Flip the character's facing direction to face the specified point.
    /// </summary>
    public void FacePoint(Vector2 point)
    {
        FacePoint(point.x);
    }

    /// <summary>
    /// Flip the character's facing direction to face the specified point.
    /// </summary>
    public void FacePoint(float point)
    {
        if (IsLeftOfPoint(point))
            FaceRight();
        else if (IsRightOfPoint(point))
            FaceLeft();
    }

    #region Animator Methods
    public bool GetAnimationBool(string boolName)
    {
        if (Animator == null)
            return false;

        return Animator.GetBool(boolName);
    }

    public int GetAnimationInt(string intName)
    {
        if (Animator == null)
            return 0;

        return Animator.GetInteger(intName);
    }

    public void SetAnimationBool(string boolName, bool value)
    {
        if (Animator == null)
            return;

        Animator.SetBool(boolName, value);
    }

    public void SetAnimationInt(string intName, int value)
    {
        if (Animator == null)
            return;

        Animator.SetInteger(intName, value);
    }

    public void SetAnimationTrigger(string triggerName)
    {
        if (Animator == null)
            return;

        Animator.SetTrigger(triggerName);
    }
    #endregion

    /// <summary>
    /// Use the character's weapon to attack a target.
    /// </summary>
    private void Attack(IAttackable target)
    {
        AttackInstance attackInstance = AttackSequence.GetAttackFrom(this);

        target.ReceiveAttack(attackInstance);

        LaunchedAttack.Invoke(this, new AttackEventArgs(attackInstance, this));
    }

    private void BlockAttack(AttackInstance attack)
    {
        Character attacker = attack.Attacker;

        Blocked.Invoke(this, new AttackEventArgs(attack, this));

        attacker.Flinch();
    }

    private void TakeDamage(int amount)
    {
        Health.Subtract(amount);
    }

    private void CheckYVelocity()
    {
        float previousVelocity = YVelocity;
        float currentVelocity = Rigidbody.velocity.y;

        if (previousVelocity == currentVelocity)
            return;

        YVelocity = currentVelocity;

        if (previousVelocity.IsPositive() != currentVelocity.IsPositive())
            YVelocityChanged?.Invoke(this, YVelocity);
    }

    private IEnumerator RollCoroutine(float speed)
    {
        Rolled?.Invoke(this, EventArgs.Empty);

        float timer = RollDuration / 100f;

        while (timer > 0f)
        {
            yield return new WaitForEndOfFrame();

            timer -= Time.deltaTime;

            Vector2 velocity = new Vector2(speed, 0f);
            velocity.y = Rigidbody.velocity.y;

            Rigidbody.velocity = velocity;
        }

        Stop();

        RollFinished?.Invoke(this, EventArgs.Empty);

        yield return null;
    }

    protected virtual void OnFeetGroundStateChanged(object sender, BoolEventArgs e)
    {
        GroundStateChanged?.Invoke(this, e);
    }

    protected virtual void OnHealthValueChanged(object sender, ValueChangedEventArgs e)
    {
        Vital health = sender as Vital;

        if (health == null)
            return;

        var eventArgs = new VitalChangedEventArgs(health, e.PreviousValue, e.CurrentValue);
        HealthChanged?.Invoke(this, eventArgs);
    }
}
