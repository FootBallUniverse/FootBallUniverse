using UnityEngine;
using System.Collections;

public class AI_GK : AIBase
{
	enum GK_State
	{
		STAY,
		ON_ALERT,
		PASS,
		GK_STATE_MAX
	};

	GK_State gkState = GK_State.STAY;

	public AI_GK()
	{
		this.position = AI_POSITION.GK;
	}

	public override void AIUpdate()
	{
		switch (this.gkState)
		{
			case GK_State.STAY:
				Stay();
				break;
			case GK_State.ON_ALERT:
				OnAlert();
				break;
			case GK_State.PASS:
				Pass();
				break;
		}
	}



	void Stay()
	{
		// 中心地へ移動（待機）

		// 敵監視
	}

	void OnAlert()
	{
	}

	void Pass()
	{
	}
}

// End of File