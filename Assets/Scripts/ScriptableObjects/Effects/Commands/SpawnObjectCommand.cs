using Effect;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnObjectCommand : ScriptableObject, IEffectCommand
{
	public abstract void DoCommand(IEffectContextHolder context);
}