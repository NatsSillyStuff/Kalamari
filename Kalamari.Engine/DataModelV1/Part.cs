using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using BepuPhysics;
using Jitter2.Collision.Shapes;
using Jitter2.Dynamics;
using Jitter2.Unmanaged;
using Kalamari.Engine.Utils;
using Raylib_cs;

namespace Kalamari.Engine.DataModelV1;

public class Part : Instance
{
    public Vector3 Position
    {
        get;
        set
        {
            bodyHandle.Position = value;
        }
    }

    public Vector3 Rotation
    {
        get;
        set
        {
            bodyHandle.Orientation = Quaternion.CreateFromYawPitchRoll(value.X, value.Y, value.Z);
        }
    }

    public Vector3 Scale
    {
        get;
        set
        {
            field = value;
            idk = Raylib.LoadModelFromMesh(Raylib.GenMeshCube(value.X, value.Y, value.Z));
            if (Anchored)
            {
                return;
            }

            if (bodyHandle == null)
            {
                return;
            }
            BoxShape shape = new BoxShape(Scale);
            bodyHandle.RemoveShape(bodyHandle.Shapes[0]);
            bodyHandle.AddShape(shape);
        }
        
    }
    private RigidBody bodyHandle;

    public bool Anchored
    {
        get;
        set
        {
            if (bodyHandle == null)
            {
                return;
            }
            if (value)
            {
                bodyHandle.MotionType = MotionType.Static;
            }
            else
            {
                bodyHandle.MotionType = MotionType.Dynamic;
            }
            
        }
    }
    public Model idk;
    
    private Material mat = Raylib.LoadMaterialDefault();

    public Raylib_cs.Color PartColor
    {
        get;
        set
        {
            unsafe
            {
                mat.Maps[0].Color = value;
            }
        }
    }
    public Part(string name, bool anchored = false) : base(name)
    {
        idk = Raylib.LoadModelFromMesh(Raylib.GenMeshCube(Scale.X, Scale.Y, Scale.Z));
        idk.Transform = Raymath.MatrixCompose(Position, Quaternion.CreateFromYawPitchRoll(Rotation.X, Rotation.Y, Rotation.Z), Scale);
        Scale = Vector3.One;
        if (Scale == Vector3.Zero)
        {
            throw new ArgumentException("Scale must not be zero.");
        }
        if (Scale == null)
        {
            throw new ArgumentException("WTF?? Scale is null!");
        }
        Anchored = anchored;
        if (bodyHandle == null)
        {
            bodyHandle = PhysMgr.physWorld.CreateRigidBody();
            if (Anchored)
            {
                bodyHandle.MotionType = MotionType.Static;
            }
            else
            {
                bodyHandle.MotionType = MotionType.Dynamic;
            }
            BoxShape shape = new BoxShape(Scale);
            bodyHandle.AddShape(shape);
            bodyHandle.Position = Position;
            bodyHandle.Orientation = Quaternion.CreateFromYawPitchRoll(Rotation.X, Rotation.Y, Rotation.Z);
            PhysMgr.AddRigidBody(bodyHandle);
            bodyHandle.SetActivationState(true);
        }
        else
        {
            Console.WriteLine("bodyHandle is null! Assuming physics init failed.");
        }
        
    }
    public override void Render()
    {
        
        //Position = bodyHandle.Position;
        //Console.WriteLine(Name + " pos: " + Position);
        Rotation = bodyHandle.Orientation.Vector;
        //Console.WriteLine(Name + " rot: " + bodyHandle.Orientation);
        
        Raylib.DrawMesh(Raylib.GenMeshCube(Scale.X, Scale.Y, Scale.Z), mat, PhysMgr.GetRayLibTransformMatrix(bodyHandle));
    }
}