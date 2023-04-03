using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	[SerializeField] private Image scoreImageToFill;
	public float Rate { set => scoreImageToFill.fillAmount = value; }
}
