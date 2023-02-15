using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeleteAfterBehaviour : MonoBehaviour
{
    [SerializeField]
    public float time;

    [SerializeField]
    public UnityEvent OnObjectDisabled;
    
    private void OnEnable()
    {
        if(time > 0)
            StartCoroutine(InactivateAfter(time));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator InactivateAfter(float time)
    {
        yield return new WaitForSeconds(time);       
        IPreDestroyListener[] preDestroyListeners = transform.GetComponentsInChildren<IPreDestroyListener>();
        for (int i = 0; i < preDestroyListeners.Length; i++)
        {
            preDestroyListeners[i].OnPreDestroy();
        }
        yield return new WaitForEndOfFrame();
        OnObjectDisabled?.Invoke();
        Destroy(gameObject);
    }

}
