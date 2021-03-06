﻿namespace Kpi.Intro2GameDev.TowerDefense.UI.Indicators
{
    using Assets.TowerDefense.Core;
    using Assets._Root.Scripts.TowerDefense.Strange.Signals;
    using JetBrains.Annotations;
    using strange.extensions.mediation.impl;
    using UnityEngine;
    using UnityEngine.UI;

    public class CoinsIndicator : View
    {
        [Inject]
        public CoinsUpdated CoinsUpdated { get; set; }

        [Inject]
        public Game Game { get; set; }

        [SerializeField, EditorAssigned]
        private Text indcator;

        protected override void Start()
        {
            base.Start();
            CoinsUpdated.AddListener(OnCoinsUpdated);
            OnCoinsUpdated(Game.Coins);
        }

        private void OnCoinsUpdated(int value)
        {
            indcator.text = value.ToString();
        }
    }
}