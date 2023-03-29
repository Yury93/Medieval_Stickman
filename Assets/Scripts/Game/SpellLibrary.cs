using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellLibrarY", menuName = "SpellLibrary")]
public class SpellLibrary : ScriptableObject
{
    public List<StickmanSpell> stickmanSpells;
}
