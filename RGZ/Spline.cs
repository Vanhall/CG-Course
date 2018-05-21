using System.Collections.Generic;
using System.Linq;

namespace RGZ
{
    /// <summary>
    /// Закрытый B-сплайн.
    /// </summary>
    public class Spline
    {
        /// <summary>
        /// Управляющие точки.
        /// </summary>
        Point2D[] ControlPoints;

        /// <summary>
        /// Вектор узлов.
        /// </summary>
        double[] Nodes;

        /// <summary>
        /// Степень сплайна.
        /// </summary>
        int p;

        /// <summary>
        /// Вспомогательный параметр (Кол-во КТ - 1).
        /// </summary>
        int n;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="CP">Массив КТ.</param>
        /// <param name="Degree">Степень сплайна.</param>
        public Spline(Point2D[] CP, int Degree)
        {
            ControlPoints = CP;
            p = Degree;
            n = CP.Length - 1;

            Nodes = new double[n + p + 2];

            int t = 0, i = 0;
            for (; i < p + 1; i++) Nodes[i] = t;
            for (; i < n + 1; i++) Nodes[i] = ++t;
            t++;
            for (; i < n + p + 2; i++) Nodes[i] = t;
        }

        /// <summary>
        /// Функция расчета сплайна.
        /// </summary>
        /// <param name="Steps">Количество шагов на интервале.</param>
        /// <returns>Массив точек сплайна с заданным шагом параметра t.</returns>
        public float[] CalcPoints(int Steps)
        {
            var result = new List<float>();

            for (int k = p; k < n + 1; k++)
            {
                double step = (Nodes[k + 1] - Nodes[k]) / Steps;
                double t0 = Nodes[k];
                for (int i = 0; i < Steps; i++)
                    result.AddRange(DeBoor(k, t0 + i * step).ToArray()); 
            }
            result.AddRange(ControlPoints.Last().ToArray());

            return result.ToArray();
        }

        /// <summary>
        /// Улучшенный алгоритм Кокса - де Бура.
        /// </summary>
        /// <param name="k">Индекс интервала [t_k, t_k+1), которому принадлежит t.</param>
        /// <param name="t">Параметр сплайна. t -> [t_k, t_k+1).</param>
        /// <returns>Точку сплайна, соответствующую значению параметра t.</returns>
        Point2D DeBoor(int k, double t)
        {
            var D = new Point2D[p + 1];
            for (int i = 0; i < p + 1; i++) D[i] = ControlPoints[i + k - p];

            for (int r = 1; r < p + 1; r++)
                for (int j = p; j > r - 1; j--)
                {
                    double alpha = (t - Nodes[j + k - p]) / (Nodes[j + 1 + k - r] - Nodes[j + k - p]);
                    D[j] = D[j - 1] * (1.0 - alpha) + D[j] * alpha;
                }
            return D[p];
        }
    }
}
 