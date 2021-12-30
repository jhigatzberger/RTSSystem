using System;
using UnityEngine.Events;

namespace RTSEngine.Entity
{
    public interface ICommandable : IEntityExtension
    {
        public CommandData? Current { get; set; }
        public void Finish();
        public Command[] CommandCompetence { get; }
        public Command FirstApplicableDynamicallyBuildableCommand { get; }
        public void Enqueue(CommandData command);
        public void ClearCommands();
        public void ExecuteFirstCommand();
    }

}
