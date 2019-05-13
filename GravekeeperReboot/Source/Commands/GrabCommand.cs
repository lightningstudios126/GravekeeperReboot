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
			if (grabber.getComponent<RotateComponent>() == null)
				throw new ArgumentException("Target does not have a RotateComponent attached!");
			if (grabber.getComponent<GrabComponent>() == null)
				throw new ArgumentException("Target does not have a GrabComponent attached!");

			this.grabber = grabber;
			grabComponent = grabber.getComponent<GrabComponent>();
		}

		public override void Execute() {
			if (!grabComponent.isGrabbing) {
				Vector2 checkPosition = grabber.position + grabber.getComponent<RotateComponent>().Direction * TiledMapConstants.TileSize;

				Collider collider = Physics.overlapCircle(checkPosition, 0.1f);
				Console.WriteLine("Checking at: " + grabber.getComponent<RotateComponent>().Direction * TiledMapConstants.TileSize);

				if (collider != null) {
					Console.WriteLine("Grabbing Entity: " + collider.entity.name);
					grabComponent.isGrabbing = true;
					grabComponent.target = collider.entity;
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
