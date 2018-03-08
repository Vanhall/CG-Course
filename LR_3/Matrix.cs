using System;

namespace LR_3
{
    /// <summary>
    /// Вспомогательный класс, реализующий простейшие действия с матрицами
    /// </summary>
    public class Matrix
    {
        /// <summary>
        /// Размерность
        /// </summary>
        public int Dimension { get; }

        /// <summary>
        /// Элементы матрицы
        /// </summary>
        double[,] mat;

        /// <summary>
        /// Создает квадратную матрицу заданной размерности
        /// </summary>
        /// <param name="Dimension">Размерность</param>
        public Matrix(int Dimension)
        {
            this.Dimension = Dimension;
            mat = new double[Dimension,Dimension];
        }

        /// <summary>
        /// Преобразует текущую матрицу в единичную
        /// </summary>
        public void SetIdentity()
        {
            for (int i = 0; i < Dimension; i++)
                for (int j = 0; j < Dimension; j++)
                    if (i == j) mat[i, j] = 1.0;
                    else mat[i, j] = 0;
        }
        
        /// <summary>
        /// Доступ к элементам по индексу
        /// </summary>
        /// <param name="Row">Строка</param>
        /// <param name="Column">Столбец</param>
        /// <returns>Искомый элемент матрицы</returns>
        public double this[int Row, int Column]
        {
            get => mat[Row, Column];
            set { mat[Row, Column] = value; }
        }

        /// <summary>
        /// Умножение матриц
        /// </summary>
        /// <param name="lop">Левый операнд</param>
        /// <param name="rop">Правый операнд</param>
        /// <returns>Результат перемножения</returns>
        public static Matrix operator *(Matrix lop, Matrix rop)
        {
            var result = new Matrix(lop.Dimension);
            for(int i = 0; i < result.Dimension; i++)
            {
                for (int j = 0; j <result.Dimension; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < result.Dimension; k++)
                    {
                        sum += lop[i, k] * rop[k, j];
                    }
                    result[i, j] = sum;
                }
            }
            return result;
        }
        
        /// <summary>
        /// Умножение матрицы на вектор
        /// </summary>
        /// <param name="mat">Матрица</param>
        /// <param name="vec">Вектор</param>
        /// <returns>Вектор - результат умножения</returns>
        public static Vector operator *(Matrix mat, Vector vec)
        {
            var result = new Vector(vec.Dimension);
            for (int i = 0; i < result.Dimension; i++)
                for (int j = 0; j < result.Dimension; j++)
                    result[i] += mat[i, j] * vec[j];
            return result;
        }

        /// <summary>
        /// Вставляет вектор в заданный столбец
        /// </summary>
        /// <param name="Column">Столбец, в который нужно подставить вектор</param>
        /// <param name="Vec">Вставляемый вектор</param>
        public void InsertAt(int Column, Vector Vec)
        {
            for (int i = 0; i < Vec.Dimension; i++)
                mat[i, Column] = Vec[i];
        }

        /// <summary>
        /// Преобразует текущую матрицу в матрицу вращения
        /// вокруг произвольной оси на заданный угол
        /// </summary>
        /// <param name="angle">Ось вращения</param>
        /// <param name="Axis">Угол в радианах</param>
        public void GenerateRotation(double angle, Vector Axis)
        {
            SetIdentity();
            var c = Math.Cos(angle);
            var s = Math.Sin(angle);
            double x = Axis[Vector.Axis.X];
            double y = Axis[Vector.Axis.Y];
            double z = Axis[Vector.Axis.Z];

            mat[0, 0] = x * x + (1 - x * x) * c;
            mat[0, 1] = x * y * (1 - c) - z * s;
            mat[0, 2] = x * z * (1 - c) + y * s;
            mat[1, 0] = x * y * (1 - c) + z * s;
            mat[1, 1] = y * y + (1 - y * y) * c;
            mat[1, 2] = y * z * (1 - c) - x * s;
            mat[2, 0] = x * z*(1 - c) - y * s;
            mat[2, 1] = y * z * (1 - c) + x * s;
            mat[2, 2] = z * z + (1 - z * z) * c;
        }
    }
}
