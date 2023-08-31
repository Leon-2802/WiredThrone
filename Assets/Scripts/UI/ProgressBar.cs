using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void SetMaxValue(float value)
    {
        slider.maxValue = value;
        slider.value = 0f;
    }

    public IEnumerator RunTimerProgress(float maxValue, ESounds endSound, UnityEvent callback)
    {
        slider.maxValue = maxValue;
        slider.value = 0f;

        for (float i = 0f; i < slider.maxValue; i += Time.deltaTime)
        {
            slider.value = i;
            yield return null;
        }

        SoundManager.instance.PlaySoundOneShot(endSound);
        callback.Invoke();
    }
}
