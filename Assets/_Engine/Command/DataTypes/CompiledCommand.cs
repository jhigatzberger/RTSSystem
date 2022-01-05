using UnityEngine;

namespace JHiga.RTSEngine.CommandPattern
{
    /// <summary>
    /// Compiled by <see cref="CommandProperties.Compile"/>.
    /// Carries all information to execute (<see cref="CommandProperties.Execute(ICommandable, CompiledCommand)"/>) a command on an <see cref="ICommandable"/> on a specific client.
    /// </summary>
    public struct CompiledCommand
    {
        public int commandID;
        public Vector3 position;
        public int targetID;
    }
}