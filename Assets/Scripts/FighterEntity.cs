using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterEntity : MonoBehaviour
{
    public PersonState State { get; protected set; }
    public void SetState(PersonState state)
    {
        State = state;
    }
}
