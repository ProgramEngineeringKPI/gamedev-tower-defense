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

        [SerializeField, EditorAssigned, Range(1, 20)]
        private float width;

        [SerializeField, EditorAssigned, Range(1, 30)]
        private float delayScatter = 10;

        [SerializeField, EditorAssigned, Range(0, 30)]
        private float delay = 4;

        private Transform enemiesParent;

        [UsedImplicitly]
        public void Start()
        {
            enemiesParent = GameObject.Find("Enemies").transform;
            StartCoroutine(SpawnSequence());
        }


        [UsedImplicitly]
        private void OnDrawGizmos()
        {
           DrawCube(transform.position, transform.rotation, new Vector3(width, 2, 2));
        }

        private void Spawn()
        {
            Instantiate(prefab, transform.position + Vector3.right * Random.Range(-width/2, width/2), Quaternion.identity, enemiesParent);
        }

        private IEnumerator SpawnSequence()
        {
            while (true)
            {
                Spawn();
                yield return new WaitForSeconds(Random.value * delayScatter + delay);
            }
        }

        public static void DrawCube(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            Matrix4x4 cubeTransform = Matrix4x4.TRS(position, rotation, scale);
            Matrix4x4 oldGizmosMatrix = Gizmos.matrix;

            Gizmos.color = Color.green;
            Gizmos.matrix *= cubeTransform;

            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);

            Gizmos.matrix = oldGizmosMatrix;
        }
    }
}