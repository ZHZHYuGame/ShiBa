using GameFramework;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SocketEnum
{
	Login,
	GameServer,
}
public class SocketConnectEventArgs : GameEventArgs
{
	public static readonly int EventId = typeof(SocketConnectEventArgs).GetHashCode();
	public override int Id
	{
		get
		{
			return EventId;
		}
	}
	public SocketEnum socketEnum
	{
	     get;
        private set;
	}

	public SocketConnectEventArgs()
	{
		socketEnum = SocketEnum.Login;
	}

	public override void Clear()
	{

	}

	public static SocketConnectEventArgs Create(SocketEnum socketEnum )
	{
		SocketConnectEventArgs openUIFormSuccessEventArgs = ReferencePool.Acquire<SocketConnectEventArgs>();
		openUIFormSuccessEventArgs.socketEnum = socketEnum;
		return openUIFormSuccessEventArgs;
	}

}
