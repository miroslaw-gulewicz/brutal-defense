using Effect;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MovementStrategy;

[RequireComponent(typeof(Movement))]
public class Balistic : Agent
{
    [SerializeField]
    private Projectile _projectileDef;

    private EffectInflictor[] _inflictors;

    private Movement _movable;

    [SerializeField]
    private Vector3 _destination;
    public Projectile ProjectileDef { get => _projectileDef; set => _projectileDef = value; }
    public Vector3 Destination { get => _destination; set => _destination = value; }
    public EffectInflictor[] Inflictors { set => _inflictors = value; }

    private void Awake()
    {
        _movable = GetComponent<Movement>();
    }

    private IEnumerator InactivateAfter(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(InactivateAfter(2f));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
    
    [ContextMenu("Initialize")]
    public void Initialize()
    {
        _sprite.sprite = _projectileDef.Sprite;
        transform.LookAt(_destination);
        _movable.Destination = Destination;


        var statsHolder = new BasicStatsHolder(_projectileDef.Stats);

        _statsManager.BasicStatsHolder = statsHolder;

        _effectManager = new EffectManager(_statsManager, this);
        _movable.Initialize();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent(out IDestructable obsticle))
        {
            obsticle.TakeDamage(ProjectileDef.DamageType, ProjectileDef.Damage);
            if (_projectileDef.ImpactEffectPrefab != null)
            {
                GameObject particle = ObjectCacheManager._Instance.GetObject(_projectileDef.ImpactEffectPrefab);
                particle.transform.position = transform.position;
            }
            gameObject.SetActive(false);
        }

        if (_inflictors != null && other.TryGetComponent(out IAffected affector))
            affector.ApplyEffect(_inflictors);
    }

    public override UnityEngine.Object ObjectDefinition => _projectileDef;
}
