using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RTS
{
    public abstract class BaseState : ScriptableObject
    {
        protected EntityController controller;
        protected GameObject self;
        public BaseState Init(EntityController controller, GameObject self)
        {
            this.controller = controller;
            this.self = self;
            return this;
        }
        public Type Super
        {
            get
            {
                Type baseType = GetType().BaseType;
                while (!baseType.BaseType.IsAbstract)
                    baseType = baseType.BaseType;
                return baseType;
            }
        }
        public abstract Type Tick();
    }
}
