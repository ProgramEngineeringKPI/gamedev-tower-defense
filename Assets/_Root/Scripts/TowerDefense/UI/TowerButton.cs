namespace Kpi.Intro2GameDev.TowerDefense.UI
{
    using Assets.TowerDefense.Core;
    using Assets.TowerDefense.Utils;

    using Intro2GameDev.TowerDefense.Core.Towers;

    using JetBrains.Annotations;

    using strange.extensions.mediation.impl;

    using UnityEngine;

    public class TowerButton : View
    {
        [Inject]
        public Game Game { get; set; }

        [EditorAssigned, SerializeField]
        private Tower prefab;

        [EditorAssigned, SerializeField]
        private ItemMover moverPrefab;           

        [UsedImplicitly]
        public void OnTowerClicked()
        {
            if (!Game.TrySpendCoins(prefab.Price))
            {
                return;
            }

            var mover = Instantiate(moverPrefab);
            mover.SetObject(Instantiate(prefab).gameObject);
        }
    }
}