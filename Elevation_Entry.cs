using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevation_Entry : MonoBehaviour
{
    public Collider2D[] mountainColliders;
    public Collider2D[] boundaryColliders;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") //检查是否是玩家进入这个碰撞体
        {
            foreach (Collider2D mountain in mountainColliders)
            {
                //遍历集合中的每一个项目；//Collider2D变量类型
                //对于每一个Collider2D碰撞体命名为mountain在mountainColliders这个集合中；
                mountain.enabled = false;
            }
            foreach (Collider2D boundary in boundaryColliders)
            {
                
                boundary.enabled = true;
            }

            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;
        }
    }


}
