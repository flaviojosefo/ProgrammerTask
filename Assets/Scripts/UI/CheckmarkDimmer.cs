using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CheckmarkDimmer : MonoBehaviour
{
    [SerializeField] private float timeToTransparent = 1f;

    private Image _image;

    // Start dimming when enabled (after save)
    private void OnEnable()
    {
        _image = GetComponent<Image>();

        StartCoroutine(Dim());
    }

    // Lerp from fully visible to transparent
    private IEnumerator Dim()
    {
        Color start = _image.color;
        start.a = 1f;

        Color end = _image.color;
        end.a = 0f;

        float t = 0f;

        while (t < 1f)
        {
            _image.color = Color.Lerp(start, end, t);

            t += Time.deltaTime / timeToTransparent;
            yield return new WaitForEndOfFrame();
        }

        // Disable object
        gameObject.SetActive(false);
    }
}
