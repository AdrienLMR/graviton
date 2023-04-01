using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private string tagColision = "Player";
    [SerializeField] private float speed = 2f;
    [SerializeField] private float stayOnStage = 0.2f;

    private float elapsedTime = default;

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= stayOnStage)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject objectCollide = collision.gameObject;
        
        if (objectCollide.CompareTag(tagColision))
        {
            Vector3 velocity = objectCollide.transform.position - transform.position;
            objectCollide.GetComponent<PlayerMovement>().SetModePushed(velocity.normalized * speed * Time.deltaTime);
            Destroy(gameObject);
        }
    }
}
