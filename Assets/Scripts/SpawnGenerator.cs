using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGenerator : MonoBehaviour
{
    #region field
    [SerializeField]
    GameObject[] propPrefabs;
    [SerializeField]
    int count = 100;
    BoxCollider area;

    List<GameObject> props = new List<GameObject>();
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
