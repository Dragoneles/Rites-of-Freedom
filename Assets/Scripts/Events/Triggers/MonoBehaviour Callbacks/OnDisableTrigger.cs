/******************************************************************************
 * 
 * File: OnDisableTrigger.cs
 * Author: Joseph Crump
 * Date: 4/08/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  UnityEvent that is invoked when the behavior is disabled.
 *  
 ******************************************************************************/

/// <summary>
/// UnityEvent that is invoked when the behavior is disabled.
/// </summary>
public class OnDisableTrigger : MonoBehaviourCallbackTrigger
{
    private void OnDisable()
    {
        OnTrigger();
    }
}
