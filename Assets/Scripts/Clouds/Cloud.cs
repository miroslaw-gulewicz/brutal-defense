using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;
using static WayPointManager;

public class Cloud : Agent
{
	[SerializeField] SpriteResolver spriteResolver;

	[SerializeField] int spritesInAsset;

	int current;

	private void Start()
	{
		GetComponent<Movement>().Initialize();
	}

	public void Next()
	{
		spriteResolver.SetCategoryAndLabel("Fire", Mathf.PingPong(current++, spritesInAsset).ToString());
	}
}