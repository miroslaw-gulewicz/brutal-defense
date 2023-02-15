using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class TurretSpawner : MonoBehaviour, IHighlitableObjectHolder
{
    [SerializeField]
    private Transform turretsParent;

    [SerializeField]
    private GameObject turretPrefab;

    private DamageVisualizer _visualizer;

    [SerializeField]
    private TurretLevelCollection[] turretLevelCollection;

    private  Dictionary<TurretObjectDef, TurretLevelCollection> turretDefTOCollection;

    private List<TurretBehaviour> _spawnedTurrets = new List<TurretBehaviour>();

    [SerializeField]
    public LayerMask layer;

    public int Layer => layer.value;

    private void Awake()
    {
        _visualizer = FindObjectOfType<DamageVisualizer>();
        turretDefTOCollection = new Dictionary<TurretObjectDef, TurretLevelCollection>();

        foreach (var item in turretLevelCollection)
        {
            foreach (var inner in item.TurretLevels)
            {
                turretDefTOCollection.Add(inner.Definition, item);
            }
        }
    }

    private void Start()
    {
        _spawnedTurrets.AddRange(FindObjectsOfType<TurretBehaviour>());
    }

    internal TurretBehaviour SpawnTurret(TurretObjectDef turretObjectDef, Vector3 gizmoPlacement)
    {
        GameObject turret = ObjectCacheManager._Instance.GetObject(turretPrefab);
        turret.transform.SetParent(turretsParent, false);
        turret.transform.position = gizmoPlacement;
        var tb = turret.GetComponent<TurretBehaviour>();
        tb.DamageVisualizer = _visualizer;
        tb.TurretObject = turretObjectDef;
        tb.DestroyCallBack = OnTurretDestroyed;
        tb.TowerLevel = turretDefTOCollection[turretObjectDef].TurretLevels.First(t => t.Definition.Equals(turretObjectDef)).Level;
        _spawnedTurrets.Add(tb);
        tb.Initialize();
        
        return tb;
    }

    private void OnTurretDestroyed(IDestructable.DestroyedSource source, TurretBehaviour turret)
    {
        _spawnedTurrets.Remove(turret);
    }

    internal void Upgrade(TurretBehaviour currentTower)
    {
        TurretLevelCollection collection = turretDefTOCollection[currentTower.TurretObject];

        foreach (var item in collection.TurretLevels)
        {           
            if (item.Level == currentTower.TowerLevel + 1)
            {
                currentTower.TurretObject = item.Definition;
                currentTower.TowerLevel = item.Level;
                currentTower.Initialize();
                break;
            }
        }
    }

    internal int GetNextLevelTurretCoinValue(TurretBehaviour currentTurret)
    {
        var value = -1;

        TurretLevelCollection collection = turretDefTOCollection[currentTurret.TurretObject];

        foreach (var item in collection.TurretLevels)
        {
            if (item.Level == currentTurret.TowerLevel + 1)
            {
                value = item.Definition.Cost;
            }
        }

        return value;
    }

    internal string GetNextLevelDescription(TurretBehaviour currentTower)
    {
        TurretLevelCollection collection = turretDefTOCollection[currentTower.TurretObject];
        StringBuilder sb = new StringBuilder();
        foreach (var item in collection.TurretLevels)
        {
            if(item.Level == currentTower.TowerLevel + 1)
            {
                sb.AppendLine(item.Definition.TowerName);
                foreach (var inflictor in item.Definition.Inflictors)
                {
                    sb.AppendLine(inflictor.Description());
                }
                
                return sb.ToString();
            }
        }

        return "Max level";
    }

    internal int CalculateValue(TurretBehaviour currentTurret)
    {
        int value = 0;

        if (!turretDefTOCollection.ContainsKey(currentTurret.TurretObject)) return value;
        
        TurretLevelCollection collection = turretDefTOCollection[currentTurret.TurretObject];

        foreach (var item in collection.TurretLevels)
        {
            if (item.Level < currentTurret.TowerLevel)
            {
                value += item.Definition.Cost;    
            }
        }

        return value;
    }

    public void ForEachObject(Action<IHighlightable> cmd)
    {
        _spawnedTurrets.ForEach(cmd);
    }
}
