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

    [SerializeField]
    private EnemyBehaviorTree behaviorTree;

    [SerializeField]
    private TargetingType targetingType = TargetingType.Nearest;

    /// <summary>
    /// The behavior tree's current active target.
    /// </summary>
    public Character CurrentTarget => behaviorTree.GetTarget();

    /// <summary>
    /// The character that uses the behavior tree.
    /// </summary>
    public Character Self => behaviorTree.GetSelf();

    private List<Character> enemyCharacters = new();

    private void OnValidate()
    {
        behaviorTree = GetComponent<EnemyBehaviorTree>();
    }

    private void Start()
    {
        FindEnemyCharacters();
        SelectNewTarget();
    }

    private void SelectNewTarget()
    {
        SetTarget(SelectTarget());
    }

    private void FindEnemyCharacters()
    {
        Character[] characters = FindObjectsOfType<Character>();

        enemyCharacters = characters
            .Where(o => Self.IsEnemyOfCharacter(o))
            .ToList();
    }

    private Character SelectTarget()
    {
        if (enemyCharacters.Count == 0)
            return null;

        switch (targetingType)
        {
            case TargetingType.Weakest:
                enemyCharacters.OrderBy(enemy => enemy.Health);
                return enemyCharacters[0];

            case TargetingType.Nearest:
                enemyCharacters.OrderBy(
                    enemy => Vector3.Distance(transform.position, enemy.transform.position));
                return enemyCharacters[0];

            case TargetingType.Strongest:
                enemyCharacters.OrderByDescending(enemy => enemy.Health);
                return enemyCharacters[0];

            default:
                break;
        }

        return enemyCharacters[0];
    }

    private void SetTarget(Character target)
    {
        if (CurrentTarget != null)
            CurrentTarget.Died.RemoveListener(OnTargetDeath);

        behaviorTree.SetTarget(target);

        CurrentTarget.Died.AddListener(OnTargetDeath);
    }

    private void OnTargetDeath(EventArgs e)
    {
        SelectNewTarget();
    }
}
