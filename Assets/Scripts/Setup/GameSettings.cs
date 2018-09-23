using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GameSettings")]
public class GameSettings : ScriptableObject
{
	[SerializeField] private float playerSpeed = 3f;
    public float PlayerSpeed
    {
        get { return playerSpeed; }
    }
    
	[SerializeField] private String floorLayer = "Floor";
    public String FloorLayer
    {
        get { return floorLayer; }
    }
}
