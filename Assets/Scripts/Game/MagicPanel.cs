using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicPanel : MonoBehaviour
{
    [SerializeField] private Stickman stickman;
    [SerializeField] private List<StickmanSpellProperty> stickmanSpells;
    [SerializeField] private Button buttonSpell0, buttonSpell1, buttonSpell2, buttonSpell3;
    public List<StickmanSpellProperty> SpellProperties => stickmanSpells;

    public void Awake()
    {
        stickman.SetCurrentSpell(stickmanSpells[0]);
        buttonSpell0.onClick.AddListener(() => stickman.SetCurrentSpell(stickmanSpells[0]));
        buttonSpell1.onClick.AddListener(() => stickman.SetCurrentSpell(stickmanSpells[1]));
        buttonSpell2.onClick.AddListener(() => stickman.SetCurrentSpell(stickmanSpells[2]));
        buttonSpell3.onClick.AddListener(() => stickman.SetCurrentSpell(stickmanSpells[3]));
    }
}
