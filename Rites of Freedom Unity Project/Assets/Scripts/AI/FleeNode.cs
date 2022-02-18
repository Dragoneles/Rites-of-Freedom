/******************************************************************************
 * 
 * File: FleeNode.cs
 * Author: Joseph Crump
 * Date: 2/15/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Leaf node that instructs the AI to flee a certain distance from their
 *  opponent.
 *  
 ******************************************************************************/
using AI.BehaviorTree;
using System.Collections;
using UnityEngine;

/// <summary>
/// Leaf node that instructs the AI to flee a certain distance from their
/// opponent.
/// </summary>
public class FleeNode : LeafNode
{
    [SerializeField]
    private float targetDistance = 3f;

    [SerializeField]
    private LayerMask movementBlockerLayers;

    private const float Left = -1f;
    private const float Right = 1f;

    private CharacterStateMachineManager stateMachine { get; set; }
    private Character self { get; set; }
    private Character target { get; set; }

    private bool pathIsBlocked { get; set; } = false;

    protected override void OnInitialize()
    {
        stateMachine = blackboard.Get<CharacterStateMachineManager>(EnemyBehaviorTree.StateMachine);
        self = blackboard.Get<Character>(EnemyBehaviorTree.Self);
        target = blackboard.Get<Character>(EnemyBehaviorTree.Target);
    }

    protected override void Start()
    {
        if (CheckPathIsBlocked() == true)
            pathIsBlocked = true;
    }

    protected override void OnReset()
    {
        pathIsBlocked = false;
    }

    protected override IEnumerator Run()
    {
        stateMachine.Move(GetFleeDirection());

        yield return new WaitForEndOfFrame();
    }

    protected override bool CheckNodeFailed()
    {
        return self.IsDead || pathIsBlocked;
    }

    protected override bool CheckNodeSucceeded()
    {
        bool inRange = Vector2.Distance(self.Position, target.Position) >= targetDistance;

        if (inRange)
            stateMachine.StopMoving();

        return inRange;
    }

    private float GetFleeDirection()
    {
        switch (self.IsLeftOfPoint(target.Position))
        {
            case true:
                return Left;

            case false:
                return Right;
        }
    }

    private bool CheckPathIsBlocked()
    {
        float pathDirection = GetFleeDirection();

        Vector2 raycastOrigin = self.Position + new Vector2(0f, 0.5f);
        Vector2 raycastDirection = new Vector2(pathDirection, 0f);
        RaycastHit2D[] hits = Physics2D.RaycastAll(
            raycastOrigin, 
            raycastDirection, 
            targetDistance, 
            movementBlockerLayers
            );

        foreach (RaycastHit2D hit in hits)
        {
            int hitLayer = hit.collider.gameObject.layer;
            if (movementBlockerLayers.ContainsLayer(hitLayer))
            {
                return true;
            }
        }

        return false;
    }
}
