using System.Collections;
using System.Collections.Generic;

using DG.Tweening;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] Transform from;
    [SerializeField] Cellgroup group;
    [SerializeField] Transform visual;
    [SerializeField] Cell to;
    [SerializeField] int count;
    [SerializeField] [Range(0, 5)] float speed;

    float time;

    public Transform From { get => from; set => from = value; }
    public Cell To { get => to; set => to = value; }
    public int Count { get => count; set => count = value; }

    // Start is called before the first frame update
    void Start()
    {
        var posFrom = FromScreenToWorld(from.transform.position);
        var posTo = FromScreenToWorld(to.transform.position);
        time = Vector2.Distance(posFrom, posTo) / speed;
        StartCoroutine(Translate(time));
        visual.position = from.position;
        visual.DOMove(to.transform.position, time);
        
    }

    public void Set(Transform from, Cellgroup group, Cell to, int count)
    {
        this.from = from;
        this.group = group;
        this.to = to;
        this.count = count;
    }

    IEnumerator Translate(float time)
    {
        yield return new WaitForSeconds(time);
        To.Add(Count, group);
        Destroy(gameObject);
    }
    public Vector3 FromScreenToWorld(Vector3 pos)
    {
        var t = Camera.main.ScreenToWorldPoint(pos);
        t.z = 0;
        return t;
    }
}
