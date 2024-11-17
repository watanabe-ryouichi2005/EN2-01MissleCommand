using System.Collections;
using System.Collections.Generic;
using UnityEngine;

   [ RequireComponent(typeof(Rigidbody2D))]
public class Meteor : MonoBehaviour
{
    // Start is called before the first frame update
        [SerializeField] private float fallspeedMin_ = 1;
        [SerializeField] private float fallspeedMax_ = 3;
    private Explosion explosionPrefab_;
    private BoxCollider2D groundCollider_;
    private Rigidbody2D rb_;
    private GameManeger gameManeger_;
    private void Start(){
        rb_ = GetComponent<Rigidbody2D>();
        SetUpVelosity();
    }
    public void Setup(BoxCollider2D ground, GameManeger gameManeger,Explosion explosionPrefab) {
        gameManeger_ = gameManeger;
        groundCollider_ = ground;
        explosionPrefab_ = explosionPrefab;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void SetUpVelosity() {
        float left = groundCollider_.bounds.center.x - groundCollider_.bounds.size.x / 2;
        float right = groundCollider_.bounds.center.x + groundCollider_.bounds.size.x / 2;
        float top = groundCollider_.bounds.center.y + groundCollider_.bounds.size.y / 2;
        float bottom = groundCollider_.bounds.center.y - groundCollider_.bounds.size.y / 2;
        float targetX = Mathf.Lerp(left, right, Random.Range(0.0f, 1.0f));
        Vector3 target = new Vector3(targetX, top, 0);
        Vector3 direction = (target - transform.position).normalized;
        float fallSpeed = Random.Range(fallspeedMin_, fallspeedMax_);
        rb_.velocity = direction * fallSpeed;
           
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       //Explosion explosion;

        if (collision.gameObject.CompareTag("Explosion"))
        {
            Explosion();
            
            
        }
       else if (collision.gameObject.CompareTag("Ground"))
        {
            Fall();
        }

        
    }

    private void Explosion()
    {
        gameManeger_.AddScore(100);
        Instantiate(explosionPrefab_, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void Fall()
    {
        gameManeger_.Damage(1);
        Destroy(gameObject); 

    }
}
