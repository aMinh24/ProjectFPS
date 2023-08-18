
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000068 RID: 104
public class KillDisplay : MonoBehaviour
{
	// Token: 0x060001DF RID: 479 RVA: 0x0000AA7C File Offset: 0x00008C7C
	public void OnEnemyKill(string killer, string dead)
	{
		this.allyName.text = dead;
		this.enemyName.text = killer;
		this.allyName.gameObject.transform.SetAsLastSibling();
		this.enemyName.gameObject.transform.SetAsFirstSibling();
	}

	// Token: 0x060001E0 RID: 480 RVA: 0x0000AACC File Offset: 0x00008CCC
	public void OnAllyKill(string killer, string dead)
	{
		this.allyName.text = killer;
		this.enemyName.text = dead;
		this.allyName.gameObject.transform.SetAsFirstSibling();
		this.enemyName.gameObject.transform.SetAsLastSibling();
	}

	// Token: 0x040001D7 RID: 471
	public Text allyName;

	// Token: 0x040001D8 RID: 472
	public Text enemyName;

	// Token: 0x040001D9 RID: 473
	public int orderNum;
}
