using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//管道移动
public class Pipeline : MonoBehaviour
{

    public float speed;//在Unity中设置speed

    void Start()
    {
        Init();
    }

    float t = 0;

    public void Init()
    {
        float y = Random.Range(-2, 4);//设置一个随机值y
        transform.localPosition = new Vector3(2, y, 0);//使得管道高度不同
    }

    void Update()
    {
        transform.position += new Vector3(-speed, 0) * Time.deltaTime;
        t += Time.deltaTime;
        if(t > 6f)//重置(初始化)管道
        {
            t = 0;
            Init();
        }
    }
}
