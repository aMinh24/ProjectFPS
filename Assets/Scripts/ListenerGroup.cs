using System;
using System.Collections.Generic;

// Token: 0x02000045 RID: 69
public class ListenerGroup
{
	// Token: 0x060000E9 RID: 233 RVA: 0x00006560 File Offset: 0x00004760
	public void BroadCast(object value)
	{
		foreach (Action<object> action in this.actions)
		{
			action(value);
		}
	}

	// Token: 0x060000EA RID: 234 RVA: 0x000065B4 File Offset: 0x000047B4
	public void Attach(Action<object> action)
	{
		using (List<Action<object>>.Enumerator enumerator = this.actions.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Equals(action))
				{
					return;
				}
			}
		}
		this.actions.Add(action);
	}

	// Token: 0x060000EB RID: 235 RVA: 0x00006618 File Offset: 0x00004818
	public void Detach(Action<object> action)
	{
		using (List<Action<object>>.Enumerator enumerator = this.actions.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Equals(action))
				{
					this.actions.Remove(action);
					break;
				}
			}
		}
	}

	// Token: 0x04000133 RID: 307
	private List<Action<object>> actions = new List<Action<object>>();
}
