namespace Kpi.Intro2GameDev.Assets.TowerDefense.UI
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using DG.Tweening;
    using Intro2GameDev.TowerDefense.Core.Enemies;
    using JetBrains.Annotations;
    using strange.extensions.mediation.impl;
    using Shared.Ui;
    using UnityEngine;
    using UnityEngine.UI;
    using _Root.Scripts.TowerDefense.Strange.Signals;

    public class UiController : View
    {
        [Inject]
        public PlayerLost PlayerLost { get; set; }

        [Inject]
        public EnemyBorn EnemyBorn { get; set; }

        [SerializeField, EditorAssigned]
        private GameObject LoseWindow;

        [SerializeField, EditorAssigned]
        private CanvasGroup back;

        [SerializeField, EditorAssigned]
        private Transform popUpParent;

        private IDictionary<IFolowee, GameObject> popUps = new Dictionary<IFolowee, GameObject>();

        protected override void Start()
        {
            base.Start();
            PlayerLost.AddListener(OnPlayerLost);
            EnemyBorn.AddListener(OnEnemyBorn);
        }

        private void OnEnemyBorn(Enemy enemy)
        {
            var popUp = Instantiate(enemy.PopUpPrefab, popUpParent, false);
            popUp.Init(enemy);
            popUps[enemy] = popUp.gameObject;
        }

        private void OnPlayerLost()
        {
            back.gameObject.SetActive(true);
            back.alpha = 0;
            back.DOFade(1f, 1.2f);

            DOVirtual.DelayedCall(0.6f, () =>
            {
                LoseWindow.SetActive(true);
                LoseWindow.transform.DOScale(Vector3.zero, 0.5f).From().SetEase(Ease.OutBack, 1.5f);
            });
        }
    }
}