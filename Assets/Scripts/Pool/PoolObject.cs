using UnityEngine;

[AddComponentMenu("Pool/PoolObject")]
public class PoolObject : MonoBehaviour
{
    #region Interface
    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
    #endregion
}