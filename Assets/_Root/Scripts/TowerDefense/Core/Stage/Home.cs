namespace Kpi.Intro2GameDev.TowerDefense.Core.Stage
{
    using Enemies;
    using JetBrains.Annotations;
    using UnityEngine;
    public class Home : MonoBehaviour
    {
        public Vector3 RandomGoal => transform.position + Vector3.right * Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);

        [UsedImplicitly]
        private void OnTriggerEnter(Collider other)
        {
            var zombie = other.gameObject.GetComponent<Enemy>();
            if (zombie != null)
            {
                Destroy(other.gameObject);
            }
        }


    }
}