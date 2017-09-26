namespace Kpi.Intro2GameDev.TowerDefense.UI.Indicators
{
    using Assets.TowerDefense.Core;
    using Assets._Root.Scripts.TowerDefense.Strange.Signals;
    using JetBrains.Annotations;
    using strange.extensions.mediation.impl;
    using UnityEngine;
    using UnityEngine.UI;

    public class LivesIndicator : View
    {
        [Inject]
        public LivesUpdated LivesUpdated { get; set; }

        [Inject]
        public Game Game { get; set; }

        [SerializeField, EditorAssigned]
        private Text indcator;

        protected override void Start()
        {
            base.Start();
            LivesUpdated.AddListener(OnLivesUpdated);
            OnLivesUpdated(Game.Lives);
        }

        private void OnLivesUpdated(int value)
        {
            indcator.text = value.ToString();
        }
    }
}