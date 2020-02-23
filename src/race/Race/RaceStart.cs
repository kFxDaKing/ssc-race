using CitizenFX.Core;
using System.Drawing;
using System.Threading.Tasks;

namespace SSRC
{
    public class RaceStart
    {
        public Vector3 Position;
        public float Heading;

        private Vector3 GroundPosition = Vector3.Zero;

        public RaceStart(Vector3 p, float heading)
        {
            Position = p;
            Heading = heading;
        }

        public async Task Render()
        {
            if (GroundPosition == Vector3.Zero)
            {
                GroundPosition = await GroundHelper.PositionOnGround(Position);
            }

            Color col = Color.FromArgb(255, 250, 220, 94);
            World.DrawMarker(MarkerType.UpsideDownCone, GroundPosition, Vector3.Up, Vector3.Zero, Vector3.One, col);
        }
    }
}
