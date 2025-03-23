using System.Collections.Generic;
using Akasha;

//소재철 튜터님 프레임워크 스크립트
public class PoolManager : Manager<PoolManager>
{
    Dictionary<string, Queue<BasePool>> pools = new Dictionary<string, Queue<BasePool>>();

    public T Spawn<T>(string id, BasePool prefab) where T : BasePool
    {
        if (pools[id].Count == 0)
        {
            for (int i = 0; i < 25; i++)
            {
                var obj = Instantiate(prefab, transform);
                pools[id].Enqueue(obj);
            }
        }
        var retObj = (T)pools[id].Dequeue();
        retObj.gameObject.SetActive(true);
        return retObj;
    }

    public void Release(BasePool item)
    {
        item.gameObject.SetActive(false);
        item.transform.parent = transform;
        pools[item.name].Enqueue(item);
    }

    protected override void OnActivate()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnDeactivate()
    {
        throw new System.NotImplementedException();
    }
}
