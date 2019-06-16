using GravekeeperReboot.Source.Components;
using GravekeeperReboot.Source.Entities;
using GravekeeperReboot.Source.Extensions;
using Nez;
using System;


namespace GravekeeperReboot.Source.Commands {
	public class DropCommand : Command {
		private Entity grabber;
		private GrabComponent grabComponent;

		private TileEntity heldEntity;

		public DropCommand(TileEntity grabber) {
			if (!grabber.HasComponent<GrabComponent>())
				throw new ArgumentException("Grabber does not have a GrabComponent attached!");

			this.grabber = grabber;
			grabComponent = grabber.getComponent<GrabComponent>();
			heldEntity = grabComponent.target;
		}

		public override void Execute() {
			grabComponent.isGrabbing = false;
			grabComponent.target = null;
		}

		public override void Undo() {
			grabComponent.isGrabbing = true;
			grabComponent.target = heldEntity;
		}
	}
}
