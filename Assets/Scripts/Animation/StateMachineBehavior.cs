/******************************************************************************
 * 
 * File: StateMachineBehavior.cs
 * Author: Joseph Crump
 * Date: 3/14/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Extension of StateMachineBehaviour that initializes a reference to a 
 *  specific component, as well as the object's transform, gameObject, 
 *  and animator.
 *  
 ******************************************************************************/
using UnityEngine;
using UnityEngine.Animations;

/// <summary>
/// Extension of <see cref="StateMachineBehaviour"/> that initializes a 
/// reference to a specific component, as well as the object's transform, 
/// gameObject, and animator.
/// </summary>
public class StateMachineBehavior<T> : StateMachineBehaviour where T : Component
{
    protected T context { get; private set; }
    protected Transform transform { get; private set; }
    protected GameObject gameObject { get; private set; }
    protected Animator animator { get; private set; }

    private bool initialized { get; set; } = false;

    protected bool GetBool(int id) => animator.GetBool(id);
    protected bool GetBool(string name) => animator.GetBool(name);
    protected int GetInteger(int id) => animator.GetInteger(id);
    protected int GetInteger(string name) => animator.GetInteger(name);
    protected float GetFloat(int id) => animator.GetFloat(id);
    protected float GetFloat(string name) => animator.GetFloat(name);

    protected void SetBool(int id, bool value) => animator.SetBool(id, value);
    protected void SetBool(string name, bool value) => animator.SetBool(name, value);
    protected void SetInteger(int id, int value) => animator.SetInteger(id, value);
    protected void SetInteger(string name, int value) => animator.SetInteger(name, value);
    protected void SetFloat(int id, float value) => animator.SetFloat(id, value);
    protected void SetFloat(string name, float value) => animator.SetFloat(name, value);
    protected void SetTrigger(int id) => animator.SetTrigger(id);
    protected void SetTrigger(string name) => animator.SetTrigger(name);
    protected void ResetTrigger(int id) => animator.ResetTrigger(id);
    protected void ResetTrigger(string name) => animator.ResetTrigger(name);

    public void IncrementInteger(string name) => SetInteger(name, GetInteger(name) + 1);
    public void DecrementInteger(string name) => SetInteger(name, GetInteger(name) - 1);
    public void ResetInteger(string name) => SetInteger(name, 0);
    public void ToggleBool(string name) => SetBool(name, !GetBool(name));
    public void EnableBool(string name) => SetBool(name, true);
    public void DisableBool(string name) => SetBool(name, false);

    public override sealed void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        if (!initialized)
        {
            context = animator.GetComponentInParent<T>();

            if (context == null)
            {
                Debug.LogError($"{animator.gameObject} or its parent need a component of type {typeof(T)}");
                return;
            }

            this.transform = context.transform;
            this.gameObject = transform.gameObject;
            this.animator = animator;

            initialized = true;

            OnStateInitialized(stateInfo, layerIndex);
        }

        OnStateEntered(stateInfo, layerIndex);
    }

    public override sealed void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        OnStateEnter(animator, stateInfo, layerIndex);
    }

    protected virtual void OnStateInitialized(AnimatorStateInfo stateInfo, int layerIndex) { }

    protected virtual void OnStateEntered(AnimatorStateInfo stateInfo, int layerIndex) { }
}
