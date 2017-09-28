namespace Kpi.Intro2GameDev.Assets.TowerDefense.UI.Game
{
    using System;

    using Intro2GameDev.TowerDefense.Core.Enemies;

    using JetBrains.Annotations;

    using Shared.Ui;

    using UnityEngine;
    using UnityEngine.UI;

    using _Root.Scripts.TowerDefense.Strange.Signals;

    public class EnemyHealthPopUp : FollowingPopUp<Enemy>
    {
        [Inject]
        public EnemyPassedBy EnemyPassedBy { get; set; }

        [EditorAssigned, SerializeField]
        private Image fill;

        private int maxHealth;

        protected override void InitInternal()
        {
            Folowee.HealthUpdated += OnHealthUpdated;
            maxHealth = Folowee.Health;
            OnHealthUpdated(Folowee);
        }

        protected override void Start()
        {
            base.Start();
            EnemyPassedBy.AddListener(OnEnemyPassedBy);
        }

        private void OnEnemyPassedBy(Enemy enemy)
        {
            if (enemy == Folowee)
            {
                Dispose();
            }
        }

        private void OnHealthUpdated(Enemy enemy)
        {
            fill.fillAmount = (float)enemy.Health / maxHealth;
            if (enemy.Health == maxHealth && gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            } 
            else if (enemy.Health != maxHealth && !gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }

            if (enemy.Health == 0)
            {
                Dispose();
            }
        }

        private void Dispose()
        {
            Folowee.HealthUpdated -= OnHealthUpdated;
            EnemyPassedBy?.RemoveListener(OnEnemyPassedBy);
            Destroy(gameObject);
        }
    }
}