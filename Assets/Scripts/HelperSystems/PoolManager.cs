using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : CustomSingleton<PoolManager>
{
    public List<Pooler> poolerList;

    public Pooler GetPoolerByItemLevel(int level)
    {
        return poolerList[level];
    }
}
