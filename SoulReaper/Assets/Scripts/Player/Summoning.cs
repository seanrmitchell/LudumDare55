using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Summoning : MonoBehaviour
{
    public Image summonImg;
    public GameObject[] enemyType;

    private GameObject tempObj;
    private bool canSummon = false;

    private void Update()
    {
        if (canSummon && Input.GetMouseButtonDown(1))
        {
            SummonCreature();
        }
    }

    public void SetSummonCreature(int layer)
    {
        foreach(GameObject enemy in enemyType)
        {
            if (enemy.layer == layer)
            {
                tempObj = enemy; 
                break;
            }
        }
        summonImg.sprite = tempObj.GetComponent<SpriteRenderer>().sprite;
        StartCoroutine(SummonCreatureTime());
    }

    private void SummonCreature()
    {
        Debug.Log("Summoning " + tempObj.name);
        Instantiate(tempObj, transform.position + Vector3.right, tempObj.transform.rotation);

        canSummon = false;
        summonImg.sprite = null;
        tempObj = null;

        StopAllCoroutines();
    }

    public IEnumerator SummonCreatureTime()
    {
        canSummon = true;
        yield return new WaitForSeconds(5f);
        canSummon = false;
        summonImg.sprite = null;
    }  



}
