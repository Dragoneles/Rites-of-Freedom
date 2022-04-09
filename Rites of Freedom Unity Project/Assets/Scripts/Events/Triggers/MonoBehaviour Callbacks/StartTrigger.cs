/******************************************************************************
 * 
 * File: StartTrigger.cs
 * Author: Joseph Crump
 * Date: 2/20/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  UnityEvent that invokes on Start.
 *  
 ******************************************************************************/

/// <summary>
/// UnityEvent that invokes on Start.
/// </summary>
public class StartTrigger : MonoBehaviourCallbackTrigger
{
    private void Start()
    {
        OnTrigger();
    }
}
