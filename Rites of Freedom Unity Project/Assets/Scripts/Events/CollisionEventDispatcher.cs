using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Object that sends a message after triggering a collision event.
/// </summary>
public class CollisionEventDispatcher : MonoBehaviour
{
    [SerializeField]
    private bool includeTriggers = true;

    [SerializeField]
    private bool includeCollisions = true;

    [SerializeField]
    private LayerMask layersDetected;

    [SerializeField]
    private List<Collider2D> ignoredColliders = new List<Collider2D>();

    [SerializeField]
    private SmartUnityEvent<CollisionEventArgs> onCollision = 
        new SmartUnityEvent<CollisionEventArgs>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!includeTriggers)
            return;

        HandleCollision(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!includeCollisions)
            return;

        HandleCollision(collision.collider);
    }

    private void HandleCollision(Collider2D collider)
    {
        if (ColliderIsValid(collider) == false)
            return;

        var eventArgs = new CollisionEventArgs(collider);

        onCollision?.Invoke(this, eventArgs);
    }

    private bool ColliderIsValid(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & layersDetected) == 0)
            return false;

        if (ignoredColliders.Contains(collision))
            return false;

        return true;
    }
}
