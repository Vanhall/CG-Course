using System.ComponentModel;
using System.Drawing;
using SharpGL;

namespace LR_2
{
    class HexagonContainer
    {
        OpenGL gl;
        public Hexagon Current;             /* выбранный (редактируемый)
                                               в данный момент объект */
        public BindingList<Hexagon> Items;  // список объектов
        public Color FillColor;             // цвет заливки новых объектов
        public Point Origin;                // Центр новых объектов
        bool renderGrid;                    // Отображать сетку растеризации?
        public RasterGrid Grid;             // Сетка растеризации

        Hexagon.RenderFlags renderMode;     // Флаги режима отрисовки
        public Hexagon.RenderFlags RenderMode
        {
            get => renderMode;
            set
            {
                renderMode = value;
                foreach (Hexagon H in Items) H.RenderMode = value;
            }
        }

        // Конструктор --------------------------------------------------------
        public HexagonContainer(OpenGLControl GLControl, Point Origin, Color Color)
        {
            gl = GLControl.OpenGL;
            FillColor = Color;
            this.Origin = Origin;
            renderGrid = false;
            Grid = new RasterGrid(GLControl, 5);

            Current = new Hexagon(gl, this.Origin, FillColor, Grid);
            Items = new BindingList<Hexagon> { Current };
        }

        // Создание нового объекта --------------------------------------------
        public void Create()
        {
            if (Current != null)
            {
                Hexagon.RenderFlags oldRenderMode = Current.RenderMode;
                Current.RenderMode = renderMode;
                Current = new Hexagon(gl, Origin, FillColor, Grid);
                Items.Add(Current);
                Current.RenderMode = oldRenderMode;
            }
            else
            {
                Current = new Hexagon(gl, Origin, FillColor, Grid);
                Current.RenderMode = renderMode;
                Items.Add(Current);
            }
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
                Hexagon.RenderFlags oldRenderMode = Current.RenderMode;
                Current.RenderMode = renderMode;
                Current = Items[index];
                Current.RenderMode = oldRenderMode;
            }
        }

        // Перерасчет растеризации объектов ----------------------------------
        public void Rasterize(bool RenderGrid)
        {
            renderGrid = RenderGrid;
            foreach (Hexagon H in Items) H.Rasterize();
        }

        // Метод отрисовки ----------------------------------------------------
        public void Render()
        {
            foreach (Hexagon H in Items) H.Render();
            if (renderGrid) Grid.Render();
        }
    }
}
