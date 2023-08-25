using UnityEngine;

[ExecuteAlways]
public class TestManualSectorConnectionStateProvider : Poller<bool>, HierarchyMsg<ConnectionState>.IProvider
{
    public bool connected = false;

    protected override bool CurrentValue
    {
        get => connected;
    }

    protected override bool LastValue
    {
        set => HierarchyMsg<ConnectionState>.Publish(this);
    }

    ConnectionState HierarchyMsg<ConnectionState>.IProvider.Provide()
    {
        return new ConnectionState { connected = LastValue };
    }
}
