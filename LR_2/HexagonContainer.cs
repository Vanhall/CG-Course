using System.ComponentModel;
using System.Drawing;
using SharpGL;

namespace LR_2
{
    class HexagonContainer
    {
        OpenGL gl;
        public Hexagon Current;              /* выбранный (редактируемый)
                                                в данный момент объект */
        public BindingList<Hexagon> Items;   // список объектов
        public Color FillColor;              // цвет заливки новых объектов
        public Point Origin;
        public bool Rasterize;
        RasterGrid Grid;

        // Конструктор --------------------------------------------------------
        public HexagonContainer(OpenGLControl GLControl, Point Origin, Color Color)
        {
            gl = GLControl.OpenGL;
            FillColor = Color;
            this.Origin = Origin;
            Rasterize = false;
            Grid = new RasterGrid(GLControl, 3);

            Current = new Hexagon(gl, this.Origin, FillColor, Grid);
            Items = new BindingList<Hexagon> { Current };
        }

        // Создание нового объекта --------------------------------------------
        public void Create()
        {
            Current = new Hexagon(gl, Origin, FillColor, Grid);
            Items.Add(Current);
        }

        // Удаление текущего объекта ------------------------------------------
        public void Remove(int index)
        {
            if (index >= 0 && index < Items.Count) Items.RemoveAt(index);
            if (Items.Count == 0) Current = null;
        }

        // Смена текущего (редактируемого) объекта ----------------------------
        public void SwitchTo(int index)
        {
            if (index >= 0 && index < Items.Count)
            {
                Current.RenderMode = Hexagon.RenderFlags.Hexagon;
                Current = Items[index];
            }
        }

        // Метод отрисовки ----------------------------------------------------
        public void Render()
        {
            foreach (Hexagon H in Items) H.Render();
            if (Rasterize) Grid.Render();
        }
    }
}
