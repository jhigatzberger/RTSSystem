using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Entity
{
    public interface ISpawner : IEntityExtension
    {
        public void AuthorizeID(int id);
        public void Spawn(GameObject gameObject);
    }
}

