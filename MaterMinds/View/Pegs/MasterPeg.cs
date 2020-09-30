using System.Windows.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Resources;
using System;

namespace MaterMinds
{
    public class MasterPeg : UserControl
    {
        public int ColorIndex { get; set; }
        public Brush Color { get; set; }

        public MasterPeg()
        {
            Height = 30;
            Name = "Peg";
            Width = 30;

        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DataObject data = new DataObject();
                data.SetData("Object", this);
                DragDrop.DoDragDrop(this, data, DragDropEffects.Move);
            }
        }
        protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
        {
            StreamResourceInfo sriCurs = GetPegToStream(e);
            if (e.Effects.HasFlag(DragDropEffects.Move))
            {
                Mouse.SetCursor(new Cursor(sriCurs.Stream));
            }
            e.Handled = true;
        }
        private StreamResourceInfo GetPegToStream(GiveFeedbackEventArgs e)
        {
            if (e.OriginalSource is RedPeg)
            {
                StreamResourceInfo sriCurs = Application.GetResourceStream(new Uri("Resources/Cursor/RedCircle.cur", UriKind.Relative));
                return sriCurs;
            }
            else if (e.OriginalSource is YellowPeg)
            {
                StreamResourceInfo sriCurs = Application.GetResourceStream(new Uri("Resources/Cursor/YellowCircle.cur", UriKind.Relative));
                return sriCurs;
            }
            else if (e.OriginalSource is GreenPeg)
            {
                StreamResourceInfo sriCurs = Application.GetResourceStream(new Uri("Resources/Cursor/GreenCircle.cur", UriKind.Relative));
                return sriCurs;
            }
            else if (e.OriginalSource is BluePeg)
            {
                StreamResourceInfo sriCurs = Application.GetResourceStream(new Uri("Resources/Cursor/BlueCircle.cur", UriKind.Relative));
                return sriCurs;
            }
            else if (e.OriginalSource is PurplePeg)
            {
                StreamResourceInfo sriCurs = Application.GetResourceStream(new Uri("Resources/Cursor/PurpleCircle.cur", UriKind.Relative));
                return sriCurs;
            }
            else
            {
                StreamResourceInfo sriCurs = Application.GetResourceStream(new Uri("Resources/Cursor/OrangeCircle.cur", UriKind.Relative));
                return sriCurs;
            }
        }
    }
}
