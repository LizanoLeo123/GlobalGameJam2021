using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{
    public GameObject Vegetable;
    public float Speed;
    public int cant;
    List<GameObject> allVegetable = new List<GameObject>();
    private GameObject[] Areas;


    private void Start()
    {
        InvokeRepeating("Generate", 0, Speed);
        Areas = GameObject.FindGameObjectsWithTag("SpawnArea");
    }

    void Generate()
    {
        

        while (cant > 0)
        {
            int areaRand = Random.Range(0, Areas.Length);

            float posX = Areas[areaRand].transform.position.x;
            float posY = Areas[areaRand].transform.position.y;

            BoxCollider2D boxArea = Areas[areaRand].GetComponent<BoxCollider2D>();
            float extendX = boxArea.bounds.extents.x;
            float extendY = boxArea.bounds.extents.y;


            float rangeX1 = posX - extendX;
            float rangeX2 = posX + extendX;

            float rangeY1 = posY - extendY;
            float rangeY2 = posY + extendY;


            float x = Random.Range(rangeX1, rangeX2);
            float y = Random.Range(rangeY1, rangeY2);

            

            Vector2 Target = new Vector2(x, y);

            GameObject thisFood = Instantiate(Vegetable, Target, Quaternion.identity);
            allVegetable.Add(thisFood);

            cant -= 1;
        }
        
    }
}
