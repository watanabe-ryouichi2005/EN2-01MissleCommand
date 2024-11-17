using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterBomItem :ItemBase
{
    [SerializeField]
    private Explosion explosionPrefab_;
    //æ“¾‚µ‚½”š”­ó‘Ô‚©‚Ç‚¤‚©H
    bool IsGet = false;
    //”š”­‚µ‘±‚¯‚éŠÔ
    private float explosionEmmitiontimer_ = 3;
    //×‚©‚È”š”­‚ğ¶¬‚·‚éƒEƒ“ƒ`
    private float explosionInterval_ = 0.2f;
    private float explosionTimar_ = 0.0f;
    Renderer renderer_;

    // Start is called before the first frame update
    public override void Get()
    {
    if(TryGetComponent(out renderer_))
        {
            renderer_.enabled = false;

        }   
    collider_.enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        IsGet = true;
    }
    protected override void Update()
    {
        if (!IsGet)
        {
            base.Update();
            return;
        }
        explosionTimar_ -= Time.deltaTime;
        if(explosionEmmitiontimer_ <= 0 ) {Destroy(gameObject); }
        UpdateClusterExplosion();
    }
    private void UpdateClusterExplosion()
    {
        explosionEmmitiontimer_ -= Time.deltaTime;
        if(explosionTimar_ > 0 ) { return; }
        float randamWidth = 2;
        Vector3 offset = new Vector3(Random.Range(-randamWidth, randamWidth),
            Random.Range(-randamWidth, randamWidth), 0);
Instantiate(explosionPrefab_,transform.position +  offset, Quaternion.identity);
        explosionTimar_ += explosionInterval_;

    }
}

