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

    private Character self;
    private Character target;
    private AIInputHandler input;

    private bool pathIsBlocked = false;

    protected override void OnInitialize()
    {
        self = Blackboard.Get(EnemyBehaviorTree.Self);
        input = Blackboard.Get(EnemyBehaviorTree.Input);
    }

    protected override void OnNodeEntered()
    {
        target = Blackboard.Get(EnemyBehaviorTree.Target);

        if (CheckPathIsBlocked() == true)
            pathIsBlocked = true;
    }

    protected override void OnReset()
    {
        pathIsBlocked = false;
    }

    protected override IEnumerator Run()
    {
        input.PerformMove(GetFleeDirection());

        yield return new WaitForEndOfFrame();
    }

    protected override bool CheckNodeFailed()
    {
        return self.IsDead || pathIsBlocked;
    }

    protected override bool CheckNodeSucceeded()
    {
        float distance = Vector2.Distance(self.Position, target.Position);
        bool inRange = distance >= targetDistance;

        if (inRange)
            input.PerformMove(0f);

        return inRange;
    }

    private float GetFleeDirection()
    {
        switch (self.IsLeftOfPoint(target.Position))
        {
            case true:
                return Direction.Left;

            case false:
                return Direction.Right;
        }
    }

    private bool CheckPathIsBlocked()
    {
        float pathDirection = GetFleeDirection();

        Vector2 raycastOrigin = self.Position + new Vector2(0f, 0.5f);
        Vector2 raycastDirection = new(pathDirection, 0f);
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
