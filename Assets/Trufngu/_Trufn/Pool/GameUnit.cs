using UnityEngine;
public class GameUnit : MonoBehaviour
{
    private Transform _tf;
    public Transform TF
    {
        get
        {
            if (_tf == null)
            {
                _tf = transform;
            }
            return _tf;
        }
    }

    public PoolType poolType;
}
