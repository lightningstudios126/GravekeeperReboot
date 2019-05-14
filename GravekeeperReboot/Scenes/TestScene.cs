﻿using GravekeeperReboot.Source;
using GravekeeperReboot.Source.Components;
using GravekeeperReboot.Source.Entities;
using GravekeeperReboot.Source.Systems;
using Microsoft.Xna.Framework;
using Nez;

namespace GravekeeperReboot.Scenes {
	class TestScene : Scene {
		GameBoard gameBoard;

		public override void initialize() {
			base.initialize();
			addRenderer(new DefaultRenderer());
		
			addEntityProcessor(new CommandSystem());
			addEntityProcessor(new InputSystem(this)); 
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
