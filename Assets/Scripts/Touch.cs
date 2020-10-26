using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class Touch : Singletone<Touch>, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] List<Cell> cells = new List<Cell>();
    [SerializeField] Transform cursor;
    [SerializeField] Cellgroup group;
    [SerializeField] bool isTouch;
    [SerializeField] Line linePrefab;

    public UnityEvent OnComplite;
    public UnityEvent OnCreateBranch;
    public Transform canvas;
    public Cell SelectCell;

    public List<Cell> Cells { get => cells; set => cells = value; }
    public Cellgroup Group { get => group; set => group = value; }
    public bool IsToucher { get => isTouch; set => isTouch= value; }
    public Vector3 CursorPosition => cursor.position;


    public bool AddNode(Cell cell)
    {
        if (cells.Contains(cell)) return false;
        cells.Add(cell);
        return true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        cursor.position = eventData.pointerCurrentRaycast.screenPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isTouch = true;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Complite();
    }

    public void Complite()
    {
        if (SelectCell)
        {
            OnCreateBranch.Invoke();
            CreateBranchs();
        }
        Cells.Clear();
        OnComplite.Invoke();
        isTouch = false;
        OnComplite.RemoveAllListeners();
    }

    private void CreateBranchs()
    {
        foreach (var item in cells)
        {
            if (item.Count == 0) continue;
            if (item == SelectCell) continue;
            var t = Instantiate(linePrefab, canvas);
            var value = item.Count / 2;
            item.Count -= value;


            t.Set(item.transform, Group, SelectCell, value);
        }
    }
}
