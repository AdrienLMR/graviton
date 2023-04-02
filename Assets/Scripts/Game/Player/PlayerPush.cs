using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerPush : MonoBehaviour
{
    private const string PlayerTag = "Player";
    private const string VortexTag = "Vortex";

    [SerializeField] private SpriteRenderer collisionSprite = default;

    [SerializeField] private Color unTriggeredColor = default;
    [SerializeField] private Color triggeredColor = default;

    [SerializeField] private float pushForce = 0f;

    private List<Collider2D> collisions = new List<Collider2D>();

    #region Unity Methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == PlayerTag || collision.tag == VortexTag)
        {
            //collisionSprite.color = triggeredColor;
            collisions.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == PlayerTag || collision.tag == VortexTag)
        {
            //collisionSprite.color = unTriggeredColor;
            collisions.Remove(collision);
        }
    }
    #endregion

    public void Push()
    {
        Vector2 velocity;
        FMODUnity.RuntimeManager.PlayOneShot("event:/CHA/sound_cha_punch_a1");
        for (int i = 0; i < collisions.Count; i++)
        {
            velocity = (collisions[i].transform.position - transform.parent.position).normalized * pushForce;

            collisions[i].gameObject.GetComponent<Movement>().SetModePushed(velocity);
        }
    }
}
