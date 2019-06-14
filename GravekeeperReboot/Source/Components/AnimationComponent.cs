using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GravekeeperReboot.Source.Components {
	class AnimationComponent : Component {
		public delegate void Animation(float progress);
		public Animation animation;
	}
}
