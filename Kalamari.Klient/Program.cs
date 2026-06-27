/*******************************************************************************************
*
*   raylib [core] example - World to screen
*
*   This example has been created using raylib 1.3 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Copyright (c) 2015 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

using System.Numerics;
using Kalamari.Engine.DataModelV1;
using Kalamari.Engine.GUI;
using Kalamari.Engine.Utils;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Kalamari.Klient;

public class Program
{
    public static int Main()
    {
        // Initialization
        //--------------------------------------------------------------------------------------
        int screenWidth = 800;
        int screenHeight = 450;
        SetTraceLogLevel(TraceLogLevel.Error);
        
        // Define the camera to look into our 3d world
        Camera3D camera = new();
        camera.Position = new Vector3(10.0f, 10.0f, 10.0f);
        camera.Target = new Vector3(0.0f, 0.0f, 0.0f);
        camera.Up = new Vector3(0.0f, 1.0f, 0.0f);
        camera.FovY = 45.0f;
        camera.Projection = CameraProjection.Perspective;
        
        Vector3 cubePosition = new(0.0f, 30f, 0f);
        Vector2 cubeScreenPosition;
        SetExitKey(KeyboardKey.Null);
        SetTargetFPS(160);
        SetConfigFlags(ConfigFlags.ResizableWindow | ConfigFlags.MaximizedWindow);
        InitWindow(screenWidth, screenHeight, "Kalamari");
        //--------------------------------------------------------------------------------------
        Font idk = AssetLoader.LoadAssetFromURI("kla://fonts/comicsans").GetContents();
        // Main game loop
        PhysMgr.InitWorld();
        DataModel Test = new DataModel("Test", 123);
        Part pidk = new Part("idk");
        pidk.SetParent(Test);
        Part Baseplate = new Part("Baseplate");
        Baseplate.SetParent(Test);
        Baseplate.Position = new Vector3(0, -3, 0);
        Baseplate.Scale = new Vector3(9f, 0.002f, 9f);
        Baseplate.Anchored = true;
        Baseplate.Rotation = new Vector3(0, 0, 0);
        Console.WriteLine("--Start rendering--");
        pidk.Rotation += new Vector3(0, 0.5f, 0);
        pidk.Scale = new Vector3(1f, 1.4f, 1f);
        pidk.Position = new Vector3(cubePosition.X, cubePosition.Y, cubePosition.Z);
        Part pidk2 = new Part("idk2");
        pidk2.SetParent(Test);
        pidk2.Rotation += new Vector3(0.5f, 0.5f, 0.5f);
        pidk2.Scale = new Vector3(1f, 0.5f, 1f);
        pidk2.Position = new Vector3(cubePosition.X, cubePosition.Y + 5, cubePosition.Z);
        ScreenGUI TestGUI = new ScreenGUI("CoreGUI");
        Frame TestFrame = new Frame("TestFrame");
        Button TestBtn = new Button("TestBtn");
        TestBtn.guiRect = new Rectangle(0, 0, 200, 200);
        TestBtn.BackgroundColor = new Color(1, 1, 255);
        TestBtn.BackgroundOpacity = 1f;
        TestBtn.SetParent(TestFrame);
        TestBtn.Click += (sender, args) =>
        {
            Console.WriteLine("Click test success!");
        };
        TestFrame.BackgroundOpacity = 0.7f;
        TestFrame.SetParent(TestGUI);
        TestGUI.SetParent(Test);
        PhysMgr.PhysEnabled = true;
        while (!WindowShouldClose())
        {
            PhysMgr.StepSim();
            if (IsWindowResized() && !IsWindowFullscreen())
            {
                screenWidth = GetScreenWidth();
                screenHeight = GetScreenHeight();
            }
            // Update
            //----------------------------------------------------------------------------------
            TestFrame.guiRect = new Rect(0, 0, screenWidth, 50);
            if (IsMouseButtonDown(MouseButton.Right))
            {
                HideCursor();
                UpdateCamera(ref camera, CameraMode.Free);
                SetMousePosition(GetScreenWidth()/2, GetScreenHeight()/2);
            }
            else
            {
                ShowCursor();
                UpdateCamera(ref camera, CameraMode.Custom);
            }
            // Calculate cube screen space position (with a little offset to be in top)
            cubeScreenPosition = GetWorldToScreen(
                new Vector3(cubePosition.X, cubePosition.Y + 2.5f, cubePosition.Z),
                camera
            );
            //----------------------------------------------------------------------------------

            // Draw
            //----------------------------------------------------------------------------------
            BeginDrawing();
            ClearBackground(Color.SkyBlue);

            BeginMode3D(camera);
            DrawGrid(90, 0.2f);

            Test.Render();
            EndMode3D();
            DrawTextEx(
                idk,
                "Enemy: 100 / 100",
                new Vector2((int)cubeScreenPosition.X - MeasureText("Enemy: 100 / 100", 20) / 2, (int)cubeScreenPosition.Y),
                20,
                2,
                Color.Black
            );
            DrawText(
                "Text is always on top of the cube",
                (screenWidth - MeasureText("Text is always on top of the cube", 20)) / 2,
                25,
                20,
                Color.Gray
            );
            TestGUI.GuiRender();
            EndDrawing();
            //----------------------------------------------------------------------------------
        }

        // De-Initialization
        //--------------------------------------------------------------------------------------
        PhysMgr.DestroyWorld();
        CloseWindow();
        //--------------------------------------------------------------------------------------

        return 0;
    }
}
