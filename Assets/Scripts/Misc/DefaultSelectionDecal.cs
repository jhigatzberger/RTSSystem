using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultSelectionDecal : MonoBehaviour
{
    void Awake()
    {
        BoxCollider collider = transform.parent.GetComponent<BoxCollider>();
        Vector3 rotatedScale = collider.size;
        float height = rotatedScale.y, width = rotatedScale.x > rotatedScale.z ? rotatedScale.x : rotatedScale.z;
        rotatedScale.z = height;
        rotatedScale.y = width;
        rotatedScale.x = width;
        transform.localScale = rotatedScale * 2;
    }

    IEnumerator ScaleUpAndDown(Transform transform, Vector3 upScale, float duration)
    {
        Vector3 initialScale = transform.localScale;

        for (float time = 0; time < duration * 2; time += Time.deltaTime)
        {
            float progress = Mathf.PingPong(time, duration) / duration;
            transform.localScale = Vector3.Lerp(initialScale, upScale, progress);
            yield return null;
        }
        transform.localScale = initialScale;
    }

    private void OnEnable()
    {
        StartCoroutine(ScaleUpAndDown(transform, new Vector3(1.2f, 1.2f, 1.2f), 0.1f));
    }
}
