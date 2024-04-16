using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    public TextMeshProUGUI soulNumText;
    public TextMeshProUGUI finalSoulNumText;
    public float soulsCollected;
    public Health playerAlive;

    private void Update()
    {
        soulNumText.text = soulsCollected.ToString();

        if(!playerAlive.isAlive)
        {
            finalSoulNumText.text = soulNumText.text;
        }
    }
}
