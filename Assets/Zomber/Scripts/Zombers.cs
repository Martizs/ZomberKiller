using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombers : MonoBehaviour
{
    public void BroadcastGameOver()
    {
        BroadcastMessage("GameOver", SendMessageOptions.DontRequireReceiver);
    }
}
