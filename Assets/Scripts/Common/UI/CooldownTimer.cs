using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownTimer : MonoBehaviour
{
    [SerializeField]
    private Image _overlay;

    private float cooldownTimer = 0;

    private void Start()
    {
        _overlay.gameObject.SetActive(false);
    }

    public void StartTimout(float cooldown, Action cooldownComplete)
    {
        if(cooldownTimer == 0)
            StartCoroutine(StartCooldown(cooldown, cooldownComplete));
    }

    private IEnumerator StartCooldown(float cooldown, Action cooldownComplete)
    {
        cooldownTimer = cooldown;
        float amount = 1f / cooldown ;
        _overlay.gameObject.SetActive(true);
        _overlay.fillAmount = 1f;
        WaitForSeconds waitForSeconds = new WaitForSeconds(1f);
        while (cooldownTimer > 0)
        {
            _overlay.fillAmount -= amount;
            cooldownTimer--;
            yield return waitForSeconds;
        }

        cooldownTimer = 0;
        _overlay.gameObject.SetActive(false);
        cooldownComplete();
    }
}
