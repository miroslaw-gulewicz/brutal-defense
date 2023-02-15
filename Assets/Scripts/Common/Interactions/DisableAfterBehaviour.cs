using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DisableAfterBehaviour : MonoBehaviour
{
    [SerializeField]
    public float time;

    [SerializeField]
    public UnityEvent OnObjectDisabled;
    
    private void OnEnable()
    {
        StartCoroutine(InactivateAfter(time));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator InactivateAfter(float time)
    {
        yield return new WaitForSeconds(time);
        OnObjectDisabled?.Invoke();
        gameObject.SetActive(false);
    }

}
