/******************************************************************************
 * 
 * File: OnDestroyTrigger.cs
 * Author: Joseph Crump
 * Date: 4/08/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  UnityEvent that is invoked when the behavior is destroyed.
 *  
 ******************************************************************************/

/// <summary>
/// UnityEvent that is invoked when the behavior is destroyed.
/// </summary>
public class OnDestroyTrigger : MonoBehaviourCallbackTrigger
{
    private void OnDestroy()
    {
        OnTrigger();
    }
}
