using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;
using static WayPointManager;

public class Cloud : Agent
{
	[SerializeField] SpriteResolver _spriteResolver;

	[SerializeField] int _spritesInAsset;

	int current;

	private void Awake()
	{
		GetComponent<Movement>().Initialize();
	}

	public void Next()
	{
		_spriteResolver.SetCategoryAndLabel("Fire", Mathf.PingPong(current++, _spritesInAsset).ToString());
	}
}