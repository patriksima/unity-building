using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingDatabase : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> blocks = new List<GameObject>();

    private int index = 0;

    #region Singleton
    private static BuildingDatabase instance;
    public static BuildingDatabase Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("Instance je null. WTF");
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public GameObject Current()
    {
        return blocks[index];
    }

    public GameObject Next()
    {
        index++;

        if (index >= blocks.Count)
        {
            index = 0;
        }

        return blocks[index];
    }

    public GameObject Prev()
    {
        index--;

        if (index < 0)
        {
            index = blocks.Count - 1;
        }

        return blocks[index];
    }
}
