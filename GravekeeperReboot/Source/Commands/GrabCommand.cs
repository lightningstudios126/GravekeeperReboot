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

		public GrabCommand(Entity grabber, bool grab) {
			this.playerInitiated = false;

			if (!grabber.HasComponent<GrabComponent>())
				throw new ArgumentException("Grabber does not have a GrabComponent attached!");

			this.grabber = grabber;
			grabComponent = grabber.getComponent<GrabComponent>();
			grabComponent.isGrabbing = !grab;
		}

		public override void Execute() {
			if (!grabComponent.isGrabbing) {
				GameBoard gameboard = grabber.scene.getSceneComponent<GameBoard>();
				TileComponent tileComponent = grabber.getComponent<TileComponent>();

				Point checkPosition = tileComponent.tilePosition + Directions.DirectionPointOffset(tileComponent.tileDirection);
				Entity checkedEntity = gameboard.FindAtLocation(checkPosition);

				if (checkedEntity != null && checkedEntity.HasComponent<ControlComponent>()) {
					grabComponent.isGrabbing = true;
					grabComponent.target = checkedEntity;
				}
			} else {
				grabComponent.isGrabbing = false;
				grabComponent.target = null;
			}
		}

		public override void Undo() { }
	}
}
