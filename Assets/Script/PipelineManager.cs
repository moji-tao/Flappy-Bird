using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipelineManager : MonoBehaviour
{
    public GameObject pipe;

    List<Pipeline> pipelines = new List<Pipeline>();

    Coroutine runner = null;

    public void Init()
    {
        for (int i = 0; i < pipelines.Count; ++i)
        {
            Destroy(pipelines[i].gameObject);
        }
        pipelines.Clear();
    }

    public void StartRun()
    {
        runner = StartCoroutine(GeneratePipelines());
    }

    public void Stop()//玩家死亡管道停止
    {
        StopCoroutine(runner);
        for(int i=0;i<pipelines.Count;++i)
        {
            pipelines[i].enabled = false;
        }
    }

    IEnumerator GeneratePipelines()
    {
        for (int i = 0; i < 3; ++i) 
        {
            if (pipelines.Count < 3)
            { 
                CreatePipeline(); 
            }
            else
            {
                pipelines[i].enabled = true;
                pipelines[i].Init();
            }

            yield return new WaitForSeconds(2f);//每2秒生成一个管道
        }
    }

    void CreatePipeline()
    {
        if (pipelines.Count < 3)
        {
            //在当前位置生成管道
            GameObject obj = Instantiate(pipe, transform);
            pipelines.Add(obj.GetComponent<Pipeline>());
        }
    }
}
