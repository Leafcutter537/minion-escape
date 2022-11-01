using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitPairings : MonoBehaviour
{
    public string[] names;
    public Sprite[] sprites;

    public Sprite GetPortrait(string name)
    {
        for (int i = 0; i < names.Length; i++)
        {
            if (names[i] == name)
                return sprites[i];
        }
        return null;
    }
}
