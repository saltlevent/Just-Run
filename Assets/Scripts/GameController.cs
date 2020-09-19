using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Road Block Settings")]
    public Sprite[] roadModel;

    GameObject[] roads;
    [Tooltip("Number of background blocks")]
    public int roadCount=5;

    [Tooltip("Speed of the background. for player, speed of character.")]
    public float roadSpeed = 10;

    [Space]
    [Header("Positions")]
    public Vector3 roadStartPosition;
    public Vector3 roadEndPosition;

    
    bool isGameStarted=false;

    void Start()
    {

        //Seçilen adet kadar yol parçası dönecek arkaplanda
        roads = new GameObject[roadCount];

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

                //Eğer ilk elemanda değilse işlem yapılır 
                //çünkü ilk elemanı başlangıç noktasını belirleyecek, diğerleri de ona eklenerek devam edecek.
                if (i != 0)
                {
                    //ilk obje için başlangıç pozisyonu belirlenecek. Hangi pozisyona konduysa oradan başlanacak.
                    //kendinden bir önceki objenin genişliği kadar ileriye konumlandırılacak.
                    roads[i].transform.position = roads[i - 1].transform.position;
                    //kullandığım objenin yatay uzunluğu kadar pozisyonda öteleyip, uc uca eklemiş oldum.
                    roads[i].transform.position += roads[i].GetComponent<SpriteRenderer>().sprite.bounds.extents.x * 2 * Vector3.right;
                }
                else
                {   //seçilen başlangıç pozisyonuna ilk yol yerleştirilir
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
                //ayrı bir koşul olmasının sebebi (n-1) sonucunun negatif döndürmesinden kaçınmak için
                else if (roads[i].transform.position.x < roadEndPosition.x)
                {
                    roads[i].transform.position = roads[roads.Length-1].transform.position;
                    roads[i].transform.position += roads[roads.Length-1].GetComponent<SpriteRenderer>().sprite.bounds.extents.x * 2 * Vector3.right;
                    //road için sprite seçimi. rastgele yol seçimi yapar.
                    roads[i].GetComponent<SpriteRenderer>().sprite = roadModel[Random.Range(0, roadModel.Length)];
                }
            }
        }
        
    }

}
