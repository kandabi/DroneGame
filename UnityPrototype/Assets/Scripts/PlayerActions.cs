using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InControl;

public class PlayerActions : PlayerActionSet
{
    public readonly PlayerAction takeoff;
    public readonly PlayerAction land;
	public readonly PlayerAction fire;

	public PlayerActions()
    {
        takeoff = CreatePlayerAction("Takeoff");
        land = CreatePlayerAction("Land");
		fire = CreatePlayerAction("Fire");

	}

	public static PlayerActions CreateWithDefaultBindings()
	{
		var playerActions = new PlayerActions();

		playerActions.takeoff.AddDefaultBinding(InputControlType.Action1);
		playerActions.land.AddDefaultBinding(InputControlType.Action2);
		playerActions.fire.AddDefaultBinding(InputControlType.Action3);

		playerActions.ListenOptions.IncludeUnknownControllers = true;
		playerActions.ListenOptions.MaxAllowedBindings = 4;
		playerActions.ListenOptions.UnsetDuplicateBindingsOnSet = true;

		return playerActions;
	}
}
