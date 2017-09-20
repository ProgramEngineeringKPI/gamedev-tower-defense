namespace Kpi.Intro2GameDev.TowerDefense.Core.Towers
{
    using System.Collections;
    using Enemies;
    using JetBrains.Annotations;
    using UnityEngine;

    public abstract class Shooter : MonoBehaviour
    {
        [SerializeField, EditorAssigned, Range(0.1f, 3)]
        private float delayBetweenShots = 0.5f;

        [SerializeField, EditorAssigned, Range(1, 20)]
        private int amount = 6;

        [SerializeField, EditorAssigned, Range(0, 10)]
        private float rechargeTime = 3;

        protected Enemy Enemy;

        public void ShootAt(Enemy enemy)
        {
            Enemy = enemy;
        }

        [UsedImplicitly]
        public void Start()
        {
            StartCoroutine(ShotSequence());
        }

        public void Reset()
        {
            Enemy = null;
        }

        [UsedImplicitly]
        public void Update()
        {
            if (Enemy == null)
            {
                return;
            }

            transform.rotation = Quaternion.LookRotation(Enemy.transform.position - 
                new Vector3(transform.position.x, Enemy.transform.position.y, transform.position.z));
        }

        private IEnumerator ShotSequence()
        {
            while (true)
            {
                yield return new WaitUntil(() => Enemy != null);
                while (Enemy != null)
                {
                    for (var shot = 0; shot < amount; shot++)
                    {
                        Shot();
                        yield return new WaitForSeconds(delayBetweenShots);
                    }

                    yield return new WaitForSeconds(rechargeTime);
                }
            }
            
        }

        protected abstract void Shot();
    }
}