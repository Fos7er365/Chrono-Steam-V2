using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestHandler : MonoBehaviour
{
    [SerializeField] GameObject regularBlessing;
    [SerializeField] GameObject goodBlessing;
    [SerializeField] GameObject ultraBlessing;
    Roulette _regularAttacksRouletteWheel;
    Dictionary<ActionNode, int> _regularAttacksRouletteWheelNodes = new Dictionary<ActionNode, int>();

    private void Start()
    {
        RegularAttacksRouletteWheelHandler();
    }

    void RegularAttacksRouletteWheelHandler()
    {
        _regularAttacksRouletteWheel = new Roulette();

        ActionNode attack1 = new ActionNode(SpawnRegularBlessing);
        ActionNode attack2 = new ActionNode(SpawnGoodBlessing);
        ActionNode attack3 = new ActionNode(SpawnUltraBlessing);

        _regularAttacksRouletteWheelNodes.Add(attack1, 20);
        _regularAttacksRouletteWheelNodes.Add(attack2, 40);
        _regularAttacksRouletteWheelNodes.Add(attack3, 15);

        ActionNode rouletteAction = new ActionNode(RegularAttacksRouletteAction);

    }

    void SpawnRegularBlessing()
    {

    }

    void SpawnGoodBlessing()
    {

    }

    void SpawnUltraBlessing()
    {

    }

    public void RegularAttacksRouletteAction()
    {
        ActionNode node = _regularAttacksRouletteWheel.Run(_regularAttacksRouletteWheelNodes);
        node.Execute();
    }

}
