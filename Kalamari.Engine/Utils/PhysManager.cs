using System.Numerics;
using System.Runtime.CompilerServices;
using Jitter2;
using Jitter2.Dynamics;
using Jitter2.LinearMath;

namespace Kalamari.Engine.Utils;

public static class PhysMgr
{
    public static bool IsReady = false;
    public static World physWorld;
    public static float PhysicsFPS = 60f;
    public static bool PhysEnabled = false;

    public static void InitWorld()
    {
        physWorld = new World();
        physWorld.SubstepCount = 4;
        IsReady = true;
    }

    public static void StepSim()
    {
        if (physWorld == null)
        {
            Console.WriteLine("[WARN] Tried to step PhysMgr sim without a sim ready! (Did you potentially forget to call InitWorld after calling DestroyWorld?)");
            return;
        }
        if (!PhysEnabled)
        {
            return;
        }

        //Console.WriteLine("Test 123");
        physWorld.Step(1f/PhysicsFPS, true);
    }
    
    public static Matrix4x4 GetRayLibTransformMatrix(RigidBody body)
    {
        JMatrix ori = JMatrix.CreateFromQuaternion(body.Orientation);
        JVector pos = body.Position;

        return new Matrix4x4(ori.M11, ori.M12, ori.M13, pos.X,
            ori.M21, ori.M22, ori.M23, pos.Y,
            ori.M31, ori.M32, ori.M33, pos.Z,
            0, 0, 0, 1.0f);
    }

    public static void DestroyWorld()
    {
        IsReady = false;
        physWorld.Dispose();
        physWorld = null;
    }
    public static RigidBody AddRigidBody(RigidBody body)
    {
        physWorld.RigidBodies.Append(body);
        return body;
    }

    public static void RemoveRigidBody(RigidBody body)
    {
        physWorld.Remove(body);
    }
}