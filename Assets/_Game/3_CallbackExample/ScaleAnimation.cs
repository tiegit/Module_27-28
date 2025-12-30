using System;
using System.Collections;
using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
    public void Play(Action callback = null) => StartCoroutine(Process(callback));

    private IEnumerator Process(Action callback)
    {
        float process = 0;
        float animationTime = 1f;

        Vector3 startScale = transform.localScale;

        while (process < animationTime)
        {
            process += Time.deltaTime;

            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, process / animationTime);

            yield return null;
        }

        callback?.Invoke();
    }
}