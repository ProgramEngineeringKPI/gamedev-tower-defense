namespace Kpi.Intro2GameDev.Assets.TowerDefense.Utils
{
    using JetBrains.Annotations;
    using strange.extensions.mediation.impl;
    using UnityEngine;
    using _Root.Scripts.TowerDefense.Strange.Signals;

    public class HotKeyListener : View
    {
        [Inject]
        public PlayerLost PlayerLost { get; set; }

        [UsedImplicitly]
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerLost.Dispatch();
            }
        }
    }
}