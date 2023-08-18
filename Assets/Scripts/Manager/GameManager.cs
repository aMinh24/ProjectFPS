
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200004E RID: 78
public class GameManager : BaseManager<GameManager>
{
	// Token: 0x06000118 RID: 280 RVA: 0x0000709D File Offset: 0x0000529D
	private new void Awake()
	{
		Application.targetFrameRate = 80;
	}

	// Token: 0x06000119 RID: 281 RVA: 0x000070A6 File Offset: 0x000052A6
	private void Start()
	{
		Debug.Log("StartGame");
		if (BaseManager<UIManager>.HasInstance())
		{
			BaseManager<UIManager>.Instance.ShowScreen<MainMenu>(null, true);
		}
	}

	// Token: 0x0600011A RID: 282 RVA: 0x000070C5 File Offset: 0x000052C5
	private void Update()
	{
	}

	// Token: 0x0600011B RID: 283 RVA: 0x000070C7 File Offset: 0x000052C7
	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
}
