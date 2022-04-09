/******************************************************************************
 * 
 * File: EnableTrigger.cs
 * Author: Joseph Crump
 * Date: 4/08/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  UnityEvent that is invoked when the behavior is enabled.
 *  
 ******************************************************************************/

/// <summary>
/// UnityEvent that is invoked when the behavior is enabled.
/// </summary>
public class OnEnableTrigger : MonoBehaviourCallbackTrigger
{
    private void OnEnable()
    {
        OnTrigger();
    }
}
