using GravekeeperReboot.Source.Components;
using GravekeeperReboot.Source.Entities;
using GravekeeperReboot.Source.Extensions;
using Microsoft.Xna.Framework;
using System;

namespace GravekeeperReboot.Source.Commands {
	public class GrabCommand : Command {
		private TileEntity grabber;
		private GrabComponent grabComponent;

		public GrabCommand(TileEntity grabber) {
			if (!grabber.HasComponent<GrabComponent>())
				throw new ArgumentException("Grabber does not have a GrabComponent attached!");

			this.grabber = grabber;
			grabComponent = grabber.getComponent<GrabComponent>();
		}

		public override void Execute() {
			GameBoard gameboard = this.grabber.scene.getSceneComponent<GameBoard>();

			Point checkPosition = grabber.tilePosition + grabber.tileDirection.Offset();
			TileEntity checkedEntity = gameboard.FindAtLocation(checkPosition);

			if (checkedEntity != null && checkedEntity.movability.HasFlag(MovabilityFlags.Grabbable)) {
				grabComponent.isGrabbing = true;
				grabComponent.target = checkedEntity;
				grabComponent.target.isBeingGrabbed = true;
			}
		}

		public override void Undo() {
			grabComponent.isGrabbing = false;
			grabComponent.target.isBeingGrabbed = false;
			grabComponent.target = null;
		}
	}
}
