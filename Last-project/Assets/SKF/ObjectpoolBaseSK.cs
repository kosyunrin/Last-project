using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SKFramework
{

    public abstract class ObjectpoolBaseSK : MonoBehaviour, IPooled<ObjectpoolBaseSK>
    {
        public int poolID { get; set; }
        public ObjectPoolsk<ObjectpoolBaseSK> pool { get; set; }

        public abstract void Shot();
    }
}
