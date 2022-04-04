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
    private List<Collider2D> ignoredColliders = new();

    [SerializeField]
    private UnityEvent<CollisionEventArgs> onCollision = new();

    [SerializeField]
    private UnityEvent<CollisionEventArgs> onExit = new();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!includeTriggers)
            return;

        HandleCollision(collision, onCollision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!includeCollisions)
            return;

        HandleCollision(collision.collider, onCollision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!includeTriggers)
            return;

        HandleCollision(collision, onExit);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!includeCollisions)
            return;

        HandleCollision(collision.collider, onExit);
    }

    private void HandleCollision(Collider2D collider, 
        UnityEvent<CollisionEventArgs> trigger)
    {
        if (ColliderIsValid(collider) == false)
            return;

        var eventArgs = new CollisionEventArgs(collider);

        trigger?.Invoke(eventArgs);
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
