using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorBarBehaviour : MonoBehaviour
{
    [SerializeField]
    Image stat;
    
    public float Value { set => stat.fillAmount = value; get => stat.fillAmount; }
}
