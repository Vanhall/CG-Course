using System.ComponentModel;
using System.Drawing;
using SharpGL;

namespace CG_Course
{
    public class PrimitiveContainer
    {
        OpenGL gl;
        public Primitive Current;               /* выбранный (редактируемый)
                                                   в данный момент объект */
        public BindingList<Primitive> Items;    // список объектов
        public Color FillColor;                 // цвет заливки новых объектов

        // Конструктор --------------------------------------------------------
        public PrimitiveContainer(OpenGLControl GLControl, Color Col)
        {
            gl = GLControl.OpenGL;
            FillColor = Col;

            Current = new Primitive(gl, FillColor);
            Items = new BindingList<Primitive>();
            Items.Add(Current);
        }

        // Создание нового объекта --------------------------------------------
        public void Create()
        {
            if (Current != null) Current.Active = false;
            Current = new Primitive(gl, FillColor);
            Items.Add(Current);
        }

        // Удаление текущего объекта ------------------------------------------
        public void Remove(int index)
        {
            if (index >= 0) Items.RemoveAt(index);
            if (Items.Count == 0) Current = null;
        }

        // Смена текущего (редактируемого) объекта ----------------------------
        public void SwitchTo(int index)
        {
            if (index >= 0)
            {
                Current.Active = false;
                Current = Items[index];
                Current.Active = true;
            }
        }

        // Метод отрисовки ----------------------------------------------------
        public void Render()
        {
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT);
            foreach (Primitive P in Items) P.Render();
            gl.Finish();
        }
    }
}
