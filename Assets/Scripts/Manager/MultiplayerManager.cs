using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerManager : BaseManager<MultiplayerManager>
{
    public GameObject cam;
    public bool curTeam;
    public List<GameObject> teamA = new List<GameObject>();
    public List<GameObject> teamB = new List<GameObject>();
    public GameObject[] posA;
    public GameObject[] posB;
    public int curPosA;
    public int curPosB;
    private void Start()
    {
        posA = GameObject.FindGameObjectsWithTag("SpawnA");
        posB = GameObject.FindGameObjectsWithTag("SpawnB");
        if (UIManager.HasInstance())
        {
            UIManager.Instance.ShowOverlap<ChooseTeamBox>(null, true);
        }
    }
    public void ChooseTeam(bool team)
    {
        
        if(team) //true = team A
        {
            GameObject player = PhotonNetwork.Instantiate("PlayerA", posA[curPosA].transform.position, Quaternion.identity);
            curPosA = (curPosA+1)%posA.Length;
        }
        else
        {
            GameObject player = PhotonNetwork.Instantiate("PlayerB", posB[curPosB].transform.position, Quaternion.identity);
            curPosB = (curPosB + 1) % posB.Length;
        }
        curTeam = team;
        cam.SetActive(false);
    }
    public void JoinTeam(GameObject player, bool team)
    {
        if (team)
        {
            teamA.Add(player);
        }
        else
        {
            teamB.Add(player);
        }
    }
}
