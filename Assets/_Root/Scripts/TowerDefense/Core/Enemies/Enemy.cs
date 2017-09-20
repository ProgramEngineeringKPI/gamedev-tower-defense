namespace Kpi.Intro2GameDev.TowerDefense.Core.Enemies
{
    using JetBrains.Annotations;
    using Stage;
    using UnityEngine;
    using UnityEngine.AI;

    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {
        private NavMeshAgent agent;

        [UsedImplicitly]
        public void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.destination = FindObjectOfType<Home>().transform.position;
        }
    }
}