namespace Kpi.Intro2GameDev.TowerDefense.Strange.Context
{
    using Assets.TowerDefense.Strange.Commands;
    using strange.extensions.context.api;
    using strange.extensions.context.impl;
    using strange.extensions.signal.impl;

    public class GameContext : MVCSContext
    {
        private readonly GameRoot root;

        public GameContext(GameRoot view) : base(view, ContextStartupFlags.MANUAL_MAPPING)
        {
            root = view; //
        }

        public override void Launch()
        {
            base.Launch();
            var startSignal = injectionBinder.GetInstance<GameLoaded>();
            startSignal.Dispatch();
        }

        protected override void mapBindings()
        {
            string[] namespaces = { "" };
            implicitBinder.ScanForAnnotatedClasses(namespaces);

            commandBinder.Bind<GameLoaded>().To<StartGameCommand>();
        }
    }

    [Implements]
    public class GameLoaded : Signal { }
}