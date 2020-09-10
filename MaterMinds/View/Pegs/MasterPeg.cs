using System.Windows.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
        //protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
        //{
        //        base.OnGiveFeedback(e);
        //    if (IsMoveble)
        //    {

        //        // These Effects values are set in the drop target's
        //        // DragOver event handler.
                
        //        if (e.Effects.HasFlag(DragDropEffects.Move))
        //        {
        //            Mouse.SetCursor(Cursors.Pen);
        //        }
        //        else
        //        {
        //            Mouse.SetCursor(Cursors.No);
        //        }
        //    }
        //        e.Handled = true;
        //}

    }
}
