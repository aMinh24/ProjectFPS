using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabMission : MonoBehaviour
{
    public Image img;
    public Text text;
    public GameObject border;
    public GameObject board;
    public Color[] colors;
    public void TurnOn()
    {
        img.color = colors[3];
        text.color = colors[1];
        border.SetActive(true);
        board.SetActive(true);
    }
    public void TurnOff()
    {
        img.color = colors[2];
        text.color = colors[0];
        border.SetActive(false);
        board.SetActive(false);
    }
}
