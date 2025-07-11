using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PowerUpData", menuName = "Scriptable Objects/PowerUpData")]
public class PowerUpData : ScriptableObject
{
    [Header("PowerUpDataList")]
    public List<PowerUpLevel> _list;

    [System.Serializable]
    public class PowerUpLevel
    {
        [Header("PowerUpName")]
        public string _name;

        [Header("ButtonImage")]
        public Sprite _sprite;

        [Header("PowerUpValue"), Tooltip("上昇値")]
        public int _powerUpValue;

        [Header("DefaultValue"), Tooltip("初期値")]
        public int _defaultValue;
    }
}
