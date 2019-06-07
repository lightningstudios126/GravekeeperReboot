using GravekeeperReboot.Source.Components;
using GravekeeperReboot.Source.Extensions;
using GravekeeperReboot.Source.Utilities;
using Microsoft.Xna.Framework;
using Nez;
using System;

namespace GravekeeperReboot.Source.Commands {
	public class GrabCommand : Command {
		private Entity grabber;
		private GrabComponent grabComponent;

		public GrabCommand(Entity grabber) {
			if (!grabber.HasComponent<GrabComponent>())
				throw new ArgumentException("Grabber does not have a GrabComponent attached!");

			this.grabber = grabber;
			grabComponent = grabber.getComponent<GrabComponent>();
		}

		public override void Execute() {
			GameBoard gameboard = grabber.scene.getSceneComponent<GameBoard>();
			TileComponent tileComponent = grabber.getComponent<TileComponent>();

			Point checkPosition = tileComponent.tilePosition + Directions.DirectionPointOffset(tileComponent.tileDirection);
			Entity checkedEntity = gameboard.FindAtLocation(checkPosition);

			if (checkedEntity != null && checkedEntity.HasComponent<ControlComponent>()) {
				grabComponent.isGrabbing = true;
				grabComponent.target = checkedEntity;
			}
		}

		public override void Undo() {
			grabComponent.isGrabbing = false;
			grabComponent.target = null;
		}
	}
}
