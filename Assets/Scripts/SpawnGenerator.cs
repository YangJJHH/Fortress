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
        area = GetComponent<BoxCollider>();
        for (int i=0; i<count; i++)
        {
            //생성함수
            Spawn();
        }

        //prop생성후 Box콜라이더 끄기
        area.enabled= false;
    }

    void Spawn()
    {
        int selection = Random.Range(0,propPrefabs.Length);
        GameObject selectedPrefab = propPrefabs[selection];
        GameObject instance =Instantiate(selectedPrefab, GetRandomPosition(), Quaternion.identity);
        props.Add(instance);
    }

    Vector3 GetRandomPosition()
    {
        Vector3 basePos = transform.position;
        Vector3 size = area.size;
        float posX = basePos.x + Random.Range(-(size.x / 2f), (size.x / 2f));
        float posY = basePos.y + Random.Range(-(size.y / 2f), (size.y / 2f));
        float posZ = basePos.z + Random.Range(-(size.z / 2f), (size.z / 2f));

        return new Vector3(posX,posY,posZ);
    }

    public void ResetProp()
    {
        foreach (GameObject prop in props)
        {
            prop.transform.position = GetRandomPosition();
            prop.SetActive(true);
        }
    }
}
