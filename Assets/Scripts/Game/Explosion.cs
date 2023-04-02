using UnityEngine;

public delegate void ExplosionEventHandler(Explosion sender);

public class Explosion : MonoBehaviour
{
    [SerializeField] private string tagColision = "Player";
    [SerializeField] private float speed = 2f;
    [SerializeField] private float stayOnStage = 0.2f;

    private float elapsedTime = default;

    public static event ExplosionEventHandler OnExplode;

    private void Start()
    {
        OnExplode?.Invoke(this);
    }

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
            //Destroy(gameObject);
        }
    }
}
