using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MultiplayerManager : BaseManager<MultiplayerManager>
{
    public GameObject cam;
    public bool curTeam;
    public int AScore = 0;
    public int BScore = 0;
    [SerializeField]
    public Dictionary<string, int[]> teamA = new Dictionary<string, int[]>();
    [SerializeField]
    public Dictionary<string, int[]> teamB = new Dictionary<string, int[]>();
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
    public void updateScore(PlayerHealthMultiplayer health)
    {
        if (health.team)
        {
            teamA[health.gameObject.name][1]++;
            teamB[health.shooter][0]++;
            BScore++;
        }
        else
        {
            teamB[health.gameObject.name][1]++;
            teamA[health.shooter][0]++;
            AScore++;
        }

        int[] score = new int[2];
        if (curTeam)
        {
            score[0] = AScore;
            score[1] = BScore;
        }
        else
        {
            score[0] = BScore;
            score[1] = AScore;
        }
        if (ListenerManager.HasInstance())
        {
            ListenerManager.Instance.BroadCast(ListenType.ON_UPDATE_KDA, score);
        }
    }
    public void ChooseTeam(bool team)
    {

        if (team) //true = team A
        {
            GameObject player = PhotonNetwork.Instantiate("PlayerA", posA[curPosA].transform.position, Quaternion.identity);
            curPosA = (curPosA + 1) % posA.Length;
        }
        else
        {
            GameObject player = PhotonNetwork.Instantiate("PlayerB", posB[curPosB].transform.position, Quaternion.identity);
            curPosB = (curPosB + 1) % posB.Length;
        }
        curTeam = team;
        cam.SetActive(false);
    }
    public void JoinTeam(PlayerHealthMultiplayer player)
    {
        if (player.team)
        {
            int[] zero = { 0, 0 };
            teamA.Add(player.gameObject.name,zero);
        }
        else
        {
            int[] zero = { 0, 0 };
            teamB.Add(player.gameObject.name, zero);
        }
        Debug.Log(teamA.Last());
        Debug.Log(teamB.Last());
    }
}
