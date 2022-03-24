﻿/******************************************************************************
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
using UnityEngine;

/// <summary>
/// Base class for characters that can animate and receive attacks.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour, IAttackable
{
    [Header("Events")]
    /// <summary>
    /// Event invoked when the character's health value changes.
    /// </summary>
    public SmartUnityEvent<VitalChangedEventArgs> HealthChanged;

    /// <summary>
    /// Event invoked when the y velocity of the character changes.
    /// Used to handle jump/fall animator states.
    /// </summary>
    public SmartUnityEvent<float> YVelocityChanged;

    /// <summary>
    /// Event invoked when the character touches or leaves the ground.
    /// "True" EventArgs indicates the character touched the ground.
    /// </summary>
    public SmartUnityEvent<bool> GroundStateChanged;

    /// <summary>
    /// Event invoked when the character attacks another character.
    /// TriggeringCharacter is the sender.
    /// </summary>
    public SmartUnityEvent<AttackEventArgs> DeliveredAttack;

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
    /// Event invoked when the character dodges an attack by rolling.
    /// </summary>
    public SmartUnityEvent<AttackEventArgs> Dodged;

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
    public SmartUnityEvent<EventArgs> Died;

    /// <summary>
    /// Event invoked when the character lands.
    /// </summary>
    public SmartUnityEvent<EventArgs> Landed;

    [Header("Stats")]
    public Vital Health = new Vital(100);
    public Vital Stamina = new Vital(80);

    [Tooltip("Rate that stamina regenerates per second.")]
    public Stat StaminaRegen = new Stat(30);

    [Tooltip("Rate thta stamina regenerates per second while blocking.")]
    public Stat StaminaRegenWhileBlocking = new Stat(15);

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
    public bool IsRolling { get; private set; } = false;
    private float YVelocity { get; set; } = 0f;
    public Vector2 Position { get => transform.position; }

    public FeetCollider Feet { get; private set; }
    public VirtualInputHandler Input { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    private Animator Animator { get; set; }
    private SpriteHandler SpriteHandler { get; set; }

    private void Awake()
    {
        Feet = GetComponentInChildren<FeetCollider>();
        Input = GetComponentInChildren<VirtualInputHandler>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();
        SpriteHandler = GetComponentInChildren<SpriteHandler>();

        Feet.GroundStateChanged += OnFeetGroundStateChanged;
        Health.ValueChanged += OnHealthValueChanged;
    }

    private void Update()
    {
        CheckYVelocity();
        RegenerateStamina();
        UpdateFacingDirection();
    }

    /// <summary>
    /// Kill this character via cheat.
    /// </summary>
    public void CheatKill()
    {
        TakeDamage(999999);
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
        if (direction == 0f)
            direction = GetFacingDirection();
        else
            direction = direction.AsDirection();

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

        if (IsRolling)
        {
            DodgeAttack(attack);
            return;
        }

        TakeDamage(attack.Damage);

        ReceivedAttack?.Invoke(new AttackEventArgs(attack, this));

        if (!IsDead)
            Flinch();
    }

    /// <summary>
    /// Play the "Hurt" animation and lock the character's controls.
    /// </summary>
    public void Flinch()
    {
        Flinched?.Invoke(EventArgs.Empty);
    }

    /// <summary>
    /// Set whether or not the character is considered to be blocking.
    /// </summary>
    public void SetBlockState(bool value)
    {
        IsBlocking = value;
    }

    public void Flip() => SpriteHandler.Flip();
    public float GetFacingDirection() => SpriteHandler.GetFacingDirection();
    public void FaceDirection(float direction) => SpriteHandler.FaceDirection(direction);
    public void FaceLeft() => SpriteHandler.FaceLeft();
    public void FaceRight() => SpriteHandler.FaceRight();
    public bool IsLeftOfPoint(Vector2 point) => SpriteHandler.IsLeftOfPoint(point);
    public bool IsFacingPoint(Vector2 point) => SpriteHandler.IsFacingPoint(point);
    public void FacePoint(Vector2 point) => SpriteHandler.FacePoint(point);

    /// <summary>
    /// Use the character's weapon to attack a target.
    /// </summary>
    private void Attack(IAttackable target)
    {
        AttackInstance attackInstance = AttackSequence.GetAttackFrom(this);

        target.ReceiveAttack(attackInstance);

        DeliveredAttack.Invoke(new AttackEventArgs(attackInstance, this));
    }

    private void UpdateFacingDirection()
    {
        float facingDirection = GetFacingDirection();
        float moveDirection = Rigidbody.velocity.x;

        float arbitraryInsignificantMagnitude = 0.01f;
        if (Mathf.Abs(moveDirection) < arbitraryInsignificantMagnitude)
            return;

        if (moveDirection.IsPositive() != facingDirection.IsPositive())
            Flip();
    }

    private void BlockAttack(AttackInstance attack)
    {
        Character attacker = attack.Attacker;

        Blocked.Invoke(new AttackEventArgs(attack, this));

        attacker.Flinch();
    }

    private void DodgeAttack(AttackInstance attack)
    {
        Dodged.Invoke(new AttackEventArgs(attack, this));
    }

    private void TakeDamage(float amount)
    {
        Health.Subtract(amount);

        if (Health <= 0 && !IsDead)
        {
            Died?.Invoke(EventArgs.Empty);
            IsDead = true;
            return;
        }
    }

    private void CheckYVelocity()
    {
        float previousVelocity = YVelocity;
        float currentVelocity = Rigidbody.velocity.y;

        if (previousVelocity == currentVelocity)
            return;

        YVelocity = currentVelocity;

        YVelocityChanged?.Invoke(YVelocity);
    }

    private void RegenerateStamina()
    {
        if (IsBlocking)
        {
            Stamina.Add(StaminaRegenWhileBlocking * Time.deltaTime);
            return;
        }

        Stamina.Add(StaminaRegen * Time.deltaTime);
    }

    private IEnumerator RollCoroutine(float speed)
    {
        Rolled?.Invoke(EventArgs.Empty);

        IsRolling = true;

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

        IsRolling = false;

        RollFinished?.Invoke(EventArgs.Empty);

        yield return null;
    }

    protected virtual void OnFeetGroundStateChanged(object sender, BoolEventArgs e)
    {
        GroundStateChanged?.Invoke(e);

        if (e)
            Landed?.Invoke(EventArgs.Empty);
    }

    protected virtual void OnHealthValueChanged(object sender, ValueChangedEventArgs e)
    {
        Vital health = sender as Vital;

        if (health == null)
            return;

        var eventArgs = new VitalChangedEventArgs(health, e.PreviousValue, e.CurrentValue);
        HealthChanged?.Invoke(eventArgs);
    }
}
