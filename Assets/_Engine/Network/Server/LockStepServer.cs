using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockStepServer : MonoBehaviour
{
    #region Initialization
    private float startTime = 0;
    [SerializeField] private float stepSize = 1f;
    public static LockStepServer Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        startTime = Time.time;
    }
    #endregion
    private ulong count;
    private float time;
    private void Update()
    {
        if (Time.time - startTime - time > stepSize)
        {
            time = Time.time - startTime;
            count++;
            LockStepNetwork.Instance.StepClientRPC(time, count);
        }
    }
}
