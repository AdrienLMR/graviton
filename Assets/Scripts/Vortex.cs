using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnCollisionVortex(Vortex sender, Vortex receiver);

public class Vortex : MonoBehaviour
{
    public static event OnCollisionVortex colisionVortex;

    [SerializeField] public int charge = 1;
    [SerializeField] private string tagCollision = "Vortex";
    [SerializeField] private Explosion explosion;
    [SerializeField] private Color colorPositive;
    [SerializeField] private Color colorNegative;

    private SpriteRenderer spriteRender = default;

    private void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();

        if (charge == 0)
        {
            Debug.Log("Explosition");
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            return;
        }

        if (charge > 0)
        {
            spriteRender.color = colorPositive; 
        }
        else if (charge < 0)
        {
            spriteRender.color = colorNegative;
        }

        transform.localScale = Vector3.one * Mathf.Abs(charge);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject _object = collision.gameObject;

        if (collision.CompareTag(tagCollision))
        {
            Vortex vortexReceiver = _object.GetComponent<Vortex>();
            colisionVortex?.Invoke(this, vortexReceiver);
        }
    }
}
