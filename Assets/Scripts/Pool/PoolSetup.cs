using UniRx;
using UnityEngine;

namespace Assets.Scripts.Pool
{
    [AddComponentMenu("Pool/PoolSetup")]
    public class PoolSetup : MonoBehaviour
    {//обертка для управления статическим классом PoolManager

        #region Unity scene settings
        [SerializeField] private PoolManager.PoolPart[] pools;

        public ReactiveCommand onPoolReady = new ReactiveCommand();
        #endregion

        #region Methods
        void OnValidate()
        {
            for (int i = 0; i < pools.Length; i++)
            {
                pools[i].name = pools[i].prefab.name;
            }
        }

        void Awake()
        {
            Initialize();
        }

        void Initialize()
        {
            PoolManager.Initialize(pools);
            onPoolReady?.Execute();
        }
        #endregion
    }
}