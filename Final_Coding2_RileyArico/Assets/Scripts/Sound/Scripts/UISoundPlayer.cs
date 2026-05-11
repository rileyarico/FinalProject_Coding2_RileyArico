using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(UnityEngine.UI.Selectable))]
public class UISoundPlayer : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [Header("UI Sound Events")]
    public SoundEvent onHover;
    public SoundEvent onClick;
    public SoundEvent onPress;
    public SoundEvent onRelease;
    public SoundEvent onHoldCharge;     // loop while holding
    public SoundEvent onHoldComplete;   // play once after filling

    [Header("Hold Settings")]
    public float holdThreshold = 0.5f; // seconds to fill
    private Coroutine holdCoroutine;
    private bool hasHeld;
    private float currentFillAmount = 0f;

    [Header("Radial Progress Bar")]
    public Image radialProgressBar;

    private PooledAudioSource chargeLoopSource;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (onHover != null)
            SoundManager.Instance.PlaySound(onHover);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (onPress != null)
            SoundManager.Instance.PlaySound(onPress);

        hasHeld = false;
        currentFillAmount = 0f;
        if (radialProgressBar) radialProgressBar.fillAmount = 0f;

        // Start hold coroutine
        holdCoroutine = StartCoroutine(HoldCheck());

        // Start charge loop
        if (onHoldCharge != null)
        {
           // onHoldCharge.loop = true; // ensure loop is on
            chargeLoopSource = SoundManager.Instance.PlayLoop(onHoldCharge);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!hasHeld && onRelease != null)
            SoundManager.Instance.PlaySound(onRelease);

        if (holdCoroutine != null)
            StopCoroutine(holdCoroutine);

        currentFillAmount = 0f;
        if (radialProgressBar) radialProgressBar.fillAmount = 0f;

        // Stop charge loop cleanly
        if (chargeLoopSource != null)
        {
            chargeLoopSource.Stop();
            chargeLoopSource = null;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!hasHeld && onClick != null)
            SoundManager.Instance.PlaySound(onClick);
    }

    private IEnumerator HoldCheck()
    {
        while (currentFillAmount < 1f)
        {
            currentFillAmount += Time.deltaTime / holdThreshold;
            currentFillAmount = Mathf.Min(currentFillAmount, 1f); // ⬅️ This is new!

            if (radialProgressBar)
                radialProgressBar.fillAmount = currentFillAmount;

            yield return null;
        }

        hasHeld = true;

        // Stop charge sound
        if (chargeLoopSource != null)
        {
            chargeLoopSource.Stop();
            chargeLoopSource = null;
        }

        // Play hold complete sound
        if (onHoldComplete != null)
        {
            SoundManager.Instance.PlaySound(onHoldComplete);
        
        }
    }

}
