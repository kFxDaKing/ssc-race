using CitizenFX.Core;
using System.Drawing;
using System.Threading.Tasks;

namespace SSRC
{
    public class RaceStart
    {
        public Vector3 Position;
        public float Heading;

        public RaceStart(Vector3 p, float heading)
        {
            Position = p;
            Heading = heading;
        }

        public void Render()
        {
            Color col = Color.FromArgb(255, 255, 0, 0);
            World.DrawMarker(MarkerType.UpsideDownCone, Position, Vector3.Up, Vector3.Zero, Vector3.One, col);
        }
    }
}
