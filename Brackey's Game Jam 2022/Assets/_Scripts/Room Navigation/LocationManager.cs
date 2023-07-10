using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager : Singleton<LocationManager>
{
    [field: SerializeField] public Location playerLocation { get; private set; }
    [field: SerializeField] public Location enemyLocation { get; private set; }


    /// <summary>
    /// Checks if the player and enemy are in the same location
    /// </summary>
    public bool isSameLocation() { 
        return playerLocation == enemyLocation;
    }

    public void ChangePlayerLocation(Location location) {
        playerLocation = location;
    }

    public void ChangeEnemyLocation(Location location) {
        enemyLocation = location;
    }

}
