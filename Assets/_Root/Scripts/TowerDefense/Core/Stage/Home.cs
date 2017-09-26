namespace Kpi.Intro2GameDev.TowerDefense.Core.Stage
{
    using Assets._Root.Scripts.TowerDefense.Strange.Signals;
    using Enemies;
    using JetBrains.Annotations;
    using strange.extensions.mediation.impl;
    using UnityEngine;
    public class Home : View
    {
        [Inject]
        public EnemyPassedBy EnemyPassedBy { get; set; }

        public Vector3 RandomGoal => transform.position + Vector3.right * Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);

        [UsedImplicitly]
        private void OnTriggerEnter(Collider other)
        {
            var zombie = other.gameObject.GetComponent<Enemy>();
            if (zombie != null)
            {
                EnemyPassedBy.Dispatch(zombie);
                Destroy(other.gameObject);
            }
        }


    }
}