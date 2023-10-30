using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    List<UnityEvent> eventQueue;
    GameObject _playerInstance;
    GameObject _camera;
    GameObject lvlManager;
    Loot_Manager _lootManager;
    int _lvlToCharge;
    int _clearRooms;
    [SerializeField] float respawnCD;
    [SerializeField] TextMeshProUGUI powerUpText;
    Transform playerSpawner;
    bool _gameOver;
    bool _win;

    public static GameManager Instance => instance;
    public List<UnityEvent> EventQueue => eventQueue;
    public bool GameOver1 => _gameOver;

    public GameObject LvlManager { get => lvlManager; set => lvlManager = value; }
    public GameObject PlayerInstance { get => _playerInstance; set => _playerInstance = value; }
    public GameObject Camera { get => _camera; set => _camera = value; }
    public bool Win { get => _win; set => _win = value; }
    public int LvlToCharge { get => _lvlToCharge; set => _lvlToCharge = value; }
    public int ClearRooms { get => _clearRooms; set => _clearRooms = value; }
    public Loot_Manager LootManager { get => _lootManager; set => _lootManager = value; }
    public TextMeshProUGUI PowerUpText { get => powerUpText; set => powerUpText = value; }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        //singleton
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        eventQueue = new List<UnityEvent>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (powerUpText.text != "")
        {
            StartCoroutine(WaitToDisableUI(2));
        }
        EventQueueHandler();
        //if (_gameOver)
        //{

        //}
    }
    void EventQueueHandler()
    {
        if (eventQueue.Count > 0)
        {
            //Debug.Log("evenQueue.Count mayor a 0");
            for (int i = EventQueue.Count - 1; i >= 0; i--)
            {
                //Debug.Log("evenQueue ejecutando for");
                if (EventQueue[i] != null)
                {
                    EventQueue[i]?.Invoke();
                    EventQueue.Remove(EventQueue[i]);
                }

            }
        }
        else return;

    }
    IEnumerator WaitToDisableUI(float secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait);
        Debug.Log("Disable UI");
        GameManager.Instance.PowerUpText.text = "";
    }
    void OnLevelWasLoaded(int level)
    {
        _playerInstance.GetComponent<Player_Controller>().Isleaving = false;
        if (playerSpawner == null)
        {
            playerSpawner = GameObject.FindGameObjectWithTag("PlayerSpawner").transform;
        }
        _camera.GetComponent<CameraFollow>().enabled = true;
        _playerInstance.transform.position = playerSpawner.position;
    }
    public void GameWin()
    {
        SceneManager.LoadScene(_lvlToCharge);
    }
    public void GameOver()
    {
        Debug.Log("YOU DIE...");
        _gameOver = true;
        if (SceneManager.GetActiveScene().name != "Tutorial LvL 1")
        {
            Invoke("reloadScene", respawnCD);
        }
        else
        {
            if (playerSpawner == null)
            {
                playerSpawner = GameObject.FindGameObjectWithTag("PlayerSpawner").transform;
            }
            _camera.GetComponent<CameraFollow>().enabled = true;
            _playerInstance.transform.position = playerSpawner.position;
            PlayerInstance.GetComponent<Player_Controller>().Animations.Revive();
            PlayerInstance.GetComponent<Player_Controller>().Life_Controller.GetHeal(float.MaxValue);
            PlayerInstance.GetComponent<Player_Controller>().Life_Controller.isDead = false;
            return;
        }
    }
    public void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerInstance.GetComponent<Player_Controller>().Animations.Revive();
        PlayerInstance.GetComponent<Player_Controller>().Life_Controller.isDead = false;
        if (SceneManager.GetActiveScene().name == "Tutorial LvL 1")
            PlayerInstance.GetComponent<Player_Controller>().Life_Controller.GetHeal(float.MaxValue);
    }
}
