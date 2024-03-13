using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    private NavMeshAgent pathfinder;
    private Transform target;

    public GameObject objetivo;
    
    void Start()
    {
        pathfinder = GetComponent<NavMeshAgent>();
        target = objetivo.transform;
    }
    void Update()
    {
        pathfinder.SetDestination(target.position);
    }
}