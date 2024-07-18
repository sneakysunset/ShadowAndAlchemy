using System.Collections;
using System.Collections.Generic;
using TriInspector;
using UnityEngine;

[DeclareBoxGroup("Grabbable"), DeclareHorizontalGroup("Grabbable/Items")]
public class Player_ItemManager : MonoBehaviour
{
    [ShowInInspector, ReadOnly, Group("Grabbable/Items")] private IGrabbable _HeldItem;
    [ShowInInspector, ReadOnly, Group("Grabbable/Items")] private IGrabbable _ClosestItem;
    [ShowInInspector, ReadOnly, Group("Grabbable/Items")] private List<IGrabbable> _InRangeItems;
    [SerializeField] private Transform _HoldPoint;


    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.TryGetComponent(out IGrabbable item))
        {
            OnItemEnterRange(item);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out IGrabbable item))
        {
            OnItemExitRange(item);
        }
    }

    private void OnItemEnterRange(IGrabbable item)
    {
        if (item.IsHoldable())
        {
        }
    }

    private void OnItemExitRange(IGrabbable item)
    {
        if (_InRangeItems.Contains(item))
        {
        }
    }
}
