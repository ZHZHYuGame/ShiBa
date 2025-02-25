using GameFramework;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneEventArgs : GameEventArgs
{
	public static readonly int EventId = typeof(SocketConnectEventArgs).GetHashCode();
	public override int Id
	{
		get
		{
			return EventId;
		}
	}
	public int sceneId
	{
		get;
		private set;
	}
	public ChangeSceneEventArgs()
	{
		
	}

	public override void Clear()
	{

	}

	public static ChangeSceneEventArgs Create(int scneneId )
	{
        ChangeSceneEventArgs changeSceneEventArgs = ReferencePool.Acquire<ChangeSceneEventArgs>();
		changeSceneEventArgs.sceneId = scneneId;
		return changeSceneEventArgs;
	}

}
