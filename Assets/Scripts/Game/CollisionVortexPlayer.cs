using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionVortexPlayer : MonoBehaviour
{
    [SerializeField] private string _tag = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_tag))
        {
            collision.GetComponent<PlayerMovement>().SetModeDie();
        }
    }
}
