
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
        //MainCamera�Ƃ����^�O������
        GameObject mainCameraObject = GameObject.FindGameObjectWithTag("MainCamera");
#if UNITY_EDITOR
        //Null�o�Ȃ����Ƃ��m�F
        Assert.IsNotNull(mainCameraObject, "MainCamera��������܂���ł���");
        
        //Camera�R���|�[�l���g�����݂�,�擾�ł��鎖���m�F
        Assert.IsTrue(
            mainCameraObject.TryGetComponent(out mainCamera_),
            "MainCamera��Camera�R���|�[�l���g������܂���");
        Assert.IsTrue(spawnPositions_.Count > 0, "spawnPositions_�ɗv�f������܂���");
        foreach (Transform t in spawnPositions_)
        {
            Assert.IsNotNull(t, "spawnPositions_��Null���܂܂�Ă��܂�");

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

