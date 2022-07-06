using LearnOpenTK.Common;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartoon_City
{

    internal class Player
    {
        Asset3d car = new Asset3d();

        Matrix4 carModel = Matrix4.Identity;

        Vector3[] _moonPosition = { new Vector3(-7.9995203f, 2.7318864f, -5.665344f) };

        //URUTAN
        //Vector3 carPositionKananDepan #0
        //Vector3 carPositionKananBelakang #1
        //Vector3 carPositionKiriDepan #2
        //Vector3 carPositionKiriBelakang #3

        Vector3[] carPosition = {

            new Vector3(-3.2469282f, -1.4789126f, -0.19049156f),
            new Vector3(-3.6802313f, -1.5321842f, -0.1563012f),
            new Vector3(-3.2132528f, -1.5441183f, -0.48544702f),
            new Vector3(-3.7041771f, -1.5343378f, -0.48462996f)
        };

        float _streetLimitKanan = 0.5862955f;
        float _streetLimitKiri = -0.6160125f;
        float _streetLimitDepan = 0.7338183f;
        float _streetLimitBelakang = -6.400983f;

        float sizeX = 0f;
        float sizeY = 0f;


        public Player()
        {
            car._children.Add(new Asset3d());
            car._children.Add(new Asset3d());
            car._children.Add(new Asset3d());
            car._children.Add(new Asset3d());
            car._children.Add(new Asset3d());
        }

        public void Load(float sizeX, float sizeY)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            car.loadObject(Constants.objectPath + "main_car_body.obj", new Vector3(0, 0, 0));
            car._centerPosition = new Vector3(-3.5399039f, -1.4611441f, -0.3291779f);
            car._children[0].loadObject(Constants.objectPath + "main_car_rim.obj", new Vector3(0, 0, 0));
            car._children[1].loadObject(Constants.objectPath + "main_car_tire.obj", new Vector3(0, 0, 0));
            car._children[2].loadObject(Constants.objectPath + "main_car_window.obj", new Vector3(0, 0, 0));
            car._children[3].loadObject(Constants.objectPath + "main_car_bumper_handle.obj", new Vector3(0, 0, 0));
            car._children[4].loadObject(Constants.objectPath + "main_car_lamp.obj", new Vector3(0, 0, 0));
            car.Load(Constants.path + "glass.vert", Constants.path + "glass.frag", sizeX, sizeY);
        }

        public void Render(int line, Camera camera)
        {
            car.Render(line, carModel, camera.GetViewMatrix(), camera.GetProjectionMatrix());
            car.setSpotLight(camera.Position, camera.Front, new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0.5f, 0.5f, 0.5f), 1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(25f)), MathF.Cos(MathHelper.DegreesToRadians(50f)));
            car.setDirectionalLight(new Vector3(2.2735434f, -3f, 2.6951704f), new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.25f, 0.25f, 0.25f), new Vector3(0, 0, 0));
            car.setPointLights(_moonPosition, new Vector3(0.00f, 0.00f, 0.0f), new Vector3(0f, 0f, 0f), new Vector3(0, 0, 0), 1.0f, 0.09f, 0.032f);

            car.setFragVariable(new Vector3(0.8f, 0, 0), camera.Position);
            car._children[0].setFragVariable(new Vector3(1, 1, 1), camera.Position);
            car._children[1].setFragVariable(new Vector3(0.0f, 0.0f, 0.0f), camera.Position);
            car._children[2].setFragVariable(new Vector3(1, 1, 1), camera.Position);
            car._children[3].setFragVariable(new Vector3(0.0f, 0.0f, 0.0f), camera.Position);
            car._children[4].setFragVariable(new Vector3(0.8f, 0.643f, 0.0f), camera.Position);
        }

        public string isCollided()
        {
            string result = "";

            foreach (var carPos in carPosition)
            {
                if (carPos.Z >= _streetLimitKanan)
                {
                    result = "right";
                }
                if (carPos.Z <= _streetLimitKiri)
                {
                    result = "left";
                }
                if (carPos.X >= _streetLimitDepan)
                {
                    result = "forward";
                }
                if (carPos.X <= _streetLimitBelakang)
                {
                    result = "backward";
                }
            }

            return result;
        }

        public void moveForward(Camera camera, float cameraSpeed, float time)
        {

            if (isCollided() != "forward")
            {
                camera.Yaw = 0;
                camera.Pitch = -26f;
                car.translateX(cameraSpeed * 0.57f);
                for (int i = 0; i < carPosition.Length; i++)
                {
                    Vector3 temp = new Vector3(carPosition[i].X + cameraSpeed * 0.5f * 0.017f, carPosition[i].Y, carPosition[i].Z);
                    carPosition[i] = temp;
                }
                //Console.WriteLine(carPosition[0].Z);
                camera.Pitch = camera.Pitch + 26f;
                camera.Position += camera.Front * cameraSpeed * time;
                camera.Pitch = camera.Pitch - 26f;
            }

        }

        public void moveBackward(Camera camera, float cameraSpeed, float time)
        {
            if (isCollided() != "backward")
            {
                camera.Yaw = 0;
                camera.Pitch = -26f;
                car.translateX(-cameraSpeed * 0.5f);
                for (int i = 0; i < carPosition.Length; i++)
                {
                    Vector3 temp = new Vector3(carPosition[i].X - cameraSpeed * 0.5f * 0.017f, carPosition[i].Y, carPosition[i].Z);
                    carPosition[i] = temp;
                }
                //Console.WriteLine(carPosition[0].Z);
                camera.Pitch = camera.Pitch + 26f;
                camera.Position -= camera.Front * cameraSpeed * time;
                camera.Pitch = camera.Pitch - 26f;
            }
        }

        public void rotateLeft(Camera camera, float cameraSpeed, float time)
        {

            camera.Yaw = 0;
            camera.Pitch = -26f;
            car.rotate(car._centerPosition, new Vector3(0, 1, 0), 1.5f, true);
            camera.Pitch = camera.Pitch + 26f;
            camera.Pitch = camera.Pitch - 26f;
        }

        public void rotateRight(Camera camera, float cameraSpeed, float time)
        {
            camera.Yaw = 0;
            camera.Pitch = -26f;
            car.rotate(car._centerPosition, new Vector3(0, 1, 0), -1.5f, false);
            camera.Pitch = camera.Pitch + 26f;
            camera.Pitch = camera.Pitch - 26f;
        }

        public void rotateLeftForward(Camera camera, float cameraSpeed, float time)
        {
            if (isCollided() != "left" && isCollided() != "forward")
            {
                camera.Yaw = 0;
                camera.Pitch = -26f;
                car.rotate(car._centerPosition, new Vector3(0, 1, 0), 1.5f, true);
                car.translateX(cameraSpeed * 0.5f * 0.8f);
                car.translateZ(-cameraSpeed * 0.5f * 0.8f);
                for (int i = 0; i < carPosition.Length; i++)
                {
                    Vector3 temp = new Vector3(carPosition[i].X + cameraSpeed * 0.5f * 0.017f, carPosition[i].Y, carPosition[i].Z - cameraSpeed * 0.5f * 0.017f);
                    carPosition[i] = temp;
                }
                //Console.WriteLine(carPosition[0].Z);
                camera.Pitch = camera.Pitch + 26f;
                camera.Position += camera.Front * cameraSpeed * 0.8f * time;
                camera.Position -= camera.Right * cameraSpeed * 0.8f * time;
                camera.Pitch = camera.Pitch - 26f;
            }
        }

        public void rotateRightForward(Camera camera, float cameraSpeed, float time)
        {
            if (isCollided() != "right" && isCollided() != "forward")
            {
                camera.Yaw = 0;
                camera.Pitch = -26f;
                car.rotate(car._centerPosition, new Vector3(0, 1, 0), -1.5f, false);
                car.translateX(cameraSpeed * 0.5f * 0.8f);
                car.translateZ(cameraSpeed * 0.5f * 0.8f);
                for (int i = 0; i < carPosition.Length; i++)
                {
                    Vector3 temp = new Vector3(carPosition[i].X + cameraSpeed * 0.5f * 0.017f, carPosition[i].Y, carPosition[i].Z + cameraSpeed * 0.5f * 0.017f);
                    carPosition[i] = temp;
                }
                //Console.WriteLine(carPosition[0].Z);
                camera.Pitch = camera.Pitch + 26f;
                camera.Position += camera.Front * cameraSpeed * 0.8f * time;
                camera.Position += camera.Right * cameraSpeed * 0.8f * time;
                camera.Pitch = camera.Pitch - 26f;
            }
        }

        public void rotateLeftBackward(Camera camera, float cameraSpeed, float time)
        {
            if (isCollided() != "right" && isCollided() != "backward")
            {
                car.rotate(car._centerPosition, new Vector3(0, 1, 0), 1.5f, true);
                car.translateX(-cameraSpeed * 0.5f * 0.8f);
                car.translateZ(+cameraSpeed * 0.5f * 0.8f);
                for (int i = 0; i < carPosition.Length; i++)
                {
                    Vector3 temp = new Vector3(carPosition[i].X - cameraSpeed * 0.5f * 0.017f, carPosition[i].Y, carPosition[i].Z + cameraSpeed * 0.5f * 0.017f);
                    carPosition[i] = temp;
                }
                //Console.WriteLine(carPosition[0].Z);
                camera.Pitch = camera.Pitch + 26f;
                camera.Position -= camera.Front * cameraSpeed * 0.8f * time;
                camera.Position += camera.Right * cameraSpeed * 0.8f * time;
                camera.Pitch = camera.Pitch - 26f;
            }
        }

        public void rotateRightBackward(Camera camera, float cameraSpeed, float time)
        {
            if (isCollided() != "left" && isCollided() != "backward")
            {
                car.rotate(car._centerPosition, new Vector3(0, 1, 0), -1.5f, false);
                car.translateX(-cameraSpeed * 0.5f * 0.8f);
                car.translateZ(-cameraSpeed * 0.5f * 0.8f);
                for (int i = 0; i < carPosition.Length; i++)
                {
                    Vector3 temp = new Vector3(carPosition[i].X - cameraSpeed * 0.5f * 0.017f, carPosition[i].Y, carPosition[i].Z - cameraSpeed * 0.5f * 0.017f);
                    carPosition[i] = temp;
                }
                //Console.WriteLine(carPosition[0].Z);
                camera.Pitch = camera.Pitch + 26f;
                camera.Position -= camera.Front * cameraSpeed * 0.8f * time;
                camera.Position -= camera.Right * cameraSpeed * 0.8f * time;
                camera.Pitch = camera.Pitch - 26f;
            }
        }

    }
}