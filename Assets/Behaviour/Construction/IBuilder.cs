namespace JHiga.RTSEngine.Construction
{
    public interface IBuilder : IEntityExtension
    {
        public int Speed { get; }
        public IBuildable Target { get; }
        public void Construct();
    }
}