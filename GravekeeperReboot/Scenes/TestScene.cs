﻿using GravekeeperReboot.Source;
using GravekeeperReboot.Source.ActionMapping;
using GravekeeperReboot.Source.Components;
using GravekeeperReboot.Source.Systems;
using Nez;

namespace GravekeeperReboot.Scenes {
	class TestScene : Scene {
		GameBoard gameBoard;

		public override void initialize() {
			base.initialize();
			addRenderer(new DefaultRenderer());
		
			addEntityProcessor(new CommandSystem());
			addEntityProcessor(new InputSystem(new ArrowKeyBinding())); 
			addEntityProcessor(new MoveSystem(new Matcher().all(typeof(MoveComponent))));
			addEntityProcessor(new RotateSystem(new Matcher().all(typeof(RotateComponent))));
			gameBoard = addSceneComponent(new GameBoard());
		}

		public override void onStart() {
			base.onStart();
			gameBoard.LoadLevel(Content.Tilemaps.testmap1);
			camera.setPosition(gameBoard.Center);
			camera.zoomIn(10);
		}

		public override void update() {
			base.update();
		}
	}
}
