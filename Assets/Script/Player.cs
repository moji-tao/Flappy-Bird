using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{

    public Rigidbody2D rigidbodyBird;

    public Animator anim;//操控动画

    public float force = 100f;//编辑器界面更改起飞力度

    private bool death = false;//死亡变量

    public delegate void DeathNotify();//委托

    public event DeathNotify OnDeath;//事件

    public UnityAction<int> OnScore;

    private Vector3 initPos;

    void Start()
    {
        initPos = transform.position;//记录游戏开始前角色的位置
        anim = GetComponent<Animator>();
        Idle();//游戏开始前进入Idle模式
    }

    public void Init()//角色初始化
    {
        transform.position = initPos;//角色归位
        death = false;//角色复活
        Idle();//进入Idle模式
    }

    void Update()
    {
        if (death)//角色死亡什么都不做
            return;

        if (Input.GetMouseButtonDown(0))//获取鼠标点击
        {
            rigidbodyBird.velocity = Vector2.zero;//受力前速度清零
            rigidbodyBird.AddForce(new Vector2(0, force), ForceMode2D.Force);//应用一个力到刚体
        }
    }

    public void Idle()
    {
        //进入游戏前刚体失效
        rigidbodyBird.simulated = false;

        anim.SetTrigger("Idle");//Idle动画开始
    }
    public void Fly()//飞行模式
    {
        //刚体激活
        rigidbodyBird.simulated = true;

        anim.SetTrigger("Fly");//Fly动画开始
    }

    public void Die()//角色死亡
    {
        death = true;
        if(OnDeath != null)
        {
            OnDeath();
        }
    }

    void OnCollisionEnter2D(Collision2D col)//触碰地面
    {
        Die();
    }
    void OnTriggerEnter2D(Collider2D col)//触碰管道
    {
        if(!col.gameObject.name.Equals("ScoreArea"))//如果角色碰到管道
        {
            Die();
        }   
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("ScoreArea"))//如果角色离开积分区
        {
            if(OnScore != null)
            {
                OnScore(1);//每穿越1根管道得1分
            }
        }
    }
}
