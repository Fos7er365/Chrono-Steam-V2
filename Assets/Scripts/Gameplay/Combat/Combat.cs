using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour, IEnemyAtack
{
    //[SerializeField] private float attackRange = 3f;
    //[SerializeField] float seekRange = 5f;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask enemyLayers;
    protected Collider[] hitEnemies;
    Enemy enemyModel;

    //public float AttackRange { get => attackRange; set => attackRange = value; }
    //public float SeekRange { get => seekRange; set => seekRange = value; }

    Roulette _regularAttacksRouletteWheel;
    Dictionary<ActionNode, int> _regularAttacksRouletteWheelNodes = new Dictionary<ActionNode, int>();

    private void Awake()
    {
        enemyModel = GetComponent<Enemy>();
    }

    private void Start()
    {
        RegularAttacksRouletteWheelHandler();
    }

    void RegularAttacksRouletteWheelHandler()
    {
        _regularAttacksRouletteWheel = new Roulette();

        ActionNode attack1 = new ActionNode(enemyModel.Animations.AttackAnimation);
        ActionNode attack2 = new ActionNode(enemyModel.Animations.Attack2Animation);
        ActionNode attack3 = new ActionNode(enemyModel.Animations.Attack3Animation);

        _regularAttacksRouletteWheelNodes.Add(attack1, 20);
        _regularAttacksRouletteWheelNodes.Add(attack2, 40);
        _regularAttacksRouletteWheelNodes.Add(attack3, 15);

        ActionNode rouletteAction = new ActionNode(RegularAttacksRouletteAction);

    }

    public void RegularAttacksRouletteAction()
    {
        ActionNode node = _regularAttacksRouletteWheel.Run(_regularAttacksRouletteWheelNodes);
        node.Execute();
    }

    public virtual void DoDamage()
    {
        // Detect enemies in range of attack
        hitEnemies = Physics.OverlapSphere(attackPoint.position, enemyModel.Stats.AttackRange, enemyLayers);
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(attackPoint.position, enemyModel.Stats.AttackRange);
    }
}
