using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Main.Scripts.FSM_SO_VERSION;

public class BossEnemyController : MonoBehaviour
{
    [SerializeField] StateData fsmInitialState;
    FsmScript bossFSM;
    Enemy enemyModel;

    private void Awake()
    {
        enemyModel = GetComponent<Enemy>();
    }
    // Start is called before the first frame update
    void Start()
    {
        bossFSM = new FsmScript(enemyModel, fsmInitialState);
    }

    // Update is called once per frame
    void Update()
    {
        bossFSM.UpdateState();
    }
}
