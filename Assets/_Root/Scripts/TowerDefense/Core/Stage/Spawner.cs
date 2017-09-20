namespace Kpi.Intro2GameDev.TowerDefense.Core.Stage
{
    using System.Collections;
    using Enemies;
    using JetBrains.Annotations;
    using UnityEngine;
    public class Spawner : MonoBehaviour
    {
        [SerializeField, EditorAssigned]
        private Enemy prefab;

        [UsedImplicitly]
        public void Start()
        {
            StartCoroutine(SpawnSequence());
        }

        private void Spawn()
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
        }

        private IEnumerator SpawnSequence()
        {
            while (true)
            {
                Spawn();
                yield return new WaitForSeconds(Random.value * 10 + 4);
            }
        }
    }
}