
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200006B RID: 107
public class TabMission : MonoBehaviour
{
	public Toggle toggle;
	public bool isTrueToggle = false;
	// Token: 0x060001EE RID: 494 RVA: 0x0000ABEC File Offset: 0x00008DEC
	public void OnChangeToggleTab()
	{
		if (toggle.isOn)
		{
            TurnOn();
        }
		else
		{
            TurnOff();
        }
		//if (isTrueToggle)
		//{
		//	TurnOn();
		//}
		//else
		//{
		//	TurnOff();
		//}
	}
	public void TurnOn()
	{
		//this.img.color = this.colors[3];
		this.img.color = DataManager.Instance.ColorUI.imgActiveColor;
		//this.text.color = this.colors[1];
		this.text.color = DataManager.Instance.ColorUI.textActiveColor;
        this.border.SetActive(true);
		this.board.SetActive(true);
	}

	// Token: 0x060001EF RID: 495 RVA: 0x0000AC40 File Offset: 0x00008E40
	public void TurnOff()
	{
		//this.img.color = this.colors[2];
		//this.text.color = this.colors[0];
		img.color = DataManager.Instance.ColorUI.imgNormalColor;
		text.color = DataManager.Instance.ColorUI.textNormalColor;
		this.border.SetActive(false);
		this.board.SetActive(false);
	}

	// Token: 0x040001DA RID: 474
	public Image img;

	// Token: 0x040001DB RID: 475
	public Text text;

	// Token: 0x040001DC RID: 476
	public GameObject border;

	// Token: 0x040001DD RID: 477
	public GameObject board;


}
