using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bow : MonoBehaviour
{
    public Transform LaunchPoint;
    public GameObject arrowPrefab;
    private Vector2 aimDirection = Vector2.right;

    public NewBehaviourScript playermovement;

    public Animator anim;


    public float shootcooldown = .5f;
    private float shootTimer;
    // Update is called once per frame
    void Update()
    {
        shootTimer -= Time.deltaTime;

        HandleAiming();

        if (Input.GetButtonDown("Shoot")&& shootTimer <= 0)
        {
            playermovement.isShooting = true;
            anim.SetBool("isShooting", true);

        }
    }

    private void OnEnable()
    {
        anim.SetLayerWeight(0, 0);
        anim.SetLayerWeight(1, 1);
    }

    private void OnDisable()
    {
        anim.SetLayerWeight(0, 1);
        anim.SetLayerWeight(1, 0);
    }


    private void HandleAiming()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if(horizontal != 0 || vertical != 0)
        {
            aimDirection = new Vector2(horizontal, vertical).normalized;
            anim.SetFloat("aimX", aimDirection.x);
            anim.SetFloat("aimY", aimDirection.y);
        }
    }


    public void Shoot()
    {
        if (shootTimer <= 0)
        {
            arrow arrow = Instantiate(arrowPrefab, LaunchPoint.position, Quaternion.identity).GetComponent<arrow>();//instantiate动态生成预制体的核心函数
                                                                                                                    //要生成哪个物体，在哪个坐标位置生成，生成出来的物体旋转角度Quaternion.identity零旋转；
            arrow.direction = aimDirection;
            shootTimer = shootcooldown;
        }
            anim.SetBool("isShooting", false);
        playermovement.isShooting = false;

    }
}
