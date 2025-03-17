using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

public class WeaponOrbitController : MonoBehaviour
{
    [Header("轨道参数")]
    [SerializeField] private float orbitRadius;    // 当前实际半径（私有但序列化，便于调试）
    public float OrbitRadius => orbitRadius;      // 公共只读属性（可选）
                                                  // [System.Serializable]

    ActiveSkill weaponData;
    public class LevelSettings
    {
        public int level;
        public int weaponCount;
        public float orbitRadius;
        public float rotationSpeed;
        public GameObject weaponPrefab; // 每个等级独立的武器预制体

        public LevelSettings()
        {
        }

        public LevelSettings(int level, int weaponCount, float orbitRadius, float rotationSpeed, GameObject weaponPrefab)
        {
            this.level = level;
            this.weaponCount = weaponCount;
            this.orbitRadius = orbitRadius;
            this.rotationSpeed = rotationSpeed;
            this.weaponPrefab = weaponPrefab;
        }
    }

    [Header("默认武器")]
    public GameObject defaultWeaponPrefab; // 用于未配置时的回退

    private GameObject _currentWeaponPrefab; // 当前使用的预制体

    [Header("核心配置")]
    public Transform target;                // 需要追踪的移动目标
                                            //  public GameObject weaponPrefab;        // 武器预制体
                                            //  public GameObject newWeaponPrefab;     //新武器预制体
    public float rotationSpeed = 30f;      // 基础旋转速度
    public Vector3 rotationAxis = Vector3.forward; // 旋转轴

    [Header("等级系统")]
    public List<LevelSettings> levelConfigs = new List<LevelSettings>();

    [Header("运行时控制")]
    [SerializeField] private int _currentLevel = 0;
    public int currentLevel
    {
        get => _currentLevel;
        set => SetLevel(value);
    }

    [Header("优化设置")]
    public bool useObjectPool = true;
    public int maxPoolSize=6;
    public float radiusChangeSpeed = 2f;

    private List<GameObject> activeWeapons = new List<GameObject>();
    private Queue<GameObject> weaponPool = new Queue<GameObject>();

    private float targetRadius;
    private int targetWeaponCount;
    ActiveSkill skill;//当前技能信息




    void Start()
    {
        InitializePool();
        currentLevel = 1;
        SetLevel(_currentLevel);
    }

    void Update()
    {
        UpdateRadius();
        UpdateWeaponPositions();
    }

    // ======== 核心功能 ========
    void InitializePool()//对象池初始化
    {
        if (!useObjectPool) return;

        for (int i = 0; i < maxPoolSize; i++)
        {
            GameObject weapon = Instantiate(defaultWeaponPrefab);
            weapon.SetActive(false);
            weapon.transform.SetParent(transform);
            weaponPool.Enqueue(weapon);
        }
    }



    void SetLevel(int newLevel)//等级切换
    {
        newLevel = Mathf.Clamp(newLevel, 0, levelConfigs.Count);

        _currentLevel = newLevel;

        LevelSettings config = levelConfigs.Find(c => c.level == newLevel);
        if (config == null) return;

        // 检查是否需要更换武器类型
        if (defaultWeaponPrefab != null)
        {
            ClearAllWeapons();
            _currentWeaponPrefab = config.weaponPrefab;
            InitializePool(); // 重新初始化对象池
        }

        targetWeaponCount = config.weaponCount;
        targetRadius = config.orbitRadius;
        rotationSpeed = config.rotationSpeed;

        UpdateWeaponCount();
    }
    void ClearAllWeapons()
    {
        // 归还所有活动武器到池中
        while (activeWeapons.Count > 0)
        {
            ReturnWeaponToPool(activeWeapons[0]);
        }

        // 清空对象池（可选：销毁旧类型武器）
        if (useObjectPool)
        {
            foreach (var weapon in weaponPool)
            {
                Destroy(weapon);
            }
            weaponPool.Clear();
        }
    }

    void UpdateWeaponCount()//更改武器数量
    {
        // 增加武器
        while (activeWeapons.Count < targetWeaponCount)
        {
            GameObject weapon = GetWeaponFromPool();
            weapon.GetComponent<Hurtwhele>().Init(weaponData);
            activeWeapons.Add(weapon);
        }

        // 减少武器
        while (activeWeapons.Count > targetWeaponCount)
        {
            ReturnWeaponToPool(activeWeapons[activeWeapons.Count - 1]);
        }
    }

    GameObject GetWeaponFromPool()//从对象池获取武器
    {
        if (weaponPool.Count > 0)
        {
            GameObject weapon = weaponPool.Dequeue();
            weapon.SetActive(true);
            return weapon;
        }
        return Instantiate(defaultWeaponPrefab);
    }

    //武器返回到对象池
    void ReturnWeaponToPool(GameObject weapon)
    {
        weapon.SetActive(false);
        weaponPool.Enqueue(weapon);
        activeWeapons.Remove(weapon);
    }

    // ======== 运动控制 ========
    void UpdateRadius()
    {
        orbitRadius = Mathf.Lerp(
            orbitRadius,
            targetRadius,
            Time.deltaTime * radiusChangeSpeed);
        orbitRadius = Mathf.Max(orbitRadius, 0.1f);
    }
    /// <summary>
    /// 武器位置更新
    /// </summary>
    void UpdateWeaponPositions()
    {
        if (target == null || activeWeapons.Count == 0) return;

        float baseAngle = rotationSpeed * Time.time;

        for (int i = 0; i < activeWeapons.Count; i++)
        {
            float angle = baseAngle + (360f / activeWeapons.Count) * i;
            activeWeapons[i].transform.position = CalculateOrbitPosition(angle);
        }
    }
    //位置计算
    Vector3 CalculateOrbitPosition(float angle)
    {
        Quaternion rotation = Quaternion.AngleAxis(angle, rotationAxis);
        return target.position + rotation * (Vector3.right * orbitRadius);
    }

    // ======== 编辑器辅助 ========
    void OnValidate()
    {
        if (Application.isPlaying)
        {
            SetLevel(_currentLevel);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (target != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(target.position, orbitRadius);
        }
    }

    internal void Init(ActiveSkill skill)
    {
        string assetName = Path.GetFileNameWithoutExtension(skill.This_object_path);
        defaultWeaponPrefab = ResourcesLoader.LoadResources<GameObject>(Application.streamingAssetsPath + "/role", assetName, "role");
        target = this.transform;
        for (int i = 1; i <= 5; i++)
        {
            levelConfigs.Add(new LevelSettings(skill.Level, skill.Level + 1, (float)(skill.Level * (1 - 0.1 * skill.Level)), (skill.Level + 1) * 100, defaultWeaponPrefab));
        }
        weaponData = skill;
        currentLevel = skill.Level;
        

    }
}