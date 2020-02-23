using CitizenFX.Core;
using System.Drawing;
using System.Threading.Tasks;

namespace SSRC
{
    public class RaceCheckpoint
    {
        public Vector3 Position;

        private Vector3 GroundPosition = Vector3.Zero;

        public RaceCheckpoint(Vector3 p)
        {
            Position = p;
        }

        public async Task Render()
        {
            if (GroundPosition == Vector3.Zero)
            {
                GroundPosition = await GroundHelper.PositionOnGround(Position); 
            }

            Color col = Color.FromArgb(255, 250, 220, 94);
            World.DrawMarker(MarkerType.VerticalCylinder, GroundPosition, Vector3.Up, Vector3.Zero, 5.0f * Vector3.One, col);
        }
    }
}
