using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideEnemi : EnemyAI
{
    void Start()
    {
        moveSpeed = 5f;
        targetTag = "House";
    }
}
