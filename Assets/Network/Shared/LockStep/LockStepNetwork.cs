using Unity.Netcode;

/// <summary>
/// TODO: DELETE!
/// </summary>
public class LockStepNetwork : NetworkBehaviour
{
    #region Singleton
    public static LockStepNetwork Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    [ClientRpc]
    public void StepClientRPC(float time, ulong count)
    {
        LockStep.Step(time, count);
    }
}
