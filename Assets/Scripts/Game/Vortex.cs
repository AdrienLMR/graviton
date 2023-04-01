using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnCollisionVortex(Vortex sender, Vortex receiver);

public class Vortex : Movement
{
    public static event OnCollisionVortex OncollisionVortex;

    [SerializeField] public int charge = 1;
    [SerializeField] private string tagCollision = "Vortex";
    [SerializeField] private Explosion explosion;
    [SerializeField] private Color colorPositive;
    [SerializeField] private Color colorNegative;
    [SerializeField] private float timeToDivide = 5f;
    [SerializeField] private float numberChargeToDivide = 1f;
    [SerializeField] private float radius = 3f;

    private SpriteRenderer spriteRender = default;
    private float elapsedTime = default;    

    override protected void Start()
    {
        base.Start();

        arena = GameObject.Find("Arena").transform;

        spriteRender = GetComponent<SpriteRenderer>();

        if (charge == 0)
        {
            //lancement du son d'explosion
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/sound_sfx_vortex_explosion");
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

        float absoluteCharge = Mathf.Abs(charge);
        transform.localScale = Vector3.one * absoluteCharge;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject _object = collision.gameObject;

        if (_object.CompareTag(tagCollision))
        {
            Debug.Log(_object.gameObject);

            Vortex vortexReceiver = _object.GetComponent<Vortex>();
            OncollisionVortex?.Invoke(this, vortexReceiver);
        }else if (_object.CompareTag("Player"))
        {
           collision.GetComponent<PlayerMovement>().SetModeDie();
        }
    }

    
    //PAS A SUPPRIMER
    protected override void DoActionMove(){}

    protected override void Update()
    {
        base.Update();

        elapsedTime += Time.deltaTime;
        int absCharge = Mathf.Abs(charge);


        if(elapsedTime >= timeToDivide && absCharge > numberChargeToDivide)
        {
            elapsedTime = 0f;

            for (int i = 0; i < absCharge; i++)
            {
                //if (i)

                float angle = Mathf.PI * 2 * i / absCharge;
                Vector3 position = new Vector3(Mathf.Cos(angle) * radius + transform.position.x, 
                    Mathf.Sin(angle) * radius + transform.position.y);

                Vortex _vortex = Instantiate(gameObject, position, Quaternion.identity,transform.parent).GetComponent<Vortex>();
                _vortex.charge = charge / Mathf.Abs(charge);
            }

            Destroy(gameObject);
        }
    }
}
