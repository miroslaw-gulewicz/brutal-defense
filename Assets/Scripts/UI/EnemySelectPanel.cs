using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySelectPanel : MonoBehaviour
{
	[SerializeField] private EnemyMiniatureTile _enemyTilePrefab;

	[SerializeField] private RectTransform _content;

	[SerializeField] private ScrollRect _scrollRect;

	List<EnemyMiniatureTile> _enemyTiles = new List<EnemyMiniatureTile>();

	public event Action<EnemyObject> TargetSelected;

	public void DisplayEnemies(List<EnemyObject> enemies)
	{
		int enemiesCount = enemies.Count;
		if (enemiesCount > _enemyTiles.Count)
		{
			int tilesToSpawn = enemiesCount - _enemyTiles.Count;
			for (int i = 0; i < tilesToSpawn; i++)
				_enemyTiles.Add(Instantiate(_enemyTilePrefab, _content.transform));
		}

		var index = 0;
		foreach (var item in _enemyTiles)
		{
			if (enemiesCount > index)
			{
				EnemyMiniatureTile.Init(item, enemies[index++], OnTileSelected);
				item.gameObject.SetActive(true);
			}
			else
				item.gameObject.SetActive(false);

			item.SetSelected(false);
		}
	}

	public void SelectTile(System.Object objectDefId)
	{
		_enemyTiles.ForEach(item => item.SetSelected(false));
		EnemyMiniatureTile enemyMiniatureTile = _enemyTiles.Find(tile =>
			tile.isActiveAndEnabled && UnityEngine.Object.Equals(tile.EnemyObject, objectDefId));
		if (enemyMiniatureTile != null)
		{
			enemyMiniatureTile.SetSelected(true);
		}
	}

	public void OnTileSelected(EnemyObject obj)
	{
		_enemyTiles.ForEach(item => item.SetSelected(false));
		EnemyMiniatureTile enemyMiniatureTile =
			_enemyTiles.Find(tile => tile.isActiveAndEnabled && tile.EnemyObject == obj);

		if (enemyMiniatureTile != null)
		{
			enemyMiniatureTile.SetSelected(true);
			TargetSelected.Invoke(obj);
		}
	}
}