using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantKillerEnemi : EnemyAI
{
    void Start()
    {
        moveSpeed = 3f;
        targetTag = "Plant";
    }
}
