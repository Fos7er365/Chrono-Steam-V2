using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField] private ActorStats stats;

    public ActorStats Stats { get => stats; }
}
