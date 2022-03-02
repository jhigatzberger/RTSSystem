using UnityEngine;
using UnityEngine.UI;

public class GenericBar : MonoBehaviour, IProgressIndicator
{
    [SerializeField] private Transform cam;
    [SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;
    [SerializeField] private float value;
    [SerializeField] private float maxValue;

    void Awake()
    {
        slider.maxValue = maxValue;
        slider.value = value;
        fill.color = gradient.Evaluate(1f);
    }
    private void OnEnable()
    {
        cam = Camera.main.transform;
    }
    public void SetProgress(float value)
    {
        slider.value = value;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    void FixedUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }

    public void Hide(bool hide)
    {
        gameObject.SetActive(!hide);
    }
}
