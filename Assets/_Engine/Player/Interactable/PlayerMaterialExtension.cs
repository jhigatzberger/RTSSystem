using UnityEngine;

namespace JHiga.RTSEngine
{
    public class PlayerMaterialExtension : BaseInteractableExtension<PlayerMaterialsProperties>
    {
        public PlayerMaterialExtension(IExtendableInteractable extendable, PlayerMaterialsProperties properties) : base(extendable, properties) { }

        public override void Enable()
        {
            Material mat = Properties.playerMaterials[PlayerContext.PlayerId];
            foreach (Renderer r in Extendable.MonoBehaviour.GetComponentsInChildren<Renderer>())
                if (r.material.mainTexture == Properties.changeTexture)
                    r.material = mat;
        }

    }
}