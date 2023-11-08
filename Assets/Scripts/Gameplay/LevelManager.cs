using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    private UnityEvent _winlvl;
    private UnityEvent _winRoom;
    private UnityEvent _bossDying;
    private Enemy _bossInstance;
    TreasureChestsSpawnHandler chestsSpawner;
    Loot_Manager lootMgr;

    [SerializeField]
    private GameObject _elevatorDoor;
    [SerializeField]
    private bool bossDead;
    [SerializeField]
    private int RoomsToClear;
    [SerializeField]
    private int NextLVL;


    public static LevelManager Instance => instance;
    public UnityEvent BossDying => _bossDying;
    public UnityEvent Winlvl => _winlvl;
    public UnityEvent WinRoom => _winRoom;

    public GameObject ElevatorDoor { get => _elevatorDoor; set => _elevatorDoor = value; }
    public Enemy BossInstance { get => _bossInstance; set => _bossInstance = value; }
    public bool BossDead { get => bossDead; set => bossDead = value; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        chestsSpawner = GetComponent<TreasureChestsSpawnHandler>();
        lootMgr = GetComponent<Loot_Manager>();
        instance = this;
    }
    private void Start()
    {
        _winlvl = new UnityEvent();
        _winRoom = new UnityEvent();
        _bossDying = new UnityEvent();
        _bossDying.AddListener(BossDie);
        _winlvl.AddListener(GameManager.Instance.GameWin);
        _winRoom.AddListener(allRoomsClear);
        GameManager.Instance.LvlManager = this.gameObject;
        GameManager.Instance.LvlToCharge = NextLVL;
        lootMgr.WeaponDrops.Clear();
        lootMgr.CurrentDrops.Clear();
    }
    private void Update()
    {
        HandleChestsSpawn();
        HandleElevatorSpawn();
        
       
    }
    void HandleChestsSpawn()
    {
        var dungeonSp = GameObject.FindWithTag("RoomTamplates");
        if (dungeonSp != null && !chestsSpawner.IsSpawned)
        {
            Debug.Log("Puedo spawnear cofres?");
            if (dungeonSp.gameObject.GetComponent<RoomTemplate>().IsDungeonSet)
            {
                Debug.Log("Puedo spawnear cofres en room template");
                chestsSpawner.HandleChestSpawning();
            }
        }
    }

    void HandleElevatorSpawn()
    {
        if (bossDead && _elevatorDoor != null)
        {
           
            _elevatorDoor.SetActive(false);
        }
        //if (SceneManager.GetActiveScene().buildIndex == NextLVL) SceneManager..OnLevelWasLoaded(NextLVL);
        if (ElevatorDoor == null)
        {
            var elevators = GameObject.FindGameObjectsWithTag("ElevatorDoor");
            foreach (var item in elevators)
            {
                if (item.activeSelf)
                {
                    _elevatorDoor = item;
                }
            }
        }
    }

    private void allRoomsClear()
    {
        if (GameManager.Instance.ClearRooms >= RoomsToClear)
        {
            GameManager.Instance.ClearRooms = 0;
            GameManager.Instance.LootManager.AddWeaponToLoot();
            GameManager.Instance.EventQueue.Add(_winlvl);
        }
        else
        {
            GameManager.Instance.ClearRooms++;
            GameManager.Instance.LootManager.AddWeaponToLoot();
            GameManager.Instance.ReloadScene();
        }
    }
    private void BossDie()
    {
        bossDead = true;
    }
}
