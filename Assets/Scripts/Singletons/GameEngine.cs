using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : Singleton<GameEngine>
{
    [SerializeField] private Player _player;
    public Player Player { get { return _player; } }
    
}
