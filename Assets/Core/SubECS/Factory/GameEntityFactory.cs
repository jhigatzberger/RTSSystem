using UnityEngine;

namespace JHiga.RTSEngine
{
    public abstract class GameEntityFactory : ScriptableObject
    {
        public static GameEntityFactory Get(UID uid)
        {
            return PlayerProperties.Get(uid).Factories[uid.poolIndex];
        }
        public int PlayerId { get; private set; }
        public int Index { get; internal set; }
        public abstract GameEntity[] Entities { get; }
        public abstract ExtensionFactory[] ExtensionFactories { get; set; }
        public abstract int GenerateEntityID();
        public abstract GameEntity Spawn(Vector3 position, UID uid);
        public static GameEntityFactory CopyForPlayer(GameEntityFactory original, int playerId, ExtensionFactory[] uniqueExtensionFactories)
        {
            GameEntityFactory factory = Instantiate(original);
            factory.PlayerId = playerId;
            factory.ExtensionFactories = uniqueExtensionFactories;
            return factory;
        }
        public IEntityExtension[] Build(IExtendableEntity entity)
        {
            IEntityExtension[] extensions = new IEntityExtension[ExtensionFactories.Length];
            for (int i = 0; i < extensions.Length; i++)
                extensions[i] = ExtensionFactories[i].Build(entity);
            return extensions;
        }
    }

}
