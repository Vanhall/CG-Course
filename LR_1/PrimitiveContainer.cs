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
        public PrimitiveContainer(OpenGLControl GLControl, Color Color)
        {
            gl = GLControl.OpenGL;
            FillColor = Color;

            Current = new Primitive(gl, FillColor);
            Items = new BindingList<Primitive> { Current };
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
            if (index >= 0 && index < Items.Count) Items.RemoveAt(index);
            if (Items.Count == 0) Current = null;
        }

        // Смена текущего (редактируемого) объекта ----------------------------
        public void SwitchTo(int index)
        {
            if (index >= 0 && index < Items.Count)
            {
                Current.Active = false;
                Current = Items[index];
                Current.Active = true;
            }
        }

        // Метод отрисовки ----------------------------------------------------
        public void Render()
        {
            foreach (Primitive P in Items) P.Render();
        }
    }
}
