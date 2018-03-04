using System;
using SharpGL;

namespace LR_3
{
    public class Camera
    {
        OpenGL gl;
        
        #region Константы и оганичители движения камеры
        const double phiMax = 360.0, phiMin = 0.0;
        const double psiMax = 89.9, psiMin = -89.9;
        const double RMax = 50.0, RMin = 5.0;
        #endregion

        double[] eye = new double[3];
        double[] center = new double[3] { 0, 0, 0 };
        double[] up = new double[3] { 0, 0, 1 };

        #region параметры камеры
        double dx, dy, dz;
        // -------------------- сферические координаты ---------------------
        private double phi = Math.PI / 4.0;
        public double Phi
        {
            get => ToDeg(phi);
            set
            {
                if (value >= phiMax) phi = ToRad(value - 360);
                else if (value < phiMin) phi = ToRad(value + 360);
                else phi = ToRad(value);
            }
        }

        private double psi = Math.PI / 4.0;
        public double Psi
        {
            get => ToDeg(psi);
            set
            {
                if (value >= psiMax) psi = ToRad(psiMax);
                else if (value <= psiMin) psi = ToRad(psiMin);
                else psi = ToRad(value);
            }
        }

        private double r = 50.0;
        public double R
        {
            get => r;
            set
            {
                if (value <= RMin) r = RMin;
                else if (value >= RMax) r = RMax;
                else r = value;
            }
        }
        #endregion

        // конструктор камеры
        public Camera(OpenGL GL)
        {
            gl = GL;
            UpdateView();
        }

        #region Перемещение камеры
        public void Rotate(double new_phi, double new_psi)
        {
            Phi = new_phi; Psi = new_psi;
            UpdateView();
        }

        public void Zoom(double new_R)
        {
            R = new_R;
            UpdateView();
        }

        public void Translate(double dX, double dY, double dZ)
        {
            dx += dX;
            dy += dY;
            dz += dZ;
            UpdateView();
        }

        public void Reset()
        {
            Phi = 45.0; Psi = 45.0; R = 50.0; dx = 0; dy = 0; dz = 0;
            UpdateView();
        }
        #endregion

        #region Вспомогательные функции
        private double ToRad(double angle) => Math.PI * angle / 180.0;

        private double ToDeg(double radians) => radians * 180.0 / Math.PI;

        private void UpdateView()
        {
            eye[0] = R * Math.Cos(phi) * Math.Cos(psi);
            eye[1] = R * Math.Sin(phi) * Math.Cos(psi);
            eye[2] = R * Math.Sin(psi);

            gl.LoadIdentity();
            gl.LookAt(
                eye[0] + dx, eye[1] + dy, eye[2] + dz,
                center[0] + dx, center[1] + dy, center[2] + dz,
                up[0], up[1], up[2]);
        }
        #endregion
    }
}
