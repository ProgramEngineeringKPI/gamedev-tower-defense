namespace Kpi.Intro2GameDev.Assets.TowerDefense.Core
{
    using System;
    using Intro2GameDev.TowerDefense.Core.Enemies;
    using UnityEngine;
    using _Root.Scripts.TowerDefense.Strange.Signals;

    [Implements]
    public class Game
    {
        [Inject]
        public CoinsUpdated CoinsUpdated { get; set; }

        [Inject]
        public LivesUpdated LivesUpdated { get; set; }

        [Inject]
        public EnemyDied EnemyDied { get; set; }

        [Inject]
        public EnemyPassedBy EnemyPassedBy { get; set; }

        [Inject]
        public PlayerLost PlayerLost { get; set; }

        private bool gameEnded;

        public int Coins
        {
            get
            {
                return coins;
            }

            set
            {
                coins = value;
                CoinsUpdated.Dispatch(value);
            }
        }

        private int coins;

        public int Lives
        {
            get
            {
                return lives;
            }

            set
            {
                lives = value;
                LivesUpdated.Dispatch(value);
            }
        }

        private int lives;

        public void Start(GameParameters parameters)
        {
            EnemyDied.AddListener(OnEnemyDied);
            EnemyPassedBy.AddListener(OnEnemyPassedBy);

            Coins = parameters.InitialCoins;
            Lives = parameters.Lives;
        }

        private void OnEnemyPassedBy(Enemy enemy)
        {
            Lives = Math.Max(0, Lives - 1);
            if (Lives == 0 && !gameEnded)
            {
                gameEnded = true;
                PlayerLost.Dispatch();
            }
        }

        private void OnEnemyDied(Enemy enemy)
        {
            Coins += enemy.CoinsReward;
        }
    }
}