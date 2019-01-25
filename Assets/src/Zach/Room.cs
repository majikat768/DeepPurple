using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 
 * Abstract parent class for all rooms that will be created
 * Should not be instantiated by itself. A subclass of room will be attached
 * to each prefab room that is created and this class helps serve as
 * a generic placeholder for processing purposes
 * 
 **/
public abstract class Room : MonoBehaviour
{

    // Abstract Varriables
    public abstract RoomType Roomtype();
    public abstract string RoomName();

    // Abstract Methods
    public abstract bool IsRoomCompleted();

    public enum RoomType
    {
        START, COMBAT, BOSS
    };
}
