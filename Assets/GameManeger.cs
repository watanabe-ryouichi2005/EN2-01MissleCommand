
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
public class GameManeger : MonoBehaviour
{
    [SerializeField, Header("Prefabs")]
    private Explosion explosionPrefab_;
   
    [SerializeField]
    private Meteor meteorPrefab_;
    [SerializeField]
    List<ItemBase> items_;
    [SerializeField, Header("ItemSettings")]
    private Transform itemSpawnPoint_;

    [SerializeField]

    private float itemSpawnInterval_ = 10;
    private float itemTimer_ = 0;
    [SerializeField, Header("MeteorSpawner")]
    private BoxCollider2D ground_;

    [SerializeField]
    private float meteorInterval_ = 1;
    private float meteorTimer_;
    private Camera mainCamera_;
  
    [SerializeField]
    private List<Transform> spawnPositions_;
    [SerializeField, Header("ScoreUISettings")]
    private ScoreText scoreText_;
    private int score_;
    [SerializeField, Header("LifeUISetting")]
    private LifeBar lifeBar_;
    [SerializeField]
    private float life_;
    private float maxLife_ = 10;

    



    // private List<item> items_;
    // Start is called before the first frame update
    private void Start()
    {
        //MainCameraというタグを検索
        GameObject mainCameraObject = GameObject.FindGameObjectWithTag("MainCamera");
#if UNITY_EDITOR
        //Null出ないことを確認
        Assert.IsNotNull(mainCameraObject, "MainCameraが見つかりませんでした");
        
        //Cameraコンポーネントが存在し,取得できる事を確認
        Assert.IsTrue(
            mainCameraObject.TryGetComponent(out mainCamera_),
            "MainCameraにCameraコンポーネントがありません");
        Assert.IsTrue(spawnPositions_.Count > 0, "spawnPositions_に要素がありません");
        foreach (Transform t in spawnPositions_)
        {
            Assert.IsNotNull(t, "spawnPositions_にNullが含まれています");

        }
#endif

        mainCameraObject.TryGetComponent(out mainCameraObject);
        ResetLife();
    }
    private void GenerateExplosion()
    {
        Vector3 clickPosition = mainCamera_.ScreenToWorldPoint(Input.mousePosition);
        clickPosition.z = 0;
        Explosion explosion = Instantiate(
            explosionPrefab_, clickPosition, Quaternion.identity);
    }
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) { GenerateExplosion();
            //Debug.Log("Mouse Click Detected");
        }
        UpdateMeteorTimer();
        UpdateItemTimer();


    }
    public void AddScore(int point)
    {
        score_ += point;
        scoreText_.SetScore(score_);

    }
    public void Damage(int point)
    {
        life_ -= point;
        UpdateLifeBar();
    }
    private void UpdateMeteorTimer()
    {
        meteorTimer_ -= Time.deltaTime;
        if (meteorTimer_ > 0) { return; }
        meteorTimer_ += meteorInterval_;
        GenerateMeteor();
    }
    private void GenerateMeteor()
    {
        int max = spawnPositions_.Count;

        int posIndex = Random.Range(0, max);
        Vector3 spawnPosition = spawnPositions_[posIndex].position;
        Meteor meteor = Instantiate(meteorPrefab_, spawnPosition, Quaternion.identity);
        meteor.Setup(ground_, this, explosionPrefab_);

    }
    private void ResetLife()
    {
        life_ = maxLife_;
        UpdateLifeBar();
    }
    private void UpdateLifeBar()
    {
        float lifeRatio = Mathf.Clamp01(life_ / maxLife_);
        lifeBar_.SetGaugeRatio(lifeRatio);

    }
    private ItemBase PickupItem()
    {
        int itemPrefabNum = items_.Count;
        Assert.IsTrue(itemPrefabNum > 0);
        int pickedupIndex = Random.Range(0, itemPrefabNum);
        ItemBase pickedupItem = items_[pickedupIndex];
        return pickedupItem;
     }
    private void UpdateItemTimer()
    {
        itemTimer_ -= Time.deltaTime;
        if (itemTimer_ > 0) { return; }
        itemTimer_ += itemSpawnInterval_;
        ItemBase pickedupItem = PickupItem();
        Instantiate(pickedupItem, itemSpawnPoint_.position, Quaternion.identity);

    }
}

