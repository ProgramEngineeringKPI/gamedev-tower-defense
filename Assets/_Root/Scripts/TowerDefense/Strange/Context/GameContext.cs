namespace Kpi.Intro2GameDev.TowerDefense.Strange.Context
{
    using strange.extensions.context.api;
    using strange.extensions.context.impl;

    public class GameContext : MVCSContext
    {
        private readonly GameRoot root;

        public GameContext(GameRoot view) : base(view, ContextStartupFlags.MANUAL_MAPPING)
        {
            root = view; //
        }

        protected override void mapBindings()
        {
            string[] namespaces = { "" };
            implicitBinder.ScanForAnnotatedClasses(namespaces);
        }
    }
}