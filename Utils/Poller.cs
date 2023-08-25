using UnityEngine;

public abstract class Poller<T> : MonoBehaviour
{
    private bool initiated = false;
    private bool broadcasted = false;

    private T _lastValue;

    protected virtual T LastValue
    {
        get
        {
            if (!initiated)
            {
                _lastValue = CurrentValue;
                initiated = true;
            }
            return _lastValue;
        }
        set { }
    }

    protected abstract T CurrentValue { get; }

    private void Update()
    {
        if (!initiated)
        {
            LastValue = LastValue;
            broadcasted = true;
        }
        else
        {
            T newValue = CurrentValue;
            if (!broadcasted || !newValue.Equals(LastValue))
            {
                _lastValue = newValue;
                LastValue = newValue;
                broadcasted = true;
            }
        }
    }
}
