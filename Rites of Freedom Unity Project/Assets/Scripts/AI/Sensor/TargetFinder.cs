/******************************************************************************
 * 
 * File: TargetFinder.cs
 * Author: Joseph Crump
 * Date: 4/09/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Basic target detection system used to select targets for the behavior tree.
 *  
 ******************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Basic target detection system used to select targets for the behavior tree.
/// </summary>
[RequireComponent(typeof(EnemyBehaviorTree))]
public class TargetFinder : MonoBehaviour
{
    public enum TargetingType { Weakest, Nearest, Strongest }
    public enum TargetFilterType { Enemy, Ally }

    [SerializeField]
    private EnemyBehaviorTree behaviorTree;

    [SerializeField]
    private TargetingType targetingType = TargetingType.Nearest;

    [SerializeField]
    private TargetFilterType targetFilter = TargetFilterType.Enemy;

    /// <summary>
    /// The behavior tree's current active target.
    /// </summary>
    public Character CurrentTarget => behaviorTree.GetTarget();

    /// <summary>
    /// The character that uses the behavior tree.
    /// </summary>
    public Character Self => behaviorTree.GetSelf();

    [Header("Readonly")]
    [SerializeField]
    private List<Character> validTargets = new();

    private void OnValidate()
    {
        behaviorTree = GetComponent<EnemyBehaviorTree>();
    }

    private void Start()
    {
        SelectNewTarget();
    }

    /// <summary>
    /// Change this target finder's alignment filter to look for allies.
    /// </summary>
    public void SetFilterToAlly()
    {
        this.targetFilter = TargetFilterType.Ally;
        SelectNewTarget();
    }

    /// <summary>
    /// Change this target finder's alignment filter to look for enemies.
    /// </summary>
    public void SetFilterToEnemy()
    {
        this.targetFilter = TargetFilterType.Enemy;
        SelectNewTarget();
    }

    private void SelectNewTarget()
    {
        FindValidCharacters();
        SetTarget(SelectTarget());
    }

    private void FindValidCharacters()
    {
        Character[] characters = FindObjectsOfType<Character>();

        switch (targetFilter)
        {
            case TargetFilterType.Enemy:
                validTargets = characters
                    .Where(o => Self.IsEnemyOfCharacter(o))
                    .ToList();
                break;

            case TargetFilterType.Ally:
                validTargets = characters
                    .Where(o => !Self.IsEnemyOfCharacter(o))
                    .Where(o => o != Self)
                    .ToList();
                break;

            default:
                break;
        }
        
    }

    private Character SelectTarget()
    {
        if (validTargets.Count == 0)
            return null;

        switch (targetingType)
        {
            case TargetingType.Weakest:
                return 
                    validTargets
                        .OrderBy(target => target.Health)
                        .First();

            case TargetingType.Nearest:
                return 
                    validTargets
                        .OrderBy(target => Vector3.Distance(Self.transform.position, target.transform.position))
                        .First();

            case TargetingType.Strongest:
                return 
                    validTargets
                        .OrderByDescending(target => target.Health)
                        .First();

            default:
                break;
        }

        return validTargets[0];
    }

    private void SetTarget(Character target)
    {
        if (CurrentTarget != null)
            CurrentTarget.Died.RemoveListener(OnTargetDeath);


        behaviorTree.SetTarget(target);

        if (CurrentTarget != null)
            CurrentTarget.Died.AddListener(OnTargetDeath);
    }

    private void OnTargetDeath(EventArgs e)
    {
        SelectNewTarget();
    }
}
