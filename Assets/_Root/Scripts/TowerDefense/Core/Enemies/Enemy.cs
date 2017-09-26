namespace Kpi.Intro2GameDev.TowerDefense.Core.Enemies
{
    using System;
    using Assets._Root.Scripts.TowerDefense.Strange.Signals;
    using JetBrains.Annotations;

    using strange.extensions.mediation.impl;

    using Stage;
    using UnityEngine;
    using UnityEngine.AI;

    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    public class Enemy : View
    {
        [Inject]
        public EnemyDied EnemyDied { get; set; }

        [SerializeField, EditorAssigned]
        private int coinsReward = 10;

        private NavMeshAgent agent;

        private Animator animator;

        private int health = 30;

        public int CoinsReward => coinsReward;

        public event Action<Enemy> Died = delegate { };

        [UsedImplicitly]
        protected override void Start()
        {
            base.Start();
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            agent.destination = FindObjectOfType<Home>().RandomGoal;
        }

        public void HitWith(int damage)
        {
            if (health == 0)
            {
                //already dead;
                return;
            }
            health = Mathf.Max(0, health - damage);
            if (health == 0)
            {
                animator.SetTrigger("Die");
                Died(this);

                EnemyDied.Dispatch(this);

                Destroy(gameObject.GetComponent<Collider>());
                agent.isStopped = true;
            }
        }
    }
}