using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    Queue<T> _pool = new Queue<T>();

    T _prefabObj;
    PoolManager _manager;

    public ObjectPool(T prefabObj, int initNum, PoolManager manager)
    {
        _prefabObj = prefabObj;
        _manager = manager;

        for (int i = 0; i < initNum; ++i)
        {
            T newObj = GameObject.Instantiate(_prefabObj);
            newObj.gameObject.SetActive(false);
            _pool.Enqueue(newObj);
            newObj.transform.SetParent(_manager.transform);
        }
    }

    public T CreateNewObj()
    {
        T newObj = GameObject.Instantiate(_prefabObj).GetComponent<T>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(_manager.transform);

        return newObj;
    }

    public T GetObj()
    {
        if (_pool.Count > 0)
        {
            T newObj = _pool.Dequeue();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);

            return newObj;
        }
        else
        {
            T newObj = CreateNewObj();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);

            return newObj;
        }
    }

    public void ReturnObj(T obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(_manager.transform);
        _pool.Enqueue(obj);
    }
}
