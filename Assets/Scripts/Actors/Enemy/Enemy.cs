using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimations))]
public class Enemy : Actor
{
    HealthController enemyHealthController;
    EnemyAnimations animations;
    [SerializeField] bool isDead;
    Player_Controller _player;
    bool isHurt;
    float timer;
    bool _itemDroped;
    Roulette roulette;
    [SerializeField] GameObject particleTransform;
    [SerializeField]
    protected List<ParticleSystem> particleSystems = new List<ParticleSystem>();
    [SerializeField]
    List<GameObject> bodyParts = new List<GameObject>();

    public bool IsDead { get => isDead; set => isDead = value; }
    public bool IsHurt { get => isHurt; set => isHurt = value; }
    public Player_Controller Player { get => _player; set => _player = value; }
    public HealthController EnemyHealthController => enemyHealthController;
    public EnemyAnimations Animations => animations;
    protected virtual void Awake()
    {
        //_life_Controller = new Life_Controller(Stats.MaxHealth);
        enemyHealthController = new HealthController(Stats.LifeRange[Random.Range(0, Stats.LifeRange.Count)]);
        animations = GetComponent<EnemyAnimations>();

        roulette = new Roulette();
        _itemDroped = false;
    }
    protected virtual void Start()
    {
        //animator = GetComponent<Animator>();
        enemyHealthController.isDead = false;
        _player = GameManager.Instance.PlayerInstance.GetComponent<Player_Controller>();
        enemyHealthController.Dead.AddListener(Die);
        enemyHealthController.Damaged.AddListener(OnDamaged);
    }

    protected virtual void Update()
    {
        if (IsHurt)
        {
            timer += Time.deltaTime;
            if (timer >= 2f)
            {
                IsHurt = false;
                timer = 0f;
            }
        }
    }

    void OnDamaged()
    {
        if (!EnemyHealthController.isDead)
        {
            IsHurt = true;
            if (gameObject.TryGetComponent<BossAI>(out var bossAI))
            {
                animations.DamagedAnimation();
            }
            else
            {
                animations.DamagedAnimation();
            }

            for (int i = 0; i < particleSystems.Count; i++)
            {
                particleSystems[i].Play();
            }
        }

    }
    public void PlayParticle(GameObject particle)
    {
        if (particle != null)
        {
            Instantiate(particle, transform.position, transform.rotation);
            particle.GetComponent<ParticleSystem>().Play();
        }
        else
            Debug.Log("playParticles function particle not asigned");
    }

    public void PlayParticleInSpecificPosition(GameObject particle)
    {
        if (particle != null)
        {
            Instantiate(particle, particleTransform.transform.position, particleTransform.transform.rotation);
            particle.GetComponent<ParticleSystem>().Play();
        }
        else
            Debug.Log("playParticles function particle not asigned");
    }

    void Die()
    {
        if (gameObject.TryGetComponent<BossAI>(out var bossAI))
        {
            foreach (var itemA in bodyParts)
            {
                //Debug.Log("recorriendo parte del cuerpo");
                //...Busco los materiales del skin mesh renderer
                for (int i = 0; i < itemA.GetComponent<SkinnedMeshRenderer>()?.materials.Length; i++)
                {
                    //  Debug.Log("recorriendo materiales");
                    var itemB = itemA.GetComponent<SkinnedMeshRenderer>().materials[i];
                    // si el material es un enemy fresnel...
                    if (itemB.name != "Monster")
                    {
                        //Debug.Log("material correcto");
                        //...seteo fesnel scale para q ya no brille
                        itemA.GetComponent<SkinnedMeshRenderer>().materials[i].SetFloat("_FresnelScale", 0);
                        itemA.GetComponent<SkinnedMeshRenderer>().materials[i].SetFloat("_FresnelSize", 0); 
                    }
                }
            }
            animations.DeathAnimation();

            //busco el evento de BossDying
            var Event = GameManager.Instance.LvlManager.GetComponent<LevelManager>().BossDying;
            //encolo el evento para su invoke
            GameManager.Instance.EventQueue.Add(Event);
        }
        else
        {
            animations.DeathAnimation();
            //encada una de mis partes del cuerpo...
            foreach (var itemA in bodyParts)
            {
                //Debug.Log("recorriendo parte del cuerpo");
                //...Busco los materiales del skin mesh renderer
                for (int i = 0; i < itemA.GetComponent<SkinnedMeshRenderer>().materials.Length; i++)
                {
                    //  Debug.Log("recorriendo materiales");
                    var itemB = itemA.GetComponent<SkinnedMeshRenderer>().materials[i];
                    // si el material es un enemy fresnel...
                    if (itemB.name != "SkeletonOutlaw00")
                    {
                        //Debug.Log("material correcto");
                        //...seteo fesnel scale para q ya no brille
                        itemA.GetComponent<SkinnedMeshRenderer>().materials[i].SetFloat("_FresnelScale", 0);
                    }
                }
            }
            if (!_itemDroped)
            {
                ExecuteRoulette();
                _itemDroped = true;
            }
        }
        //Debug.Log("Enemey died!");
        IsDead = true;
        if (TryGetComponent<Rigidbody>(out Rigidbody rb)) rb.useGravity = false;
        if (TryGetComponent<BoxCollider>(out BoxCollider coll)) coll.isTrigger = true;
        Destroy(gameObject, 15f);

    }
    void ExecuteRoulette()
    {
        Debug.Log("CurrentDrops.count  " + GameManager.Instance.LootManager.CurrentDrops.Count);
        GameObject item = roulette.Run(GameManager.Instance.LootManager.CurrentDrops);
        if (item != null)
        {
            Instantiate(item, new Vector3(transform.position.x, 1, transform.position.z), Quaternion.identity);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            if (gameObject.TryGetComponent<BossAI>(out var bossAI))
            {
                animations.DamagedAnimation();
                EnemyHealthController.GetDamage(other.gameObject.GetComponent<Weapon>().WeaponStats.AttDamage);
            }
            else
            {
                animations.DamagedAnimation();
                EnemyHealthController.GetDamage(other.gameObject.GetComponent<Weapon>().WeaponStats.AttDamage);
            }
        }
    }
}
