using System;

namespace LR_3
{
    /// <summary>
    /// Вспомогательный класс, реализующий простейшие операции над векторами
    /// </summary>
    public class Vector
    {
        public int Dimension { get; }
        double[] vec;

        public enum Axis { X, Y, Z }

        public Vector(int Dimension)
        {
            this.Dimension = Dimension;
            vec = new double[Dimension];
        }

        public Vector(int Dimension, double Init)
        {
            this.Dimension = Dimension;
            vec = new double[Dimension];
            for (int i = 0; i < this.Dimension; i++) vec[i] = Init;
        }

        public Vector(double[] Vertex)
        {
            Dimension = Vertex.Length;
            vec = Vertex;
        }

        public double this[int index]
        {
            get => vec[index];
            set { vec[index] = value; }
        }

        public double this[Axis Axis]
        {
            get => vec[(int)Axis];
            set { vec[(int)Axis] = value; }
        }

        /// <summary>
        /// Сумма векторов
        /// </summary>
        /// <param name="lop">Левый операнд</param>
        /// <param name="rop">Правый операнд</param>
        /// <returns>Сумму векторов</returns>
        public static Vector operator +(Vector lop, Vector rop)
        {
            //TODO: проверка размерности
            var result = new Vector(lop.Dimension);
            for (int i = 0; i < lop.Dimension; i++) result[i] = lop[i] + rop[i];
            return result;
        }

        /// <summary>
        /// Разность векторов
        /// </summary>
        /// <param name="lop">Левый операнд</param>
        /// <param name="rop">Правый операнд</param>
        /// <returns>Разность векторов</returns>
        public static Vector operator -(Vector lop, Vector rop)
        {
            //TODO: проверка размерности
            var result = new Vector(lop.Dimension);
            for (int i = 0; i < lop.Dimension; i++) result[i] = lop[i] - rop[i];
            return result;
        }

        /// <summary>
        /// Обращение вектора
        /// </summary>
        /// <param name="op">Исходный вектор</param>
        /// <returns>Обращенный вектор</returns>
        public static Vector operator -(Vector op)
        {
            //TODO: проверка размерности
            var result = new Vector(op.Dimension);
            for (int i = 0; i < op.Dimension; i++) result[i] = -op[i];
            return result;
        }
        
        /// <summary>
        /// Скалярное произведение двух векторов
        /// </summary>
        /// <param name="lop">Левый вектор</param>
        /// <param name="rop">Правый вектор</param>
        /// <returns>Скалярное произведение</returns>
        public static double operator *(Vector lop, Vector rop)
        {
            //TODO: проверка размерности
            double sum = 0;
            for (int i = 0; i < lop.Dimension; i++) sum += lop[i] * rop[i];
            return sum;
        }

        /// <summary>
        /// Векторное произведение
        /// </summary>
        /// <param name="lop">Левый вектор</param>
        /// <param name="rop">Правый вектор</param>
        /// <returns>Результирующий вектор</returns>
        public static Vector operator ^(Vector lop, Vector rop)
        {
            //TODO: проверка размерности
            var result = new Vector(lop.Dimension);
            result[Axis.X] = lop[Axis.Y] * rop[Axis.Z] - lop[Axis.Z] * rop[Axis.Y];
            result[Axis.Y] = lop[Axis.Z] * rop[Axis.X] - lop[Axis.X] * rop[Axis.Z];
            result[Axis.Z] = lop[Axis.X] * rop[Axis.Y] - lop[Axis.Y] * rop[Axis.X];
            return result;
        }

        /// <summary>
        /// Длина вектора
        /// </summary>
        /// <returns>Длину вектора</returns>
        public double GetLength() => Math.Sqrt(this * this);

        /// <summary>
        /// Угол между векторами A и B
        /// </summary>
        /// <param name="A">Вектор А</param>
        /// <param name="B">Вектор B</param>
        /// <returns>Угол в радианах</returns>
        public static double AngleBetween(Vector A, Vector B)
        {
            //TODO: проверка размерности
            double scalar = A * B;
            double lengths = A.GetLength() * B.GetLength();
            if (lengths < double.Epsilon) throw new DivideByZeroException();
            return Math.Acos(scalar / lengths);
        }

        /// <summary>
        /// Копирует элементы заданного вектора в текущий
        /// </summary>
        /// <param name="Source"></param>
        public void CopyFrom(Vector Source)
        {
            //TODO: проверка размерности
            for (int i = 0; i < Source.Dimension; i++) vec[i] = Source[i];
        }

        /// <summary>
        /// Нормализует вектор
        /// </summary>
        public Vector Normalize()
        {
            var result = new Vector(Dimension);
            var l = GetLength();
            if (l < double.Epsilon)
            {
                result[Axis.X] = 0;
                result[Axis.Y] = 0;
                result[Axis.Z] = 0;
            }
            else
                for (int i = 0; i < Dimension; i++) result[i] = vec[i] / l;
            return result;
        }

        public float[] ToArray()
        {
            var result = new float[3];
            for (int i = 0; i < 3; i++) result[i] = (float)vec[i];
            return result;
        }
    }
}
