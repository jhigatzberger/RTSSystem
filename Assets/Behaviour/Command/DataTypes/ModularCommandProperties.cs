using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine.CommandPattern
{
    public class ModularCommandProperties : CommandProperties
    {
        [SerializeField] private BaseDecision[] _contextDecisions;
        [SerializeField] private BaseDecision[] _forcedDecisions;
        [SerializeField] private CommandAction[] _onExecute;
        public override bool IsApplicable(ICommandable entity, bool forced = false)
        {
            if (forced)
            {
                foreach (BaseDecision d in _forcedDecisions)
                    if (!d.Decide(entity.Entity))
                        return false;
            }
            else
            {
                foreach (BaseDecision d in _contextDecisions)
                    if (!d.Decide(entity.Entity))
                        return false;
            }
            return true;
        }
        public override void Execute(ICommandable commandable, ResolvedCommandReferences references)
        {
            foreach (CommandAction a in _onExecute)
                a.Execute(commandable, references);
        }
        public override Target PackTarget(ICommandable commandable)
        {
            return Target.FromContext;
        }
    }
}
