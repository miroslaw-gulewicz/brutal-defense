using Aim;
using UnityEngine;

public class Shooting : MonoBehaviour
{
	[SerializeField] private Agent _agent;

	[SerializeField] private Weapon _weapon;

	[SerializeField] private IndicatorBarBehaviour _nextAttackTimerBar;

	[SerializeField] private TargetingSystem _targetingSystem;

	[SerializeField] protected Balistic _balisticPrefab;

	private float lastAttackedAt = float.MinValue;

	[SerializeField] private ProximityTriggerBehaviour proximityTriggerBehaviour;

	[SerializeField] private GameObject _targetingGameObject;

	[SerializeField] private WeaponController _weaponController;

	private float _attackSpeed;

	public Weapon Weapon
	{
		get => _weapon;
		set { _weapon = value; }
	}

	public TargetingSystem TargetingSystem
	{
		get => _targetingSystem;
		set => _targetingSystem = value;
	}

	private void Awake()
	{
		_agent = GetComponent<Agent>();
		proximityTriggerBehaviour.TrigerEnterCallback = TriggerEnter;
		proximityTriggerBehaviour.TrigerExitCallback = TriggerExit;
		proximityTriggerBehaviour.TrigerStayCallback = TriggerEnter;

		if (!_targetingGameObject) _targetingGameObject = gameObject;
	}

	public void Initialize()
	{
		_agent.BasicStats.AttackSpeedUpdated += OnAttackSpeedChanged;
		OnAttackSpeedChanged();
		proximityTriggerBehaviour.Range = _weapon.Range;
		_targetingSystem = TargetingSystemFactory.Supply(TargetingSystemType.SIMPLE);
		if (_weaponController && _weapon.WeaponSpriteLibrary)
			_weaponController.SpriteLibrary = _weapon.WeaponSpriteLibrary;
	}

	private void OnAttackSpeedChanged()
	{
		_attackSpeed = _agent.BasicStats.AttackSpeed;
	}

	private void TriggerExit(Collider obj)
	{
		if (obj.TryGetComponent(out Agent dest))
			_targetingSystem.TargetExits(dest.gameObject);
	}

	private void TriggerEnter(Collider obj)
	{
		if (obj.TryGetComponent(out Agent dest))
			_targetingSystem.TargetEnters(dest.gameObject);
	}

	[ContextMenu("FireAtWill")]
	private void FireAtWill()
	{
		var projectile = ObjectCacheManager._Instance.GetObject(_balisticPrefab.gameObject, false);

		Balistic balistic = projectile.GetComponent<Balistic>();
		balistic.Destination = _targetingSystem.Target.transform.position;
		balistic.ProjectileDef = _weapon.Projectile;
		balistic.Inflictors = _weapon.Inflictors;
		balistic.transform.position = _targetingGameObject.transform.position;
		balistic.Initialize();
		projectile.SetActive(true);
	}

	private void Update()
	{
		if (_targetingSystem.Target)
		{
			if (Time.time > lastAttackedAt + _attackSpeed)
			{
				_weaponController?.Fire();
				FireAtWill();
				lastAttackedAt = Time.time;
				_nextAttackTimerBar.Value = 1f;
				Debug.Log("Fire");
			}
			else
			{
				_nextAttackTimerBar.Value = Mathf.Clamp((Time.time - lastAttackedAt) / _attackSpeed, 0, 1);
			}
		}
	}

	public void ChangeTargetingSystem(TargetingSystem targetingSystem)
	{
		_targetingSystem = targetingSystem;
		_targetingSystem.Setup(targetingSystem);
	}
}