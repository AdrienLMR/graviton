using UnityEngine;

[DisallowMultipleComponent]
public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private SpriteRenderer collisionSprite = default;

    [SerializeField] private Color unTriggeredColor = default;
    [SerializeField] private Color triggeredColor = default;

    [SerializeField] private float pushForce = 0f;

    #region Unity Methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collisionSprite.color = triggeredColor;

            //if (button attack)
            //collision.gameObject.GetComponent<Movement>().SetModePushed(transform.parent.forward * pushForce);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collisionSprite.color = unTriggeredColor;
    }
    #endregion
}
