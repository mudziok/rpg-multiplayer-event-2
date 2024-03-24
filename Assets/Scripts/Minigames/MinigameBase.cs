using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameBase : MonoBehaviour
{
    public event Action actionPerformedEvent;
    public event Action closedEvent;

    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    protected void Close()
    {
        closedEvent?.Invoke();
        gameObject.SetActive(false);
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }

    protected void PerformAction()
    {
        actionPerformedEvent?.Invoke();
    }

    protected IEnumerator RotationCoroutine(Transform rect, Vector3 target, float speed, Action onEnd)
    {
        Quaternion targetRotation = Quaternion.Euler(target);
        while (targetRotation != rect.rotation)
        {
            rect.rotation = Quaternion.RotateTowards(rect.rotation, targetRotation, speed * Time.deltaTime);
            yield return null;
        }
        onEnd();
    }

    protected enum FadeDirection
    {
        FadeOut,
        FadeIn
    }
    protected IEnumerator ImageFadeCoroutine(Image image, float duration, FadeDirection direction, Action onEnd)
    {
        image.CrossFadeAlpha(direction == FadeDirection.FadeOut ? 0 : 1, duration, false);
        yield return new WaitForSeconds(duration);
        onEnd();
    }
}
