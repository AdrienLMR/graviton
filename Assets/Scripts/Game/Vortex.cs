using DG.Tweening;
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
    [SerializeField] private float ratio = 0.25f;
    [SerializeField] public Animator animator;
    

    [Header("Shake")]
    [SerializeField] private float shakeStrength = 0.15f;
    [SerializeField] private int shakeVibrato = 10;

    private bool stopShake = false;

    private Tween tweenDivideShake = default;

    private float elapsedTime = default;
    private float elapsedTimeShake = default;

    private int shakeLevel = 0;

    public bool doAnimDivision = false;
    public bool doAnimSpawn = false;
    public int factorReduce = 1;

    protected void Start()
    {
        arena = GameObject.Find("Arena").transform;
        animator = GetComponentInChildren<Animator>();

        if (charge == 0)
        {
            //lancement du son d'explosion
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/sound_sfx_vortex_explosion", GetComponent<Transform>().position);
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            return;
        }

        if (charge > 0)
        {
            animator.SetBool("VortexPlus", true);

            if (charge > 1)
            {
                animator.SetBool("VortexPlus", false);
                animator.SetBool("VortexPlus2",true);
            }
        }
        else if (charge < 0)
        {
            animator.SetBool("VortexMoins1",true);

            if (charge < -1)
            {
                animator.SetBool("VortexMoins1", false);
                animator.SetBool("VortexMoins2",true);
            }
        }

        float absoluteCharge = Mathf.Abs(charge);
        transform.localScale = Vector3.one * absoluteCharge;

        if (doAnimDivision && !doAnimSpawn)
        {
            Vector3 lastScale = transform.localScale;
            transform.localScale = Vector3.zero;

            transform.DOScale(lastScale, 0.55f);

            Debug.Log("Lourd");
        }else if (!doAnimDivision && doAnimSpawn)
        {
            Vector3 lastScale = transform.localScale;
            Vector3 newScale = transform.localScale + Vector3.one * factorReduce;

            transform.localScale = newScale;

            transform.DOScale(lastScale, 0.55f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject _object = collision.gameObject;

        if (_object.CompareTag(tagCollision))
        {
            Vortex vortexReceiver = _object.GetComponent<Vortex>();
            OncollisionVortex?.Invoke(this, vortexReceiver);
        }
        else if (_object.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().SetModeDie(transform.position);
        }
    }


    //PAS A SUPPRIMER
    protected override void DoActionMove() { }

    public override void SetModePushed(Vector3 velocity)
    {
        base.SetModePushed(velocity);
        stopShake = true;
        if (tweenDivideShake != null)
            tweenDivideShake.Kill();

    }

    public override void SetModeMove()
    {
        base.SetModeMove();

        stopShake = false;
    }

    protected override void Update()
    {
        base.Update();

        elapsedTime += Time.deltaTime;
        elapsedTimeShake += Time.deltaTime;

        int absCharge = Mathf.Abs(charge);
        float delayIncrease = 0.2f;

        if (absCharge > 1 && elapsedTimeShake >= delayIncrease && !stopShake)
        {
            if (tweenDivideShake != null)
                tweenDivideShake.Kill();

            int vibrato = shakeLevel * shakeVibrato;

            shakeLevel++;
            tweenDivideShake = transform.DOShakePosition(1, shakeStrength, vibrato);

            elapsedTimeShake = 0F;
        }

        if (elapsedTime >= timeToDivide && absCharge > numberChargeToDivide)
        {
            elapsedTime = 0f;

            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/sound_sfx_split", GetComponent<Transform>().position);

            for (int i = 0; i < absCharge; i++)
            {
                int numberOfAugmentation = Mathf.FloorToInt(absCharge / 4);

                if (numberOfAugmentation >= 1 && i == 0)
                    radius = radius * numberOfAugmentation * ratio;

                float angle = Mathf.PI * 2 * i / absCharge;

                Vector3 position = transform.position + radius * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));

                Vortex _vortex = Instantiate(gameObject, position, Quaternion.identity, transform.parent).GetComponent<Vortex>();
                _vortex.charge = charge / Mathf.Abs(charge);
                _vortex.doAnimDivision = true;
            }

            Destroy(gameObject);
        }
    }
}
