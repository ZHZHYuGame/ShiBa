using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Range(1, 10)]
    public int oneMapScale;
    Dictionary<int, Dictionary<int, GameObject>> allMaps = new Dictionary<int, Dictionary<int, GameObject>>();
    List<GameObject> mapPools = new List<GameObject>();
    public GameObject map;
    public void CreatMap(int x, int y)
    {
        List<GameObject> viewMap = new List<GameObject>();
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                viewMap.Add(CreatOMap(x + i, y + j));
            }
        }
        for (int i = 0; i < mapPools.Count; i++)
        {
            if (!viewMap.Contains(mapPools[i]))
            {
                mapPools[i].transform.localScale =Vector3.zero;
            }
        }
        mapPools.Clear();
        mapPools = viewMap;
        for (int i = 0; i < mapPools.Count; i++)
        {
            mapPools[i].transform.localScale = Vector3.one;
        }
    }

    private GameObject CreatOMap(int v1, int v2)
    {
        if (allMaps.ContainsKey(v1))
        {
            if (!allMaps[v1].ContainsKey(v2))
            {
                GameObject go = Instantiate(map, transform);
                go.transform.position = new Vector3(v1 * 10 * oneMapScale, 0, v2 * 10 * oneMapScale);
                go.GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV();
                allMaps[v1].Add(v2, go);
            }
        }
        else
        {
            GameObject go = Instantiate(map, transform);
            go.transform.position = new Vector3(v1 * 10 * oneMapScale, 0, v2 * 10 * oneMapScale);
            go.GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV();
            Dictionary<int, GameObject> v2map = new Dictionary<int, GameObject>();
            v2map.Add(v2, go);
            allMaps.Add(v1, v2map);
        }
        return allMaps[v1][v2];
    }
}

