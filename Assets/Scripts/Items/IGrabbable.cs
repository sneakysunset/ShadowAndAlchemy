using UnityEngine;

public interface IGrabbable
{
    public void Grab(Transform parent);

    public void Release();

    public bool IsHeld { get; }

    public bool IsHoldable();

    public bool Throw(Vector2 endPosition);

    public void OnEnterRange();

    public void OnExitRange();  
}
