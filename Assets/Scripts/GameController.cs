using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Sprite[] roadModel;

    [HideInInspector]
    public GameObject[] roads = new GameObject[5];

    public Vector3 roadStartPosition;
    public Vector3 roadEndPosition;

    bool isGameStarted=false;
    [Tooltip("Speed of the background. for player, speed of character.")]
    public float roadSpeed = 10;

    void Start()
    {
        RoadCreator();
        isGameStarted = true;
    }
    
    void Update()
    {
            RoadCreator();
    }

    //yol oluşturma işlemlerini gerçekleştiren metod.
    void RoadCreator()
    {
        //Eğer oyun henüz başlamamışsa kameraya göre yolları yerleştirir.
        if (!isGameStarted)
        {
            for (int i = 0; i < roads.Length; i++)
            {
                //gameobject'ten 5 adet yol oluşturma.
                roads[i] = new GameObject($"{i}. yol");
                //gameobjecte sprite renderer componenti ekleme.
                roads[i].AddComponent<SpriteRenderer>();
                //road için sprite seçimi. rastgele yol seçimi yapar.
                roads[i].GetComponent<SpriteRenderer>().sprite = roadModel[Random.Range(0,roadModel.Length)];

                if (i != 0)
                {
                    //ilk obje için başlangıç pozisyonu belirlenecek. Hangi pozisyona konduysa oradan başlanacak.
                    //kendinden bir önceki objenin genişliği kadar ileriye konumlandırılacak.
                    roads[i].transform.position = roads[i - 1].transform.position;
                    //kullandığım objenin yatay uzunluğu kadar pozisyonda öteleyip, uc uca eklemiş oldum.
                    roads[i].transform.position += roads[i].GetComponent<SpriteRenderer>().sprite.bounds.extents.x * 2 * Vector3.right;
                }
                else
                {   //seçilen başlangıç pozisyonuna yol yerleştirilir
                    roads[0].transform.position = roadStartPosition;
                }
                //
                //hareket etmeleri için gereken component ekleme
                roads[i].AddComponent<RoadMove>();
            }
        }
        else
        {
            //Her yol parçası için bir döngü
            //Bu döngü, sınırı geçmiş ve ekranda artık görülmeyen yol parçasını sona ekler.
            for (int i = 0; i < roads.Length; i++)
            {       
                //en baştaki yolun arkasına ekle
                if (roads[i].transform.position.x < roadEndPosition.x && i!=0)
                {
                    roads[i].transform.position = roads[i-1].transform.position;
                    roads[i].transform.position += roads[i-1].GetComponent<SpriteRenderer>().sprite.bounds.extents.x * 2 * Vector3.right;
                    //road için sprite seçimi. rastgele yol seçimi yapar.
                    roads[i].GetComponent<SpriteRenderer>().sprite = roadModel[Random.Range(0, roadModel.Length)];
                }
                //eğer 0. yol gelirse 4. yolun arkasına ekle
                else if (roads[i].transform.position.x < roadEndPosition.x)
                {
                    roads[i].transform.position = roads[4].transform.position;
                    roads[i].transform.position += roads[4].GetComponent<SpriteRenderer>().sprite.bounds.extents.x * 2 * Vector3.right;
                    //road için sprite seçimi. rastgele yol seçimi yapar.
                    roads[i].GetComponent<SpriteRenderer>().sprite = roadModel[Random.Range(0, roadModel.Length)];
                }
            }
        }
        
    }

}
