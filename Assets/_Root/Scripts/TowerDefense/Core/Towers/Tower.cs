namespace Kpi.Intro2GameDev.TowerDefense.Core.Towers
{
    using System.Collections.Generic;
    using System.Linq;
    using Enemies;
    using JetBrains.Annotations;
    using UnityEngine;

    [ExecuteInEditMode]
    [RequireComponent(typeof(SphereCollider))]
    public class Tower : MonoBehaviour
    {
        [EditorAssigned, SerializeField]
        private int price;           

        [SerializeField, EditorAssigned, Range(2, 40)]
        private float radius = 30;

        private new SphereCollider collider;

        private readonly List<Enemy> enemies = new List<Enemy>();

        public int Price => price;

        private Shooter shooter;

        private Enemy currentGoal;

        [UsedImplicitly]
        public void Start()
        {
            collider = GetComponent<SphereCollider>();
            shooter = GetComponentInChildren<Shooter>();
        }

        [UsedImplicitly]
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position + Vector3.up * 0.5f, radius);
            
        }

#if UNITY_EDITOR
        [UsedImplicitly]
        public void Update()
        {
            if (collider.radius.Equals(radius))
            {
                return;
            }

            collider.radius = radius;
        }
#endif

        private void TryChooseNextGoal()
        {
            if (!enemies.Any())
            {
                return;
            }

            shooter.Reset();
            currentGoal = enemies[Random.Range(0, enemies.Count)];
            shooter.ShootAt(currentGoal);
        }

        [UsedImplicitly]
        private void OnTriggerEnter(Collider other)
        {
            var enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy == null || enemies.Contains(enemy))
            {
                return;
            }

            enemies.Add(enemy);
            enemy.Died += RemoveEnemy;

            if (currentGoal == null)
            {
                TryChooseNextGoal();
            }
        }

        [UsedImplicitly]
        private void OnTriggerExit(Collider other)
        {
            var enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy == null)
            {
                return;
            }

            RemoveEnemy(enemy);
        }

        private void RemoveEnemy(Enemy enemy)
        {
            enemies.Remove(enemy);
            enemy.Died -= RemoveEnemy;

            if (currentGoal == enemy)
            {
                currentGoal = null;
                TryChooseNextGoal();
            }
        }



    }
}