using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Summoning : MonoBehaviour
{
    public float summonTime;
    public Image summonImg;
    public GameObject[] enemyType;

    private GameObject tempObj;
    private bool canSummon = false;
    private Sprite orgImg;

    private void Start()
    {
        orgImg = summonImg.sprite;
    }

    private void Update()
    {
        if (canSummon && Input.GetMouseButtonDown(1))
        {
            SummonCreature();
        }
    }

    public void SetSummonCreature(GameObject obj)
    {
        tempObj = obj; 
        summonImg.sprite = tempObj.GetComponent<SpriteRenderer>().sprite;
        StartCoroutine(SummonCreatureTime());
    }

    private void SummonCreature()
    {
        Debug.Log("Summoning " + tempObj.name);
        Instantiate(tempObj, transform.position + Vector3.right, tempObj.transform.rotation);

        canSummon = false;
        summonImg.sprite = orgImg;
        tempObj = null;

        StopAllCoroutines();
    }

    public IEnumerator SummonCreatureTime()
    {
        canSummon = true;
        yield return new WaitForSeconds(summonTime);
        tempObj = null;
        canSummon = false;
        summonImg.sprite = orgImg;
    }  



}
