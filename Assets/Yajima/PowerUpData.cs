using System.Collections.Generic;
using UnityEngine;

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

        [Header("ButtonText")]
        public string _Text;
        /// <summary>
        /// 弾のスピード　初期値0.1f
        /// </summary>
        [Header("弾の移動速度UP初期値0.1f")]
        public float _ammoSpeedUP;
        /// <summary>
        ///　弾の飛距離　初期値35f
        /// </summary>
        [Header("弾の飛距離UP初期値35f")]
        public float _ammoMaxPosUP;
        /// <summary>
        /// プレイヤー移動速度 初期値10f
        /// </summary>
        [Header("プレイヤー移動速度UP初期値10f")]
        public float _moveSpeedUP;
        /// <summary>
        /// インクの最大値　初期値100f
        /// </summary>
        [Header("インクの最大値UP初期値100f")]
        public float _maxInkUP;
        /// <summary>
        /// HP回復 最大値300f
        /// </summary>
        [Header("HP回復")]
        public float _hpRecovery;

    }
}
