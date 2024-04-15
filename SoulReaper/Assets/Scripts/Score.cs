using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    public TextMeshProUGUI soulNumText;
    public float soulsCollected;

    private void Update()
    {
        soulNumText.text = soulsCollected.ToString();
    }
}
