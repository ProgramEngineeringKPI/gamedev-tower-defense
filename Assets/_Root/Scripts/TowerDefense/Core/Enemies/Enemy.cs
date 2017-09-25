namespace Kpi.Intro2GameDev.TowerDefense.Core.Enemies
{
    using System;
    using JetBrains.Annotations;

    using strange.extensions.mediation.impl;

    using Stage;
    using UnityEngine;
    using UnityEngine.AI;

    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    public class Enemy : View
    {
        private NavMeshAgent agent;

        private Animator animator;

        private int health = 30;

        public event Action<Enemy> Died = delegate { };

        [UsedImplicitly]
        protected override void Start()
        {
            base.Start();
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            agent.destination = FindObjectOfType<Home>().transform.position;
        }

        public void HitWith(int damage)
        {
            health = Mathf.Max(0, health - damage);
            if (health == 0)
            {
                animator.SetTrigger("Die");
                Died(this);
                Destroy(gameObject.GetComponent<Collider>());
                agent.isStopped = true;
            }
        }
    }
}