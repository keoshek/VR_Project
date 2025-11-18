using UnityEngine;

[System.Serializable]
public class LazyAnimatorHash
{
    private readonly string _paramName;
    private int _hash;

    public LazyAnimatorHash(string paramName)
    {
        _paramName = paramName;
    }

    public int Hash
    {
        get
        {
            if (_hash == 0)
                _hash = Animator.StringToHash(_paramName);
            return _hash;
        }
    }

    // Implicit cast so you can use it directly as an int
    public static implicit operator int(LazyAnimatorHash lazy) => lazy.Hash;

    public override string ToString() => _paramName;
}
