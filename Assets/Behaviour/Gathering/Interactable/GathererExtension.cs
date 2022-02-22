using System;
using UnityEngine;

namespace JHiga.RTSEngine.Gathering
{
    public class GathererExtension : BaseInteractableExtension<GathererProperties>, IGatherer
    {
        private Animator anim;
        public GathererExtension(IExtendableEntity entity, GathererProperties properties) : base(entity, properties)
        {
            anim = Entity.MonoBehaviour.GetComponent<Animator>();
        }
        public int Capacity => Properties.carryingCapacity;
        public int CurrentResourceType { get; set; }

        private int _currentLoad;
        public int CurrentLoad
        {
            get => _currentLoad;
            set
            {
                _currentLoad = value;
                if (_currentLoad > Capacity)
                    _currentLoad = Capacity;
                if(_currentLoad > 0)
                    anim.SetLayerWeight(1, 1);
                else
                    anim.SetLayerWeight(1, 0);
            }
        }
        public float Speed => Properties.speed;
        public IGatherable Target { get => Entity.GetExtension<ITargeter>().Target.Value.entity.GetExtension<IGatherable>(); }
        private IDropoff _dropoff;
        public IDropoff Dropoff
        {
            get
            {
                if (_dropoff == null || Array.IndexOf(_dropoff.ResourceTypes, CurrentResourceType) < 0)
                    _dropoff = DropoffManager.GetClosest(CurrentResourceType, Entity.MonoBehaviour.transform.position);
                return _dropoff;
            }
        }
        public void DropOff()
        {
            if (_dropoff == null)
                return;
            if(PlayerContext.PlayerId == Entity.UniqueID.playerIndex)
                _dropoff.Deliver(new ResourceData
                {
                    resourceType = CurrentResourceType,
                    amount = CurrentLoad
                });
            CurrentLoad = 0;
        }
        public void Gather()
        {
            if (Target == null)
                return;
            if(Target.ResourceType != CurrentResourceType)
            {
                CurrentResourceType = Target.ResourceType;
                CurrentLoad = 0;
            }
            CurrentLoad += Target.Gather(Speed);
        }
        public override void Clear()
        {
            base.Clear();
            _dropoff = null;
        }
    }
}