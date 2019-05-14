using GravekeeperReboot.Source.Components;
using GravekeeperReboot.Source.Extensions;
using Microsoft.Xna.Framework;
using Nez;
using System;

namespace GravekeeperReboot.Source.Commands {
	public class GrabCommand : Command {
		private Entity grabber;
		private GrabComponent grabComponent;

		public GrabCommand(Entity grabber) {
			if (!grabber.HasComponent<RotateComponent>())
				throw new ArgumentException("Target does not have a RotateComponent attached!");
			if (!grabber.HasComponent<GrabComponent>())
				throw new ArgumentException("Target does not have a GrabComponent attached!");

			this.grabber = grabber;
			grabComponent = grabber.getComponent<GrabComponent>();
		}

		public override void Execute() {
			if (!grabComponent.isGrabbing) {
				Point checkPosition = grabber.getComponent<MoveComponent>().position + Utilities.Direction.DirectionPointOffset(grabber.getComponent<RotateComponent>().direction);
				Entity checkedEntity = grabber.scene.getSceneComponent<GameBoard>().FindAtLocation(checkPosition);
				Console.WriteLine("Checking at: " + checkPosition);
				Graphics.instance.batcher.drawCircle(grabber.scene.getSceneComponent<GameBoard>().TileToWorldPosition(checkPosition), 4, Color.Blue);

				if (checkedEntity != null) {
					Console.WriteLine("Grabbing Entity: " + checkedEntity.name);
					grabComponent.isGrabbing = true;
					grabComponent.target = checkedEntity;
				}
			} else {
				Console.WriteLine("Releasing Entity: " + grabComponent.target.name);
				grabComponent.isGrabbing = false;
				grabComponent.target = null;
			}
		}

		public override void Undo() {

		}
	}
}
