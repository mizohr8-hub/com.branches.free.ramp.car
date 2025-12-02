using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Blek Game.
/// </summary>
namespace TrickGameManager
{

	public enum GameState
	{
		NotStarted,
		PlayState,
		CompletedState,
		GameOver

	}

	/// <summary>
	/// Game manager.
	/// </summary>
	public class GameManagerBouncy
	{
		private static GameManagerBouncy gmInstance;

		private GameState currentGameState;
		//the current game state

		private int currentLevel;
		//current level of the game


		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
		public static GameManagerBouncy instance {
			get {
				if (gmInstance == null)
					gmInstance = new GameManagerBouncy ();
				return gmInstance;
			}
		}

		private GameManagerBouncy ()
		{
			//initalize
			CurrentGameState = GameState.NotStarted;
			CurrentLevel = 0;
		}

		/// <summary>
		/// Gets or sets the state of the current game.
		/// </summary>
		/// <value>The state of the current game.</value>
		public GameState CurrentGameState {
			get {
				return currentGameState;
			}
			set {
				currentGameState = value;
			}
		}

		/// <summary>
		/// Gets or sets the current level.
		/// </summary>
		/// <value>The current level.</value>
		public int CurrentLevel {
			get {
				return currentLevel;
			}
			set {
				currentLevel = value;
			}
		}

	}

	/// <summary>
	/// Constants.
	/// </summary>
	public class Constants
	{
		public const string GameControllerTag = "GameController";
		public const string EndTag = "End";
		public const string ColorTag = "Color";
		public const float RestartWaitTime = 0.75f;
		public static Vector3 DefaultPlayerPosition = new Vector3 (100f, 100f, 0f);
		public const float DefaultTrailTime = 0.75f;
		public const float MoveDelay = 0.01f;
		public const string LevelText = "Level ";
	}

}