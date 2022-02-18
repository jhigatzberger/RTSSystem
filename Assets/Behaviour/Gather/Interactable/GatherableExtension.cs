namespace JHiga.RTSEngine.Gather
{
    public class GatherableExtension : BaseInteractableExtension<GatherableProperties>, IGatherable
    {
        public GatherableExtension(IExtendableEntity entity, GatherableProperties properties) : base(entity, properties)
        {
        }

        public override void Enable()
        {
            TotalStorage = Properties.totalStorage;
        }
        public int Rate => Properties.rate;
        public int ResourceType => Properties.resourceType;
        public IGatherable.StorageType GatherableStorageType => Properties.storageType;
        private int _totalStorage;
        public int TotalStorage {
            get => _totalStorage;
            set
            {
                _totalStorage = value;
                if (_totalStorage <= 0)
                    Entity.Disable();

            }
        }
        public int Gather(float speed)
        {
            int amount = (int)(Rate * speed);
            if (amount > TotalStorage)
                amount = TotalStorage;
            TotalStorage -= amount;
            return amount;
        }
    }
}