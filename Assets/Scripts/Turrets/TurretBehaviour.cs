using Effect;
using System;
using UnityEngine;
using static IDestructable;

public class TurretBehaviour : Agent
{
	[SerializeField] private TurretObjectDef _turretObject;

	public new UnityEngine.Object ObjectDefinition => _turretObject;

	[SerializeField] int towerLevel = 0;

	public TurretObjectDef TurretObject
	{
		get => _turretObject;
		set => _turretObject = value;
	}

	public int TowerLevel
	{
		get => towerLevel;
		set => towerLevel = value;
	}

	public Action<DestroyedSource, TurretBehaviour> DestroyCallBack { get; internal set; }

	[SerializeField] private RangeHighlight _rangeHighlight;

	public void Start()
	{
		Initialize();
	}

	[ContextMenu("Init")]
	public void Initialize()
	{
		_sprite.sprite = TurretObject.Sprite;

		var statsHolder = new BasicStatsHolder(_turretObject.BasicStats);
		var resistanceHolder = new ResistanceHolder(_turretObject.Resistance);


		_statsManager.ResistanceHolder = resistanceHolder;
		_statsManager.BasicStatsHolder = statsHolder;

		_effectManager = new EffectManager(_statsManager, this);
		_effectManager.DamageTakenCallback += OnDamageTaken;

		statsHolder.CurrentHpUpdated += OnUpdateHp;

		_hpBar.Value = 1f;

		Shooting shooting = GetComponent<Shooting>();
		shooting.Weapon = TurretObject;
		shooting.Initialize();

		_rangeHighlight.RangeRadius = _turretObject.Range;
	}

	private void OnUpdateHp()
	{
		_hpBar.Value = (float)BasicStats.CurrentHp / BasicStats.StartHP;
		if (BasicStats.CurrentHp <= 0)
		{
			DestroyCallBack?.Invoke(DestroyedSource.KILLED, this);
			gameObject.SetActive(false);
		}
	}

	public override void SetSelected(bool selected)
	{
		base.SetSelected(selected);
		_rangeHighlight.Highlight(selected);
	}

	public void Update()
	{
		_effectManager.OnUpdate();
	}

	public DamageVisualizer DamageVisualizer
	{
		get => _damageVisualizer;
		set => _damageVisualizer = value;
	}
}