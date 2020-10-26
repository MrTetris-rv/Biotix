using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Advertisements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : Singletone<GameManager>
{
    [SerializeField] UnityEvent OnWin;
    [SerializeField] UnityEvent OnLose;
    [SerializeField] Cellgroup player;
    [SerializeField] List<Cell> _AllCells;
    
    public static int i = 1;
    public void NextLevel()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("video");
        }
        i++;
        SceneManager.LoadScene(i);
    }


public List<Cell> AllCells { get => _AllCells; set => _AllCells = value; }

    public void Win()
    {
        OnWin.Invoke();
        NextLevel();
    }

    public void WinThisGroup(Cellgroup Cellgroup)
    {
        if (Cellgroup == player)
        {
            Win();
        }
        else
        {
            Lose();
        }
    }

    public void Lose()
    {
        OnLose.Invoke();
    }

    void Start()
    {
        AllCells = FindObjectsOfType<Cell>().ToList();

        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("3878559", false);
        }
    }
}
