using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : Singleton<GameEngine>
{
    public Player GetPlayer { get { return GameObject.FindWithTag("Player").GetComponent<Player>(); } }
}
