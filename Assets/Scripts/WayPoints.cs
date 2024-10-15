using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public static Transform[] points;

    private void Awake()
    {
        points = new Transform[transform.childCount]; // [] içerisinde size belirttik.
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
}
