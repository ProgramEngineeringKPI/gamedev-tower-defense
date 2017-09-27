namespace Kpi.Intro2GameDev.Assets.TowerDefense.UI
{
    using System;
    using DG.Tweening;
    using JetBrains.Annotations;
    using strange.extensions.mediation.impl;
    using UnityEngine;
    using UnityEngine.UI;
    using _Root.Scripts.TowerDefense.Strange.Signals;

    public class UiController : View
    {
        [Inject]
        public PlayerLost PlayerLost { get; set; }

        [SerializeField, EditorAssigned]
        private GameObject LoseWindow;

        [SerializeField, EditorAssigned]
        private CanvasGroup back;

        protected override void Start()
        {
            base.Start();
            PlayerLost.AddListener(OnPlayerLost);
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