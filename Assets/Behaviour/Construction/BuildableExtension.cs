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
            _finished = false;
        }

        private bool _finished;
        private bool Finished
        {
            get => _finished;
            set
            {
                Entity.Disable(value);
                _finished = value;
            }
        }
        public bool Construct(int speed)
        {
            if (Finished)
                return false;
            CurrentConstructionLevel += speed;
            return Finished;
        }

        private int _currentConstructionLevel;
        public int CurrentConstructionLevel
        {
            get => _currentConstructionLevel;
            private set
            {
                if (value > MaxConstructionLevel)
                    value = MaxConstructionLevel;
                _currentConstructionLevel = value;
                if (value == MaxConstructionLevel)
                    Finished = true;
            }
        }
        public GameEntityPool FinishiedEntity => Properties.finishiedEntity;
    }
}