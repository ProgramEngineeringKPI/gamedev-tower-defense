namespace Kpi.Intro2GameDev.Assets._Root.Scripts.TowerDefense.Strange.Signals
{
    using Intro2GameDev.TowerDefense.Core.Enemies;
    using strange.extensions.signal.impl;

    [Implements]
    public class EnemyDied : Signal<Enemy>{}

    [Implements]
    public class EnemyBorn : Signal<Enemy> { }

    [Implements]
    public class EnemyPassedBy : Signal<Enemy> { }

    [Implements]
    public class CoinsUpdated : Signal<int> { }

    [Implements]
    public class LivesUpdated : Signal<int> { }

    [Implements]
    public class PlayerLost : Signal { }

    [Implements]
    public class PlayerWon : Signal { }
}