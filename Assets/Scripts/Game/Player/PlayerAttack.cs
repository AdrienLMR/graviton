using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerAttack : MonoBehaviour
{
    private const string PlayerTag = "Player";
    private const string VortexTag = "Vortex";

    [SerializeField] private SpriteRenderer collisionSprite = default;

    [SerializeField] private float pushForce = 0f;

    [SerializeField] private Animator animatorPush = default;
    [SerializeField] private GameObject vortex = default;

    [SerializeField] private float distanceToSpawn = 4f;
    [SerializeField] private float coolDown = 0f;
    [SerializeField] private Animator animator;

    private float elapsedTime = default;
    public float inputAddVortex { private get; set; }

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

    public void AddVortex1()
    {
        if (collisions.Count > 0)
        {
            Push();
            return;
        }

        FMODUnity.RuntimeManager.PlayOneShot("event:/CHA/sound_cha_vortex_positive", GetComponent<Transform>().position);

        elapsedTime = 0f;

        Vortex _vortex = CreateVortex();
        _vortex.charge = 1;
        //_vortex.SetModePushed(transform.up * forcePushVortex);

        animator.SetBool("AttackRight", true);
    }

    public void AddVortex2()
    {
        if (collisions.Count > 0)
        {
            Push();
            return;
        }

        FMODUnity.RuntimeManager.PlayOneShot("event:/CHA/sound_cha_vortex_negative", GetComponent<Transform>().position);

        elapsedTime = 0f;

        Vortex _vortex = CreateVortex();
        _vortex.charge = -1;
        //_vortex.SetModePushed(transform.up * forcePushVortex);

        animator.SetBool("AttackLeft", true);
    }

    private void Push()
    {
        animatorPush.SetTrigger("isPush");

        Vector2 velocity;
        FMODUnity.RuntimeManager.PlayOneShot("event:/CHA/sound_cha_punch_a1", GetComponent<Transform>().position);
        for (int i = 0; i < collisions.Count; i++)
        {
            velocity = (collisions[i].transform.position - transform.parent.position).normalized * pushForce;

            collisions[i].gameObject.GetComponent<Movement>().SetModePushed(velocity);
        }
    }

    private Vortex CreateVortex()
    {
        Vector3 positionSpawn = transform.parent.position + transform.parent.up * distanceToSpawn;

        return Instantiate(vortex, positionSpawn, Quaternion.identity, transform.parent.parent).GetComponent<Vortex>();
    }
}
