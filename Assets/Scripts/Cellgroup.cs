using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Linq;

public class Cellgroup : MonoBehaviour
{
    [SerializeField] Color groupColor;
    [SerializeField] [Range(0, 2)] float speed;
    [SerializeField] protected List<Cell> Cells = new List<Cell>();
    [SerializeField] UnityEvent OnLastGroup;
    [SerializeField] UnityEvent OnEmpty;


    public Color GroupColor { get => groupColor; set => groupColor = value; }
    public float Speed { get => speed; set => speed = value; }

    public void AddNode(Cell cell)
    {
        Cells.Add(cell);
        var NotMineNodes = GameManager.Instance.AllCells.Where(n => n.Group != this).Count();
        if ( NotMineNodes == 1)
        {
            GameManager.Instance.WinThisGroup(this);
        }
    }
    public void RemoveNode(Cell cell)
    {
        Cells.Remove(cell);
        if (Cells.Count == 0)
        {
            OnEmpty.Invoke();
        }
    }
   
}
