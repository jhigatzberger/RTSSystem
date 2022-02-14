using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepDisplay : MonoBehaviour
{
    Text text;
    private void Awake()
    {
        text = GetComponent<Text>();
        LockStep.OnStep += DisplayStep;
    }

    public void DisplayStep()
    {
        text.text = LockStep.count.ToString();
    }
}
