using LearnOpenTK.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartoon_City
{

    static class Constants
    {
        public const string path = "../../../Shaders/";
        public const string objectPath = "../../../Objects/";
    }
    internal class Window : GameWindow
    {
        //Camera and Assets

        Camera camera;
        bool cameraMode = false;
        Vector3 lastCameraPositon;

        Asset3d trees = new Asset3d();
        Asset3d road = new Asset3d();
        Asset3d road_decor = new Asset3d();
        Asset3d road_strip = new Asset3d();
        Asset3d sidewalk = new Asset3d();
        Asset3d sidewalk_decor = new Asset3d();
        Asset3d lamps = new Asset3d();
        Asset3d house_detail_brick = new Asset3d();
        Asset3d building_base = new Asset3d();
        Asset3d front_top_building = new Asset3d();
        Asset3d bottom_building = new Asset3d();
        Asset3d platform_building = new Asset3d();
        Asset3d door_window = new Asset3d();
        Asset3d top_border_building = new Asset3d();
        Asset3d chimney_body = new Asset3d();
        Asset3d chimney_top = new Asset3d();
        Asset3d mini_square_roof = new Asset3d();
        Asset3d window_frame = new Asset3d();
        Asset3d window_cage = new Asset3d();
        Asset3d window_glass = new Asset3d();
        Asset3d wrecked_car_tire = new Asset3d();
        Asset3d wrecked_car_rim = new Asset3d();
        Asset3d wrecked_car_body = new Asset3d();
        Asset3d wrecked_car_window = new Asset3d();
        Asset3d bumper_handle_wrecked_car = new Asset3d();
        Asset3d car_lamp = new Asset3d();
        Asset3d cone = new Asset3d();
        Asset3d moon = new Asset3d();

        Player player = new Player();

        Vector3[] _moonPosition = { new Vector3(-7.9995203f, 2.7318864f, -5.665344f) };

        Vector3[] _lampPositions = {
                new Vector3(-0.17693141f, -1.2500714f, 3.3932884f),
                new Vector3(-0.7817676f, -1.2471954f, 3.3834538f),
                new Vector3(-2.51437f, -1.2471954f, 3.3834538f),
                new Vector3(-3.153377f, -1.2471954f, 3.3834538f),
                new Vector3(-4.1804531f, -1.2471954f, 3.3834538f),
                new Vector3(-5.5194537f, -1.2471954f, 3.3834538f),

                new Vector3(-5.462322f, -1.3194546f, -1.5044234f),
                new Vector3(-4.1804531f, -1.3194546f, -1.5044234f),
                new Vector3(-3.153377f, -1.3194546f, -1.5044234f),
                new Vector3(-2.51437f, -1.3194546f, -1.5044234f),
                new Vector3(-0.7817676f, -1.3194546f, -1.5044234f),
                new Vector3(-0.17693141f, -1.3194546f, -1.5044234f),
        };

        bool firstMove;
        Vector2 lastPos;
        Matrix4 model = Matrix4.Identity;


        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            camera = new Camera(new Vector3(-4.565097f, -0.9874678f, -0.3335253f), Size.X / Size.Y);
            camera.Yaw = camera.Yaw + 90;
            camera.Pitch = camera.Pitch - 26f;
            firstMove = true;
            CursorGrabbed = true;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.Enable(EnableCap.DepthTest);
            GL.ClearColor(new Color4(0.18f, 0.27f, 0.51f, 1));

            //Load Terrain
            {

                trees.loadObject(Constants.objectPath + "tree.obj", new Vector3(0, 0, 0));
                trees.Load(Constants.path + "glass.vert", Constants.path + "glass.frag", Size.X, Size.Y);

                road.loadObject(Constants.objectPath + "road.obj", new Vector3(0, 0, 0));
                road.Load(Constants.path + "objectshader.vert", Constants.path + "glass.frag", Size.X, Size.Y);

                road_strip.loadObject(Constants.objectPath + "roadstrip.obj", new Vector3(0, 0, 0));
                road_strip.Load(Constants.path + "objectshader.vert", Constants.path + "objectshader.frag", Size.X, Size.Y);

                sidewalk.loadObject(Constants.objectPath + "sidewalk.obj", new Vector3(0, 0, 0));
                sidewalk.Load(Constants.path + "objectshader.vert", Constants.path + "objectshader.frag", Size.X, Size.Y);

                road_decor.loadObject(Constants.objectPath + "road_decor.obj", new Vector3(0, 0, 0));
                road_decor.Load(Constants.path + "objectshader.vert", Constants.path + "glass.frag", Size.X, Size.Y);

                sidewalk_decor.loadObject(Constants.objectPath + "sidewalk_decor.obj", new Vector3(0, 0, 0));
                sidewalk_decor.Load(Constants.path + "objectshader.vert", Constants.path + "objectshader.frag", Size.X, Size.Y);

                lamps.loadObject(Constants.objectPath + "lamps.obj", new Vector3(0, 0, 0));
                lamps.Load(Constants.path + "objectshader.vert", Constants.path + "objectshader.frag", Size.X, Size.Y);

                house_detail_brick.loadObject(Constants.objectPath + "house_detail_brick.obj", new Vector3(0, 0, 0));
                house_detail_brick.Load(Constants.path + "glass.vert", Constants.path + "glass.frag", Size.X, Size.Y);

                building_base.loadObject(Constants.objectPath + "building_base.obj", new Vector3(0, 0, 0));
                building_base.Load(Constants.path + "glass.vert", Constants.path + "glass.frag", Size.X, Size.Y);

                front_top_building.loadObject(Constants.objectPath + "front_top_building.obj", new Vector3(0, 0, 0));
                front_top_building.Load(Constants.path + "glass.vert", Constants.path + "glass.frag", Size.X, Size.Y);

                bottom_building.loadObject(Constants.objectPath + "bottom_building.obj", new Vector3(0, 0, 0));
                bottom_building.Load(Constants.path + "glass.vert", Constants.path + "glass.frag", Size.X, Size.Y);

                platform_building.loadObject(Constants.objectPath + "platform_building.obj", new Vector3(0, 0, 0));
                platform_building.Load(Constants.path + "glass.vert", Constants.path + "glass.frag", Size.X, Size.Y);

                door_window.loadObject(Constants.objectPath + "door_window.obj", new Vector3(0, 0, 0));
                door_window.Load(Constants.path + "glass.vert", Constants.path + "glass.frag", Size.X, Size.Y);

                top_border_building.loadObject(Constants.objectPath + "top_border_building.obj", new Vector3(0, 0, 0));
                top_border_building.Load(Constants.path + "glass.vert", Constants.path + "glass.frag", Size.X, Size.Y);

                chimney_body.loadObject(Constants.objectPath + "chimney_body.obj", new Vector3(0, 0, 0));
                chimney_body.Load(Constants.path + "glass.vert", Constants.path + "glass.frag", Size.X, Size.Y);

                chimney_top.loadObject(Constants.objectPath + "chimney_top.obj", new Vector3(0, 0, 0));
                chimney_top.Load(Constants.path + "glass.vert", Constants.path + "glass.frag", Size.X, Size.Y);

                mini_square_roof.loadObject(Constants.objectPath + "mini_square_roof.obj", new Vector3(0, 0, 0));
                mini_square_roof.Load(Constants.path + "glass.vert", Constants.path + "glass.frag", Size.X, Size.Y);

                window_frame.loadObject(Constants.objectPath + "window_frame.obj", new Vector3(0, 0, 0));
                window_frame.Load(Constants.path + "glass.vert", Constants.path + "glass.frag", Size.X, Size.Y);

                window_cage.loadObject(Constants.objectPath + "window_cage.obj", new Vector3(0, 0, 0));
                window_cage.Load(Constants.path + "glass.vert", Constants.path + "glass.frag", Size.X, Size.Y);

                window_glass.loadObject(Constants.objectPath + "window_glass.obj", new Vector3(0, 0, 0));
                window_glass.Load(Constants.path + "glass.vert", Constants.path + "glass.frag", Size.X, Size.Y);

                wrecked_car_tire.loadObject(Constants.objectPath + "wrecked_car_tire.obj", new Vector3(0, 0, 1.25f));
                wrecked_car_tire.Load(Constants.path + "objectshader.vert", Constants.path + "objectshader.frag", Size.X, Size.Y);

                wrecked_car_rim.loadObject(Constants.objectPath + "wrecked_car_rim.obj", new Vector3(0, 0, 1.25f));
                wrecked_car_rim.Load(Constants.path + "objectshader.vert", Constants.path + "objectshader.frag", Size.X, Size.Y);

                wrecked_car_body.loadObject(Constants.objectPath + "wrecked_car_body.obj", new Vector3(0, 0, 1.25f));
                wrecked_car_body.Load(Constants.path + "objectshader.vert", Constants.path + "objectshader.frag", Size.X, Size.Y);

                wrecked_car_window.loadObject(Constants.objectPath + "wrecked_car_window.obj", new Vector3(0, 0, 1.25f));
                wrecked_car_window.Load(Constants.path + "glass.vert", Constants.path + "glass.frag", Size.X, Size.Y);

                bumper_handle_wrecked_car.loadObject(Constants.objectPath + "bumper_handle_wrecked_car.obj", new Vector3(0, 0, 1.25f));
                bumper_handle_wrecked_car.Load(Constants.path + "objectshader.vert", Constants.path + "objectshader.frag", Size.X, Size.Y);

                car_lamp.loadObject(Constants.objectPath + "car_lamp.obj", new Vector3(0, 0, 1.25f));
                car_lamp.Load(Constants.path + "glass.vert", Constants.path + "glass.frag", Size.X, Size.Y);

                cone.loadObject(Constants.objectPath + "cone.obj", new Vector3(0, 0, 1.25f));
                cone.Load(Constants.path + "objectshader.vert", Constants.path + "objectshader.frag", Size.X, Size.Y);

                moon.loadObject(Constants.objectPath + "moon.obj", new Vector3(0, 0, 0));
                moon.Load(Constants.path + "objectshader.vert", Constants.path + "objectshader.frag", Size.X, Size.Y);

                player.Load(Size.X, Size.Y);
            }

            Vector3 wrecked_car_center_position = new Vector3(-1.7293575f, -1.4322062f, 1.4807904f);
            wrecked_car_tire.rotate(wrecked_car_center_position,new Vector3(0,1,0),180,false);
            wrecked_car_rim.rotate(wrecked_car_center_position, new Vector3(0, 1, 0), 180, false);
            wrecked_car_body.rotate(wrecked_car_center_position, new Vector3(0, 1, 0), 180, false);
            wrecked_car_window.rotate(wrecked_car_center_position, new Vector3(0, 1, 0), 180, false);
            bumper_handle_wrecked_car.rotate(wrecked_car_center_position, new Vector3(0, 1, 0), 180, false);
            car_lamp.rotate(wrecked_car_center_position, new Vector3(0, 1, 0), 180, false);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Matrix4 temp = Matrix4.Identity;



            //Phong settings
            {
                List<Asset3d> listObjects = new List<Asset3d>();
                listObjects.Add(road); //0
                listObjects.Add(trees); //1
                listObjects.Add(road_decor); //2
                listObjects.Add(road_strip); //3
                listObjects.Add(sidewalk); //4
                listObjects.Add(sidewalk_decor); //5
                listObjects.Add(lamps); //6
                listObjects.Add(house_detail_brick); //7
                listObjects.Add(building_base); //8
                listObjects.Add(front_top_building); //9
                listObjects.Add(bottom_building); //10
                listObjects.Add(platform_building); //11
                listObjects.Add(door_window); //12
                listObjects.Add(top_border_building); //13
                listObjects.Add(chimney_body); //14
                listObjects.Add(chimney_top); //15
                listObjects.Add(mini_square_roof); //16
                listObjects.Add(window_frame); //17
                listObjects.Add(window_cage); //18
                listObjects.Add(window_glass); //19
                listObjects.Add(wrecked_car_tire);//20
                listObjects.Add(wrecked_car_rim);//21
                listObjects.Add(wrecked_car_body);//22
                listObjects.Add(wrecked_car_window);//23
                listObjects.Add(bumper_handle_wrecked_car);//24
                listObjects.Add(car_lamp);//25
                listObjects.Add(cone);//26
                listObjects.Add(moon);//27


                foreach (Asset3d a in listObjects)
                {
                    a.Render(0, model, camera.GetViewMatrix(), camera.GetProjectionMatrix());
                    a.setSpotLight(camera.Position, camera.Front, new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0.5f, 0.5f, 0.5f), 1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                    a.setDirectionalLight(new Vector3(2.2735434f, -3f, 2.6951704f), new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.25f, 0.25f, 0.25f), new Vector3(0, 0, 0));
                    
                }
                //road
                listObjects[0].setPointLights(_moonPosition, new Vector3(0.1f, 0.1f, 0.1f), new Vector3(0.1f, 0.1f, 0.1f), new Vector3(0.3f, 0.3f, 0.3f), 1.0f, 0.0014f, 0.000007f);

                //trees
                listObjects[1].setPointLights(_moonPosition, new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f), new Vector3(0, 0, 0), 1.0f, 0.0014f, 0.000007f);


                listObjects[2].setPointLights(_moonPosition, new Vector3(0.1f, 0.1f, 0.1f), new Vector3(0.1f, 0.1f, 0.1f), new Vector3(0.1f, 0.1f, 0.1f), 1.0f, 0.0014f, 0.000007f);
                listObjects[3].setPointLights(_lampPositions, new Vector3(0.00f, 0.00f, 0.0f), new Vector3(0f, 0f, 0f), new Vector3(0, 0, 0), 1.0f, 0.09f, 0.032f);

                //sidewalk
                listObjects[4].setPointLights(_lampPositions, new Vector3(1f, 1f, 1f), new Vector3(1,1,1), new Vector3(0.05f,0.05f,0.05f), 1.0f, 0.9f, 2.1f);

                listObjects[5].setPointLights(_lampPositions, new Vector3(0.00f, 0.00f, 0.0f), new Vector3(0f, 0f, 0f), new Vector3(0, 0, 0), 1.0f, 0.09f, 0.032f);
                listObjects[6].setPointLights(_lampPositions, new Vector3(0.00f, 0.00f, 0.0f), new Vector3(0f, 0f, 0f), new Vector3(0, 0, 0), 1.0f, 0.09f, 0.032f);

                listObjects[7].setPointLights(_moonPosition, new Vector3(0.20f, 0.20f, 0.20f), new Vector3(0.1f, 0.1f, 0.1f), new Vector3(0, 0, 0), 1.0f, 0.014f, 0.0007f);
                listObjects[8].setPointLights(_moonPosition, new Vector3(0.20f, 0.20f, 0.20f), new Vector3(0.1f, 0.1f, 0.1f), new Vector3(0, 0, 0), 1.0f, 0.014f, 0.0007f);
                listObjects[9].setPointLights(_moonPosition, new Vector3(0.20f, 0.20f, 0.20f), new Vector3(0.1f, 0.1f, 0.1f), new Vector3(0, 0, 0), 1.0f, 0.014f, 0.0007f);
                listObjects[10].setPointLights(_moonPosition, new Vector3(0.20f, 0.20f, 0.20f), new Vector3(0.1f, 0.1f, 0.1f), new Vector3(0, 0, 0), 1.0f, 0.014f, 0.0007f);
                listObjects[11].setPointLights(_moonPosition, new Vector3(0.20f, 0.20f, 0.20f), new Vector3(0.1f, 0.1f, 0.1f), new Vector3(0, 0, 0), 1.0f, 0.014f, 0.0007f);
                listObjects[12].setPointLights(_moonPosition, new Vector3(0.20f, 0.20f, 0.20f), new Vector3(0.1f, 0.1f, 0.1f), new Vector3(0, 0, 0), 1.0f, 0.014f, 0.0007f);
                listObjects[13].setPointLights(_moonPosition, new Vector3(0.20f, 0.20f, 0.20f), new Vector3(0.1f, 0.1f, 0.1f), new Vector3(0, 0, 0), 1.0f, 0.014f, 0.0007f);
                listObjects[14].setPointLights(_moonPosition, new Vector3(0.20f, 0.20f, 0.20f), new Vector3(0.1f, 0.1f, 0.1f), new Vector3(0, 0, 0), 1.0f, 0.014f, 0.0007f);
                listObjects[15].setPointLights(_moonPosition, new Vector3(0.20f, 0.20f, 0.20f), new Vector3(0.1f, 0.1f, 0.1f), new Vector3(0, 0, 0), 1.0f, 0.014f, 0.0007f);
                listObjects[16].setPointLights(_moonPosition, new Vector3(0.20f, 0.20f, 0.20f), new Vector3(0.1f, 0.1f, 0.1f), new Vector3(0, 0, 0), 1.0f, 0.014f, 0.0007f);
                listObjects[17].setPointLights(_moonPosition, new Vector3(0.20f, 0.20f, 0.20f), new Vector3(0.1f, 0.1f, 0.1f), new Vector3(0, 0, 0), 1.0f, 0.014f, 0.0007f);
                listObjects[18].setPointLights(_moonPosition, new Vector3(0.20f, 0.20f, 0.20f), new Vector3(0.1f, 0.1f, 0.1f), new Vector3(0, 0, 0), 1.0f, 0.014f, 0.0007f);

                //Jendela rumah
                listObjects[19].setPointLights(_moonPosition, new Vector3(0.3f, 0.3f, 0.3f), new Vector3(0.1f, 0.1f, 0.1f), new Vector3(1, 1, 1), 1f, 0.014f, 0.0007f);

                listObjects[20].setPointLights(_lampPositions, new Vector3(0.00f, 0.00f, 0.0f), new Vector3(0f, 0f, 0f), new Vector3(0, 0, 0), 1.0f, 0.09f, 0.032f);
                listObjects[21].setPointLights(_lampPositions, new Vector3(0.00f, 0.00f, 0.0f), new Vector3(0f, 0f, 0f), new Vector3(0, 0, 0), 1.0f, 0.09f, 0.032f);
                listObjects[22].setPointLights(_lampPositions, new Vector3(0.00f, 0.00f, 0.0f), new Vector3(0f, 0f, 0f), new Vector3(0, 0, 0), 1.0f, 0.09f, 0.032f);
                
                //Jendela mobil rusak
                listObjects[23].setPointLights(_moonPosition, new Vector3(0.3f, 0.3f, 0.3f), new Vector3(0.1f, 0.1f, 0.1f), new Vector3(1, 1, 1), 1f, 0.014f, 0.0007f);

                listObjects[24].setPointLights(_lampPositions, new Vector3(0.00f, 0.00f, 0.0f), new Vector3(0f, 0f, 0f), new Vector3(0, 0, 0), 1.0f, 0.09f, 0.032f);

                //Lampu mobil rusak
                listObjects[25].setPointLights(_moonPosition, new Vector3(0.3f, 0.3f, 0.3f), new Vector3(0.1f, 0.1f, 0.1f), new Vector3(1, 1, 1), 1f, 0.014f, 0.0007f);

                listObjects[26].setPointLights(_lampPositions, new Vector3(0.00f, 0.00f, 0.0f), new Vector3(0f, 0f, 0f), new Vector3(0, 0, 0), 1.0f, 0.09f, 0.032f);
                listObjects[27].setPointLights(_lampPositions, new Vector3(0.1f, 0.1f, 0.1f), new Vector3(0.1f, 0.1f, 0.1f), new Vector3(0.0f, 0.0f, 0.0f), 1.0f, 0.09f, 0.032f);

                trees.setFragVariable(new Vector3(0.008f,0.197f,0f), camera.Position);
                road.setFragVariable(new Vector3(0.25f, 0.26f, 0.25f), camera.Position);
                road_decor.setFragVariable(new Vector3(0.6f, 0.6f, 0.6f), camera.Position);
                road_strip.setFragVariable(new Vector3(1f,1f,1f), camera.Position);
                sidewalk.setFragVariable(new Vector3(0.153f, 0.022f, 0f), camera.Position);
                sidewalk_decor.setFragVariable(new Vector3(0.8f,0.145f,0.11f), camera.Position);
                lamps.setFragVariable(new Vector3(0.065f, 0.044f, 0.042f), camera.Position);
                house_detail_brick.setFragVariable(new Vector3(0.8f, 0.145f, 0.11f), camera.Position);
                building_base.setFragVariable(new Vector3(0.8f,0.288f,0.014f), camera.Position);
                front_top_building.setFragVariable(new Vector3(0.8f, 0.224f, 0.013f), camera.Position);
                bottom_building.setFragVariable(new Vector3(0.8f, 0.758f, 0.171f), camera.Position);
                platform_building.setFragVariable(new Vector3(0.719f, 0.8f, 0.752f), camera.Position);
                door_window.setFragVariable(new Vector3(0.434f, 0.156f, 0.049f), camera.Position);
                top_border_building.setFragVariable(new Vector3(0.8f, 0.117f, 0.007f), camera.Position);
                chimney_body.setFragVariable(new Vector3(0.27f, 0.27f, 0.27f), camera.Position);
                chimney_top.setFragVariable(new Vector3(0.076f, 0.050f, 0.040f), camera.Position);
                mini_square_roof.setFragVariable(new Vector3(0.123f, 0.095f, 0.099f), camera.Position);
                window_frame.setFragVariable(new Vector3(0.434f, 0.156f, 0.049f), camera.Position);
                window_cage.setFragVariable(new Vector3(0.434f, 0.156f, 0.049f), camera.Position);
                window_glass.setFragVariable(new Vector3(1f, 1f, 1f), camera.Position);
                wrecked_car_tire.setFragVariable(new Vector3(0,0,0),camera.Position);
                wrecked_car_rim.setFragVariable(new Vector3(1, 1, 1), camera.Position);
                wrecked_car_body.setFragVariable(new Vector3(0.8f, 0, 0), camera.Position);
                wrecked_car_window.setFragVariable(new Vector3(1,1,1),camera.Position);
                bumper_handle_wrecked_car.setFragVariable(new Vector3(0, 0, 0), camera.Position);
                car_lamp.setFragVariable(new Vector3(0.8f, 0.643f, 0), camera.Position);
                cone.setFragVariable(new Vector3(0.8f,0.132f,0.007f),camera.Position);
                moon.setFragVariable(new Vector3(0.666f, 0.666f, 0.666f), camera.Position);
            }

            player.Render(0, camera);

            //Camera movement
            {   
                float cameraSpeed = 0.5f;
                var mouse = MouseState;
                var sensitivity = 0.1f;
                if (firstMove)
                {
                    lastPos = new Vector2(mouse.X, mouse.Y);
                    firstMove = false;
                }
                else
                {
                    var deltaX = mouse.X - lastPos.X;
                    var deltaY = mouse.Y - lastPos.Y;

                    lastPos = new Vector2(mouse.X, mouse.Y);
                    camera.Yaw += deltaX * sensitivity;
                    camera.Pitch -= deltaY * sensitivity;
                }

                if (cameraMode)
                {
                    //Camera maju
                    if (KeyboardState.IsKeyDown(Keys.W))
                    {
                        camera.Position += camera.Front * cameraSpeed * (float)args.Time;
                    }
                    //Camera mundur
                    if (KeyboardState.IsKeyDown(Keys.S))
                    {
                        camera.Position -= camera.Front * cameraSpeed * (float)args.Time;
                    }
                    //Camera Kanan
                    if (KeyboardState.IsKeyDown(Keys.D))
                    {
                        camera.Position += camera.Right * cameraSpeed * (float)args.Time;
                    }
                    //Camera Kiri
                    if (KeyboardState.IsKeyDown(Keys.A))
                    {
                        camera.Position -= camera.Right * cameraSpeed * (float)args.Time;
                    }

                    //Camera naik
                    if (KeyboardState.IsKeyDown(Keys.Space))
                    {
                        camera.Position += camera.Up * cameraSpeed * (float)args.Time;
                    }
                    //Camera turun
                    if (KeyboardState.IsKeyDown(Keys.LeftShift))
                    {
                        camera.Position -= camera.Up * cameraSpeed * (float)args.Time;
                    }

                    //Console.WriteLine("new Vector3(" + camera.Position.X + "f, " + camera.Position.Y + "f, " + camera.Position.Z + "f)");
                }
                else
                {
                    //Camera maju
                    if (KeyboardState.IsKeyDown(Keys.W))
                    {
                        if (KeyboardState.IsKeyDown(Keys.W) && KeyboardState.IsKeyDown(Keys.A))
                        {
                            player.rotateLeftForward(camera, cameraSpeed, (float)args.Time);
                        }

                        else if (KeyboardState.IsKeyDown(Keys.W) && KeyboardState.IsKeyDown(Keys.D))
                        {
                            player.rotateRightForward(camera, cameraSpeed, (float)args.Time);
                        }
                        else
                        {
                            player.moveForward(camera, cameraSpeed, (float)args.Time);
                        }

                    }
                    //Camera mundur
                    if (KeyboardState.IsKeyDown(Keys.S))
                    {
                        if (KeyboardState.IsKeyDown(Keys.S) && KeyboardState.IsKeyDown(Keys.A))
                        {
                            player.rotateLeftBackward(camera, cameraSpeed, (float)args.Time);
                        }

                        else if (KeyboardState.IsKeyDown(Keys.S) && KeyboardState.IsKeyDown(Keys.D))
                        {
                            player.rotateRightBackward(camera, cameraSpeed, (float)args.Time);
                        }
                        else
                        {
                            player.moveBackward(camera, cameraSpeed, (float)args.Time);
                        }

                    }
                    //Camera Kanan
                    if (KeyboardState.IsKeyDown(Keys.D))
                    {
                        player.rotateRight(camera, cameraSpeed, (float)args.Time);
                    }
                    //Camera Kiri
                    if (KeyboardState.IsKeyDown(Keys.A))
                    {

                        player.rotateLeft(camera, cameraSpeed, (float)args.Time);

                    }

                }

            }

            SwapBuffers();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            camera.AspectRatio = Size.X / (float)Size.Y;


            GL.Viewport(0, 0, Size.X, Size.Y);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            if (KeyboardState.IsKeyPressed(Keys.F5))
            {
                if (cameraMode)
                {
                    camera.Position = lastCameraPositon;
                    cameraMode = false;
                }
                else
                {
                    lastCameraPositon = camera.Position;
                    cameraMode = true;
                }
            }
        }
    }

}
