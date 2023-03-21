using Effect;
using System;
using UnityEngine;
using static EffectInflictorAgent;

public class SpellCaster : MonoBehaviour
{
	[SerializeField] private LayerMask _worldLayerMask;

	[SerializeField] private GameObject spellsRoot;

	[SerializeField] private ObjectPlacementControl placementControl;

	[SerializeField] private Spell _activeSpell;

	[SerializeField] private GameObject _target;

	public Spell ActiveSpell
	{
		get => _activeSpell;
	}

	public event Action<GameObject> TargetAquired;

	public event Action<Spell> SpellCasted;


	/// <summary>
	/// Subscribe to events SpellCaster.SpellCasted and SpellCaster.TargetAquired
	/// </summary>
	/// <param name="spell"></param>
	public void SetupSpell(Spell spell)
	{
		_activeSpell = spell;
		placementControl.Reset();
		placementControl.Setup(_activeSpell.GizmoPrefab, OnAquireTarget, _activeSpell.TargetsAgentLayer,
			spell.TargetsAgentLayer != _worldLayerMask);
	}

	public void ClearCasting()
	{
		placementControl.Reset();
		_activeSpell = null;
	}

	private void OnAquireTarget(GameObject newTarget)
	{
		_target = newTarget;
		TargetAquired?.Invoke(_target);
	}

	public void CastSpell()
	{
		if (_activeSpell == null) return;

		if (_activeSpell.TargetsAgentLayer == _worldLayerMask)
		{
			Debug.Log("Gizmo @ " + placementControl.Gizmo.transform.position);
			GameObject spellEffect = _activeSpell.EffectAgent.ApplyEffect(placementControl.Gizmo);
			Debug.Log("Spell @ " + spellEffect.gameObject.transform.position);
		}
		else if (_activeSpell.Inflictors != null)
		{
			_target.GetComponent<IAffected>().ApplyEffect(_activeSpell.Inflictors);
		}

		SpellCasted?.Invoke(_activeSpell);
	}

	[ContextMenu("CastSpell@Target")]
	public void InitAndCastSpell()
	{
		SetupSpell(_activeSpell);
		if (_activeSpell.GizmoPrefab != null)
			placementControl.Gizmo.transform.position = _target.transform.position;
		CastSpell();
	}
}