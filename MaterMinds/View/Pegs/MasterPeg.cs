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
        public bool IsMoveble { get; set; }
        public string Color { get; set; }

        public MasterPeg()
        {
            Height = 30;
            Name = "Peg";
            Width = 30;
            IsMoveble = true;

        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (IsMoveble)
            {

                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    DataObject data = new DataObject();
                    data.SetData("Object", this);
                    DragDrop.DoDragDrop(this, data, DragDropEffects.Move);
                }
            }
        }
        /// <summary>
        /// Change cursor to peg depending on color.
        /// Sorce: https://wpf.2000things.com/tag/cursor/ 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
        {
            if (e.Effects.HasFlag(DragDropEffects.Move) && e.OriginalSource is RedPeg)
            {
                StreamResourceInfo sriCurs = Application.GetResourceStream(new Uri("Resources/Cursor/RedCircle.cur", UriKind.Relative));
                Mouse.SetCursor(new Cursor(sriCurs.Stream));
            }
            else if (e.Effects.HasFlag(DragDropEffects.Move) && e.OriginalSource is YellowPeg)
            {
                StreamResourceInfo sriCurs = Application.GetResourceStream(new Uri("Resources/Cursor/YellowCircle.cur", UriKind.Relative));
                Mouse.SetCursor(new Cursor(sriCurs.Stream));
            }
            else if (e.Effects.HasFlag(DragDropEffects.Move) && e.OriginalSource is GreenPeg)
            {
                StreamResourceInfo sriCurs = Application.GetResourceStream(new Uri("Resources/Cursor/GreenCircle.cur", UriKind.Relative));
                Mouse.SetCursor(new Cursor(sriCurs.Stream));
            }
            else if (e.Effects.HasFlag(DragDropEffects.Move) && e.OriginalSource is BluePeg)
            {
                StreamResourceInfo sriCurs = Application.GetResourceStream(new Uri("Resources/Cursor/BlueCircle.cur", UriKind.Relative));
                Mouse.SetCursor(new Cursor(sriCurs.Stream));
            }
            else if (e.Effects.HasFlag(DragDropEffects.Move) && e.OriginalSource is PurplePeg)
            {
                StreamResourceInfo sriCurs = Application.GetResourceStream(new Uri("Resources/Cursor/PurpleCircle.cur", UriKind.Relative));
                Mouse.SetCursor(new Cursor(sriCurs.Stream));
            }
            else if (e.Effects.HasFlag(DragDropEffects.Move) && e.OriginalSource is OrangePeg)
            {
                StreamResourceInfo sriCurs = Application.GetResourceStream(new Uri("Resources/Cursor/OrangeCircle.cur", UriKind.Relative));
                Mouse.SetCursor(new Cursor(sriCurs.Stream));
            }
            else
            {
                Mouse.SetCursor(Cursors.No);
            }
            e.Handled = true;
        }

    }
}
