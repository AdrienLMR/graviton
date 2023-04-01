using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerPush : MonoBehaviour
{
    [SerializeField] private SpriteRenderer collisionSprite = default;

    [SerializeField] private Color unTriggeredColor = default;
    [SerializeField] private Color triggeredColor = default;

    [SerializeField] private float pushForce = 0f;

    private List<Collider2D> collisions = new List<Collider2D>();

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
        Vector2 velocity;

        for (int i = 0; i < collisions.Count; i++)
        {
            velocity = (collisions[i].transform.position - transform.parent.position) * pushForce;

            collisions[i].gameObject.GetComponent<Movement>().SetModePushed(velocity);
        }
    }
}
