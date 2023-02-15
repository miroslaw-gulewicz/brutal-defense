using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    [SerializeField]
    TurretSpawner turretSpawner;

    [SerializeField]
    private int _coins;

    public int Coins { get => _coins; set => _coins = value; }

    public event Action OnCoinsChanged;

    private void Start()
    {
        
    }

    public void AddCoin(int amount)
    {
        _coins += amount;
        OnCoinsChanged.Invoke();
    }


    public bool HasSufficientCoins(int cost)
    {
        return Coins >= cost;
    }

    internal void Buy(int cost)
    {
       AddCoin(-cost);
    }

    public void UpgradeTower(TurretBehaviour currentTurret)
    {
        AddCoin(-NextLevelTowerValue(currentTurret));
        turretSpawner.Upgrade(currentTurret);
    }

    internal void SellTower(TurretBehaviour currentTurret)
    {
        AddCoin(currentTurret.TurretObject.Cost);
        AddCoin(TurretValue(currentTurret));
        currentTurret.gameObject.SetActive(false);
    }

    internal int TurretValue(TurretBehaviour currentTurret)
    {
        return turretSpawner.CalculateValue(currentTurret);
    }

    internal int NextLevelTowerValue(TurretBehaviour currentTurret)
    {
        return turretSpawner.GetNextLevelTurretCoinValue(currentTurret);
    }
}
