using GravekeeperReboot.Source.ActionMapping;
using GravekeeperReboot.Source.Commands;
using GravekeeperReboot.Source.Components;
using Nez;
using System;

namespace GravekeeperReboot.Source.Systems {
	public class InputSystem : ProcessingSystem {
		private CommandSystem commandSystem;
		KeyBinding inputMap;

		private Entity player;
		private TileComponent playerTile;
		private GrabComponent grabComponent;

		GameBoard gameBoard;

		public InputSystem(KeyBinding inputMap) {
			this.inputMap = inputMap;
		}

		public event Action OnInput, OnPressUp, OnPressDown, OnPressLeft, OnPressRight, OnPressGrab, OnPressUndo;

		public override void process() {
			if (commandSystem == null) commandSystem = scene.getEntityProcessor<CommandSystem>();
			if (player == null) {
				player = scene.findEntity("Player");
				playerTile = player.getComponent<TileComponent>();
				grabComponent = player.getComponent<GrabComponent>();
				gameBoard = scene.getSceneComponent<GameBoard>();
			}

			if (Input.isKeyPressed(inputMap.UpButton)) {
				OnPressUp();
				OnInput();
			}
			if (Input.isKeyPressed(inputMap.DownButton)) {
				OnPressDown();
				OnInput();
			}
			if (Input.isKeyPressed(inputMap.LeftButton)) {
				OnPressLeft();
				OnInput();
			}
			if (Input.isKeyPressed(inputMap.RightButton)) {
				OnPressRight();
				OnInput();
			}

			// "Toggle" grab
			if (Input.isKeyPressed(inputMap.GrabButton)) {
				OnPressGrab();
				OnInput();
			}

			// "Hold" grab 
			//if (Input.isKeyPressed(inputMap.GrabButton)) Grab(true);
			//if (Input.isKeyReleased(inputMap.GrabButton)) Grab(false);

			if (Input.isKeyPressed(inputMap.UndoButton)) {
				OnPressUndo();
				OnInput();
			}
		}
	}
}
