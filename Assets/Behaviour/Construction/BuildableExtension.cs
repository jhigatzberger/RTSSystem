namespace JHiga.RTSEngine.Construction
{
    public class BuildableExtension : BaseInteractableExtension<BuildableProperties>, IBuildable
    {
        public BuildableExtension(IExtendableEntity entity, BuildableProperties properties) : base(entity, properties)
        {
        }
        public int MaxConstructionLevel => Properties.maxConstructionLevel;
        public override void Enable()
        {
            CurrentConstructionLevel = 0;
        }
        private int _currentConstructionLevel;
        public int CurrentConstructionLevel
        {
            get => _currentConstructionLevel;
            set
            {
                if (value > MaxConstructionLevel)
                    value = MaxConstructionLevel;
                _currentConstructionLevel = value;
                if(value==MaxConstructionLevel)
                    Entity.Disable(true);
            }
        }
        public GameEntityPool FinishiedEntity => Properties.finishiedEntity;
    }
}