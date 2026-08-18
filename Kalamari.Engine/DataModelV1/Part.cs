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
        get => bodyHandle.Position;
        set
        {
            _position = value;
            bodyHandle.Position = value;
            _transform = Raymath.MatrixCompose(value, new Quaternion(Rotation, 1f), Scale);
            
        }
    }

    private Matrix4x4 _transform;
    public Vector3 Rotation
    {
        get;
        set
        {
            field = value;
            bodyHandle.Orientation = Quaternion.CreateFromYawPitchRoll(value.X, value.Y, value.Z);
            _transform = Raymath.MatrixCompose(Position, new Quaternion(value, 1f), Scale);
        }
    }

    public Vector3 Scale
    {
        get => field;
        set
        {
            field = value;
            //idk = Raylib.GenMeshCube(value.X, value.Y, value.Z);
            if (Anchored)
            {
                return;
            }

            if (bodyHandle == null)
            {
                return;
            }
            _transform = Raymath.MatrixCompose(Position, new Quaternion(Rotation, 1f), value);
            BoxShape shape = new BoxShape(Scale);
            bodyHandle.RemoveShape(bodyHandle.Shapes[0]);
            bodyHandle.AddShape(shape);
        }
        
    }
    private RigidBody bodyHandle;

    public bool Anchored
    {
        get => field;
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

            field = value;
        }
    }
    public Mesh idk;
    
    private Material mat = Raylib.LoadMaterialDefault();
    private Vector3 _position;

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
        mat = Raylib.LoadMaterialDefault();
        Scale = Vector3.One;
        PartColor = Color.White;
        idk = Raylib.GenMeshCube(Scale.X, Scale.Y, Scale.Z);
        _transform = Raymath.MatrixCompose(Vector3.Zero, Quaternion.Create(Vector3.Zero, 1f), Vector3.One);
        //idk.Transform = Raymath.MatrixCompose(Vector3.Zero, Quaternion.Create(Vector3.Zero, 1f), Vector3.One);
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
            Position = Vector3.Zero;
            Rotation = Vector3.Zero;
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
        //WHY THE FUCK DOESNT PHYSICS WORK AFTER THE OPTIMIZATION IM GONNA FUCKING KILL MYSELF
        //MORE IMPORTANTLY: WHY DOESNT THE BASEPLATE RENDER???
        //Console.WriteLine("Rendering " + Name + " | pos: " + Position + " | rot: " + Rotation);
        if (!Anchored)
        {
            Position = bodyHandle.Position;
            Rotation = bodyHandle.Orientation.Vector;
        }
        else
        {
            Position = _position;
        }
        Raylib.DrawMesh(idk, mat, _transform);
    }
}