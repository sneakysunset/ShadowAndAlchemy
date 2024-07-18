using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillerEnemi : EnemyAI
{
    void Start()
    {
        moveSpeed = 2f;
        targetTag = "Player";
    }
}
