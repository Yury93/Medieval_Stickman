using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterstateObject : MonoBehaviour
{
    public static InterstateObject instance;
    public const string ID_CHARACTERS = "ID_CHAR";
    public void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);

        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// 0 - ≈—À» —“» Ã¿Õ 1 ≈—À» —“» √®–À
    /// </summary>
    /// <returns></returns>
    public int GetStickmanId ()
    {
        return PlayerPrefs.GetInt(ID_CHARACTERS);
    }
}
