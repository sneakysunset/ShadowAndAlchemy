using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour, IGrabbable
{
    [SerializeField] private float _BaseTimeToThrow;
    [SerializeField] private float _MaxThrowRange;
    [SerializeField] private AnimationCurve _ThrowTimeByDistanceCurve;
    [SerializeField] private AnimationCurve _ThrowSpeedAtDistanceCurve; 
    private bool _IsHeld;
    private bool _IsThrown;

    bool IGrabbable.IsHeld => _IsHeld;
    protected Rigidbody2D _Rigidbody;
    IEnumerator _ThrowEnumHandler;

    private void Start()
    {
        _Rigidbody = GetComponent<Rigidbody2D>();
    }

    public virtual void Grab(Transform parent)
    {
        _Rigidbody.isKinematic = true;
        _IsHeld = true;
        transform.parent = parent;
        _ThrowEnumHandler = null;
    }

    public void Release()
    {
        _IsHeld = true;
        transform.parent = null;
    }


    public virtual bool IsHoldable()
    {
        if (_IsHeld) 
            return false;
        else
            return true;
    }

    public virtual bool Throw(Vector2 endPosition)
    {
        _Rigidbody.isKinematic = false;
        _ThrowEnumHandler = ThrowMotion(endPosition);
        StartCoroutine(_ThrowEnumHandler);
        return true;
    }

    IEnumerator ThrowMotion(Vector2 endPosition)
    {
        _IsThrown = true;
        float interpoler = 0;
        Vector2 startPosition = transform.position;
        float distance = Vector2.Distance(startPosition, endPosition);
        float duration = GetThrowDuration(distance);
        WaitForSeconds waiter = new WaitForSeconds(duration);
        while (interpoler < 1)
        {
            interpoler += Time.deltaTime / duration;
            Vector2 newPosition = Vector2.Lerp(startPosition, endPosition, _ThrowSpeedAtDistanceCurve.Evaluate(interpoler));
            _Rigidbody.MovePosition(newPosition);
            yield return waiter;
        }
        transform.position = endPosition;
        OnThrowEnd();
    }

    protected virtual void OnThrowEnd()
    {
        _IsThrown = false;
        _Rigidbody.isKinematic = true;
        _ThrowEnumHandler = null;
    }


    protected virtual float GetThrowDuration(float distance)
    {
        return _ThrowTimeByDistanceCurve.Evaluate(distance / _MaxThrowRange) * _BaseTimeToThrow;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_IsThrown)
        {
            if(_ThrowEnumHandler != null)
                StopCoroutine(_ThrowEnumHandler);
            OnThrowEnd();
        }
    }
}
