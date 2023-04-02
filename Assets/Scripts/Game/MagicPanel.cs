using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MagicPanel : MonoBehaviour
{
    [SerializeField] private Stickman stickman;
    [SerializeField] private List<StickmanSpellProperty> stickmanSpells;
    [SerializeField] private Button buttonSpell0, buttonSpell1, buttonSpell2, buttonSpell3;
    [SerializeField] private TextMeshProUGUI description;
    public List<StickmanSpellProperty> SpellProperties => stickmanSpells;

    public void Awake()
    {
        stickman.SetCurrentSpell(stickmanSpells[0]);
        buttonSpell0.onClick.AddListener(() => stickman.SetCurrentSpell(stickmanSpells[0]));
        buttonSpell1.onClick.AddListener(() => stickman.SetCurrentSpell(stickmanSpells[1]));
        buttonSpell2.onClick.AddListener(() => stickman.SetCurrentSpell(stickmanSpells[2]));
        buttonSpell3.onClick.AddListener(() => stickman.SetCurrentSpell(stickmanSpells[3]));
    }
    public void SetActiveMagic1(bool result)
    {
        buttonSpell0.gameObject.SetActive(result);
    }
    public void SetActiveMagic2(bool result)
    {
        buttonSpell1.gameObject.SetActive(result);
        if(result == true)
        {
            description.enabled = true;
        }
    }
    public void SetActiveMagic3(bool result)
    {
        buttonSpell2.gameObject.SetActive(result);
    }
    public void SetActiveMagic4(bool result)
    {
        buttonSpell3.gameObject.SetActive(result);
    }
}
