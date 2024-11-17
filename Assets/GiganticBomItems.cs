using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiganticBomItems : ItemBase
{
    //private List<ItemBase> item_;
    [SerializeField]
    Explosion giganticExplosionPrefab_;
    // Start is called before the first frame update
    //îöî≠ÇÇΩÇ≠Ç≥ÇÒê∂ê¨
    public override void Get()
    {
        Instantiate(giganticExplosionPrefab_, transform.position, Quaternion.
        identity);
        Destroy(gameObject);

        //throw new System.NotImplementedException();
    }
    
}
