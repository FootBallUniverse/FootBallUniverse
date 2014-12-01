using UnityEngine;
using System.Collections;

public class AIBase {
	public enum AI_POSITION
	{
		GK,
		DF,
		MF,
		FW,
		AI_POSITION_MAX
	};

	protected AI_POSITION position = AI_POSITION.AI_POSITION_MAX;

	public AIBase(){}
	public virtual void AIUpdate(){}
	public AI_POSITION GetPosition() { return this.position; }
}
