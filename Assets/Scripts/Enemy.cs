using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Enemy : Cellgroup
{
  
    [SerializeField] float time;
    [SerializeField] Line linePrefab;
    [SerializeField] Transform canvas;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(StartAttack());
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSecondsRealtime(time);
        while (Cells.Count > 0)
        {
            List<Cell> enemies = GameManager.Instance.AllCells
                .Where(n => n.Group != this)
                .ToList();

            if (enemies.Count == 0) break;
            Cells = Cells.OrderBy(n => n.Count).Reverse().ToList();

            var max = Cells.First();

            Cell target = null;
            
                    target = enemies.Select(
                         n => new
                         {
                             target = n,
                             distance = Vector2.Distance(n.transform.position, max.transform.position)
                         }
                         ).Aggregate((a, b) => a.distance < b.distance ? a : b).target;

                

            if (target)
            {
                List<Cell> SelectedCells = new List<Cell>();
                foreach (var item in Cells)
                {
                    SelectedCells.Add(item);
                    if (SelectedCells.Sum(n => n.Count) > target.Count)
                    {
                        break;
                    }
                }

                foreach (var item in SelectedCells)
                {
                    if (item.Count == 0) continue;
                    var t = Instantiate(linePrefab, canvas);
                    var value = item.Count / 2;
                    item.Count -= value;


                    t.Set(item.transform, this, target, value);
                }
            }
            yield return new WaitForSecondsRealtime(time);
        }
    }

}