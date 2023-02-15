using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeHighlight : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _rangeSprite;
    
    [Range(0f, 10f)]
    [SerializeField]
    private float _range;

    public float RangeRadius { set {
            _rangeSprite.transform.localScale = new Vector2(value, value) * 2;
    } }

    public void Awake()
    {
        RangeRadius = _range;
    }

    public void Highlight(bool highlighted)
    {
       gameObject.SetActive(highlighted);
    }
}
