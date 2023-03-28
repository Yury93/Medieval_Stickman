using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLibrary : MonoBehaviour
{
    public SpellLibrary spellLibrary;
    public  static GameLibrary instance;
    void Awake()
    {
        instance = this;
    }

 
}
