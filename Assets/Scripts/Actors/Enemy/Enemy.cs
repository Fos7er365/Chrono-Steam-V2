using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(EnemyAnimations))]
public class Enemy : Actor, IEnemy
{
    HealthController enemyHealthController;
    EnemyAnimations animations;
    [SerializeField] bool isDead;
    Player_Controller _player;
    bool isHurt;
    float timer;
    bool _itemDropped;
    bool isMachinePartSpawn;
    Roulette roulette;
    [SerializeField] GameObject particleTransform;
    [SerializeField]
    protected List<ParticleSystem> particleSystems = new List<ParticleSystem>();
    [SerializeField]
    List<GameObject> bodyParts = new List<GameObject>();
    [SerializeField] GameObject timeMachineGO;
    [SerializeField] GameObject machinePartSpawnPositionGO;
    ObstacleAvoidance obsAvoidance;
    Rigidbody enemyRb;

    public bool IsDead { get => isDead; set => isDead = value; }
    public bool IsHurt { get => isHurt; set => isHurt = value; }
    public Player_Controller Player { get => _player; set => _player = value; }
    public HealthController EnemyHealthController => enemyHealthController;
    public EnemyAnimations Animations => animations;

    public Rigidbody EnemyRb { get => enemyRb; set => enemyRb = value; }
    public ObstacleAvoidance ObsAvoidance { get => obsAvoidance; set => obsAvoidance = value; }

    protected virtual void Awake()
    {
        obsAvoidance = GetComponent<ObstacleAvoidance>();
        //_life_Controller = new Life_Controller(Stats.MaxHealth);
        enemyHealthController = new HealthController(Stats.LifeRange[Random.Range(0, Stats.LifeRange.Count)]);
        animations = GetComponent<EnemyAnimations>();
        enemyRb = GetComponent<Rigidbody>();

        roulette = new Roulette();
        _itemDropped = false;
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
        CheckMachinePartDrop();
        if (IsHurt) DisableHealthLoss();
        else if(isDead) DisableGameObjectCollisionProperties();
    }

    void DisableHealthLoss()
    {
        timer += Time.deltaTime;
        if (timer >= 2f)
        {
            IsHurt = false;
            timer = 0f;
        }
    }

    void DisableGameObjectCollisionProperties()
    {
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        Debug.Log("Boss Collider Deactivated" + gameObject.GetComponent<Rigidbody>().useGravity);
        if (gameObject.TryGetComponent<CapsuleCollider>(out var capsuleColl))
        {
            Debug.Log("Boss Collider Deactivated");
            capsuleColl.isTrigger = true;
        }
        else if (gameObject.TryGetComponent<BoxCollider>(out var boxColl))
        {
            Debug.Log("Boss Collider Deactivated");
            boxColl.isTrigger = true;
        }
    }

    void CheckMachinePartDrop()
    {
        if(Stats.EnemyType == "Boss" && isDead)
        {
            DropMachinePart();
        }
    }

    void DropMachinePart()
    {
        if(GameManager.Instance.MachinePartsPickedUp < 2)
        {
            if (isMachinePartSpawn || gameObject.CompareTag("Final_Boss")) return;
            else
            {
                Instantiate(timeMachineGO, machinePartSpawnPositionGO.transform.position, Quaternion.Euler(90, 0, 0));
                timeMachineGO.GetComponent<Rigidbody>().AddForce(transform.up * 10f, ForceMode.Impulse);
                isMachinePartSpawn = true;

            }
        }
    }

    void OnDamaged()
    {
        if (!EnemyHealthController.isDead)
        {
            IsHurt = true;
            if (gameObject.TryGetComponent<BossAI>(out var bossAI)) animations.DamagedAnimation();
            else animations.DamagedAnimation();

        }

    }
    void HandlePartSystems()
    {
        if(particleSystems.Count > 0)
        {
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
    public void DestroyParticle(GameObject particle)
    {
        if (particle != null) particle.GetComponent<ParticleSystem>().Stop();
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
        if(gameObject.tag == "Final_Boss")
        {
            //animations.
            animations.DeathAnimation();
            //busco el evento de BossDying
            var Event = GameManager.Instance.LvlManager.GetComponent<LevelManager>().BossDying;
            //if (SceneManager.GetActiveScene().buildIndex == 5) SceneManager.LoadScene(7); 
            GameManager.Instance.EventQueue.Add(Event);
            IsDead = true;
            var go = GameObject.Find("Final Level Dialogue Post Boss Fight");
            go.GetComponent<DialogueTrigger>().IsAvailableToShowDialogue = true;
            GameManager.Instance.Win = true;
            Destroy(gameObject, 5f);

        }
        else
        {
            if (TryGetComponent<Rigidbody>(out Rigidbody rb)) rb.useGravity = false;
            if (TryGetComponent<BoxCollider>(out BoxCollider coll)) coll.isTrigger = true;
            //CheckMachinePartDrop();
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
                if (SceneManager.GetActiveScene().buildIndex == 5) SceneManager.LoadScene(7); //Escena de win (Hecho rudimentariamente, hay que hacerlo mejor)
                                                                                              //encolo el evento para su invoke
                GameManager.Instance.EventQueue.Add(Event);
            }
            else
            {
                animations.DeathAnimation();
                if (bodyParts.Count > 0)
                {
                    //TurnOffRegularEnemyFresnel();
                    //Destroy(gameObject);
                }
                if (!_itemDropped)
                {
                    ExecuteRoulette();
                    _itemDropped = true;
                }
            }
            //Debug.Log("Enemey died!");
            IsDead = true;

            //if (gameObject.CompareTag("Final_Boss") && SceneManager.GetActiveScene().buildIndex == 6) SceneManager.LoadScene(7); //Escena de win (Hecho rudimentariamente, hay que hacerlo mejor)
            //if (TryGetComponent<Rigidbody>(out Rigidbody rb)) rb.useGravity = false;
            //if (TryGetComponent<BoxCollider>(out BoxCollider coll)) coll.isTrigger = true;
            Destroy(gameObject, 1f);

        }
    }
    void TurnOffRegularEnemyFresnel()
    {

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
        Debug.Log("Collision? " + other);
        if (other.gameObject.CompareTag("Weapon") || other.gameObject.CompareTag("FloorWeapon"))
        {
            var wp = other.gameObject.GetComponent<Weapon>();
            //if (gameObject.TryGetComponent<BossAI>(out var bossAI))
            //{
                //animations.DamagedAnimation();
                if (wp != null)
                {
                    EnemyHealthController.GetDamage(wp.WeaponStats.AttDamage);
                    Debug.Log("Boss currentHealth" + enemyHealthController.CurrentLife);
                }
            //}
            //else
            //{
            //    //animations.DamagedAnimation();
            //    EnemyHealthController.GetDamage(other.gameObject.GetComponent<Weapon>().WeaponStats.AttDamage);
            //    Debug.Log("Boss currentHealth" + enemyHealthController.CurrentLife);
            //}
        }
    }
}

public interface IEnemy
{

}
