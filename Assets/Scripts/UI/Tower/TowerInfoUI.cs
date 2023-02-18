using Aim;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfoUI : MonoBehaviour, ITab
{
    [SerializeField]
    private EconomyManager economyManager;

    [SerializeField]
    private WaveManager _waveManager;

    [SerializeField]
    private TurretBehaviour _currentTurret;

    [SerializeField]
    private WorldObjectSelectionManager worldObjectSelectionManager;

    [SerializeField]
    private Image _displayImage; 

    [SerializeField]
    private Canvas _towerInfoCanvas;

    [SerializeField]
    private TMPro.TextMeshProUGUI _turretName;

    [SerializeField]
    private TMPro.TextMeshProUGUI _damage;

    [SerializeField]
    private TMPro.TextMeshProUGUI _range;

    [SerializeField]
    private TMPro.TextMeshProUGUI _fireSpeed;

    [SerializeField]
    private TMPro.TextMeshProUGUI _hp;

    [SerializeField]
    private PayButton sellTowerButton;

    [SerializeField]
    private PayButton upgradeTowerButton;    
    
    [SerializeField]
    private UpgradeTowerButton upgradeTowerControl;

    [SerializeField]
    private TMPro.TMP_Dropdown shootingStrategyChoose;

    [SerializeField]
    private EnemySelectPanel _enemySelectPanel;

    [SerializeField]
    private ShootingStrategySwitcher _turretShootingStrategySwitcher;

    [SerializeField]
    private List<TowerShootingStrategyDropdownData> strategies;

    public void Awake()
    {
        sellTowerButton.onClick.AddListener(SellTower);
        upgradeTowerButton.onClick.AddListener(DoUpgrade);

        shootingStrategyChoose.ClearOptions();
        List<TMPro.TMP_Dropdown.OptionData> options = new List<TMPro.TMP_Dropdown.OptionData>();

        foreach(var item in strategies)
        {
            options.Add(item);
        }
        
        shootingStrategyChoose.AddOptions(options);
        shootingStrategyChoose.onValueChanged.AddListener(OnStrategyChanged);

        _enemySelectPanel.TargetSelected += _enemySelectPanel_TargetSelected;
    }

    private void _enemySelectPanel_TargetSelected(EnemyObject obj)
    {
        _turretShootingStrategySwitcher.EnemyObject = obj;
        _turretShootingStrategySwitcher.SetupTargetingSystem();
    }

    private void OnStrategyChanged(int strategyIndex)
    {
        _enemySelectPanel.gameObject.SetActive(false);
        var targetingStrategyType = strategies[strategyIndex].targeting;

        _turretShootingStrategySwitcher.Shooting = _currentTurret.GetComponent<Shooting>();
            
        _turretShootingStrategySwitcher.SystemType = targetingStrategyType;

        if (targetingStrategyType == TargetingSystemType.PRIORITY)
        {
            _enemySelectPanel.gameObject.SetActive(true);
            _enemySelectPanel.DisplayEnemies(_waveManager.WaveEnemiesDefinition());
            _enemySelectPanel.SelectTile(_turretShootingStrategySwitcher.Shooting.TargetingSystem.TargetDefinition);
        } 
        else
        {
            _turretShootingStrategySwitcher.SetupTargetingSystem();
        }
    }

    private void DoUpgrade()
    {
        economyManager.UpgradeTower(_currentTurret);
        DisplayTurretInfo(_currentTurret);
    }

    private void SellTower()
    {
        worldObjectSelectionManager.Deselect(_currentTurret);
        economyManager.SellTower(_currentTurret);
        DisplayTurretInfo(_currentTurret);
    }

    public void Hide()
    {
        _towerInfoCanvas.gameObject.SetActive(false);
        _enemySelectPanel.gameObject.SetActive(false);
    }

    public void Show()
    {
        _towerInfoCanvas.gameObject.SetActive(true);
    }

    internal void DisplayTurretInfo(TurretBehaviour turret)
    {
        _currentTurret = turret;

        _displayImage.sprite = _currentTurret.TurretObject.Sprite;
        _turretName.text = turret.TurretObject.TowerName;
        _range.text = turret.TurretObject.Range.ToString();
        _hp.text = turret.BasicStats[StatEnum.HP].ToString();
        _fireSpeed.text = turret.BasicStats[StatEnum.ATTACK_SPEED].ToString();
        _damage.text = turret.TurretObject.Projectile.Damage.ToString();

        sellTowerButton.Value = economyManager.TurretValue(_currentTurret);

        upgradeTowerControl.CurrentTower = turret;
        var nextUpgrade = economyManager.NextLevelTowerValue(_currentTurret);
        upgradeTowerButton.gameObject.SetActive(nextUpgrade != -1);
        if (nextUpgrade != -1)
        {
            upgradeTowerButton.Value = nextUpgrade;
        }

        Shooting shooting = turret.GetComponent<Shooting>();
        TargetingSystemType targetingSystemType = TargetingSystemFactory.AsTargetingSystemType(shooting.TargetingSystem);
        shootingStrategyChoose.value = strategies.FindIndex(data => data.targeting == targetingSystemType);
        OnStrategyChanged(shootingStrategyChoose.value);
    }

    [Serializable]
    public class TowerShootingStrategyDropdownData : TMPro.TMP_Dropdown.OptionData
    {
        [SerializeField]
        internal TargetingSystemType targeting;
    }
}
