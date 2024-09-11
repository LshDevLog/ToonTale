using UnityEngine;
using System;
using System.IO;
using UnityEditor;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public StatsData _statsData;
    public EquipmentData _equipmentData;
    public SystemData _systemData;

    private const string DATA_FILE_NAME = "PlayerData.json";
    private string _dataPath;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        _dataPath = Application.dataPath + "/" + DATA_FILE_NAME;
        InitAllData();


    }

    private void InitAllData()
    {
        if (File.Exists(_dataPath))
        {
            LoadData();
        }
    }

    public bool ExistingSaveData()
    {
        bool exist = (File.Exists(_dataPath));

        return exist;
    }
    public void SaveData()
    {
        PlayerDataBox _playerDataBox = new PlayerDataBox();

        if(Equipment.Instance != null)
        {
            var equip = Equipment.Instance;
            _equipmentData._equippedWeapon = equip._equippedWeapon;
            _equipmentData._equippedShield = equip._equippedShield;
            _equipmentData._slot1_Weapon = equip._slot1_Weapon;
            _equipmentData._slot2_Weapon = equip._slot2_Weapon;
            _equipmentData._slot3_Weapon = equip._slot3_Weapon;
        }


        _playerDataBox._statsData = _statsData;
        _playerDataBox._equipmentData = _equipmentData;
        _playerDataBox._systemData = _systemData;

        string json = JsonUtility.ToJson(_playerDataBox, true);

        File.WriteAllText(_dataPath, json);
    }

    public void LoadData()
    {
        string json = File.ReadAllText(_dataPath);

        PlayerDataBox data = JsonUtility.FromJson<PlayerDataBox>(json);
        _statsData = data._statsData;
        _equipmentData = data._equipmentData;
        _systemData = data._systemData;
    }
}