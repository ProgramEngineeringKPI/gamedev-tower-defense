namespace Kpi.Intro2GameDev.Assets.TowerDefense.Strange.Commands
{
    using Core;
    using strange.extensions.command.impl;

    public class StartGameCommand : Command
    {
        [Inject]
        public Game Game { get; set; }

        public override void Execute()
        {
            Game.Start(new GameParameters()
            {
                InitialCoins = 100,
                Lives = 20
            });
        }
    }
}