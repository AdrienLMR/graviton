using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private SpriteRenderer collisionSprite = default;

    [SerializeField] private Color unTriggeredColor = default;
    [SerializeField] private Color triggeredColor = default;

    [SerializeField] private float pushForce = 0f;

    [SerializeField] private List<Collider2D> collisions = default;

    #region Unity Methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collisionSprite.color = triggeredColor;
            collisions.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collisionSprite.color = unTriggeredColor;
        collisions.Remove(collision);
    }
    #endregion

    public void Push()
    {
        for (int i = 0; i < collisions.Count; i++)
        {
            collisions[i].gameObject.GetComponent<Movement>().SetModePushed(transform.parent.forward * pushForce);
        }
    }
}
