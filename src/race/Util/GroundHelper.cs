using CitizenFX.Core;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace SSRC
{
    public static class GroundHelper
    {
        public static async Task<Vector3> PositionOnGround(Vector3 inP)
        {
            int startTime = GetGameTimer();
            Vector3 groundPosition = Vector3.Zero;
            bool searching = true;

            while (searching)
            {
                RequestCollisionAtCoord(inP.X, inP.Y, 0.0f);

                float groundZ = 0.0f;
                bool hasFoundGround = GetGroundZFor_3dCoord(inP.X, inP.Y, 1000.0f, ref groundZ, false);

                if (!hasFoundGround)
                {
                    if ((GetGameTimer() - startTime) > 5000)
                    {
                        ChatHelper.Print(nameof(GroundHelper), $"Failed to find ground for position X: {inP.X}, Y: {inP.Y}, Z: {inP.Z}");
                        searching = false;
                    }
                }
                else
                {
                    groundPosition = new Vector3(inP.X, inP.Y, groundZ);
                    searching = false;
                }

                await BaseScript.Delay(20);
            }

            return groundPosition;
        }
    }
}
