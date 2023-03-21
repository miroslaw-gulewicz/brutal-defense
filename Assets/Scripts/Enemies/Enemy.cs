using Effect;
using System;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;
using static IDestructable;

public class Enemy : Agent
{
	[SerializeField] private bool autoInit;

	[SerializeField] private EnemyObject enemyObject;

	[SerializeField] private SpriteLibrary spriteLibrary;

	public override UnityEngine.Object ObjectDefinition => enemyObject;

	private void Awake()
	{
		if (autoInit) Initialize();
	}

	protected void FixedUpdate()
	{
		_effectManager.OnUpdate();
	}

	public void Initialize()
	{
		_sprite.sprite = EnemyObject.Sprite;
		spriteLibrary.spriteLibraryAsset = EnemyObject.SpriteLibrary;


		_statsManager.ResistanceHolder = new ResistanceHolder(EnemyObject.Resistance);
		_statsManager.BasicStatsHolder = new BasicStatsHolder(EnemyObject.BasicStats);

		_effectManager = new EffectManager(_statsManager, this);
		_effectManager.DamageTakenCallback += OnDamageTaken;
		_effectManager.OnEffectAppliedCallback += OnEffectApplied;
		_effectManager._hpBelow0Callback += (ctx) => DoDestroy(DestroyedSource.KILLED);
		_effectManager.DefaultEffects = EnemyObject.DefaultEffects;

		_statsManager.BasicStatsHolder.CurrentHpUpdated += OnUpdateHp;
		OnUpdateHp();

		if (enemyObject.SelfInflictors != null)
			_effectManager.ApplyEffect(enemyObject.SelfInflictors);
	}

	private void OnUpdateHp()
	{
		_hpBar.Value = BasicStats.CurrentHp / BasicStats.StartHP;
	}

	public void DoDestroy(DestroyedSource source)
	{
		if (DestroyCallBack != null)
			DestroyCallBack.Invoke(source, this);
		else
			gameObject.SetActive(false);
	}

	private void OnDisable()
	{
		_effectManager?.ClearEffects();
	}

	public EnemyObject EnemyObject
	{
		get => enemyObject;
		set => enemyObject = value;
	}

	public DamageVisualizer DamageVisualizer
	{
		get => _damageVisualizer;
		set => _damageVisualizer = value;
	}

	public Action<DestroyedSource, Enemy> DestroyCallBack { get; internal set; }
}