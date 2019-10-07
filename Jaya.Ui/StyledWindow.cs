using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Styling;
using System;

namespace Jaya.Ui
{
    public class StyledWindow : Window, IStyleable
    {
        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);

            SetupWindowEdge(e, "PART_RightGrip", StandardCursorType.RightSide, WindowEdge.East);
            SetupWindowEdge(e, "PART_LeftGrip", StandardCursorType.LeftSide, WindowEdge.West);
            SetupWindowEdge(e, "PART_TopGrip", StandardCursorType.TopSide, WindowEdge.North);
            SetupWindowEdge(e, "PART_BottomGrip", StandardCursorType.BottomSize, WindowEdge.South);
            SetupWindowEdge(e, "PART_TopLeftGrip", StandardCursorType.TopLeftCorner, WindowEdge.NorthWest);
            SetupWindowEdge(e, "PART_TopRightGrip", StandardCursorType.TopRightCorner, WindowEdge.NorthEast);
            SetupWindowEdge(e, "PART_BottomLeftGrip", StandardCursorType.BottomLeftCorner, WindowEdge.SouthWest);
            SetupWindowEdge(e, "PART_BottomRightGrip", StandardCursorType.BottomRightCorner, WindowEdge.SouthEast);

            GetControl<TextBlock>(e, "PART_TitleBar").PointerPressed += (object sender, PointerPressedEventArgs args) => PlatformImpl?.BeginMoveDrag();

        }

        Type IStyleable.StyleKey => typeof(StyledWindow);

        void SetupWindowEdge(TemplateAppliedEventArgs e, string name, StandardCursorType cursor, WindowEdge edge)
        {
            var control = GetControl<Border>(e, name);
            control.Cursor = new Cursor(cursor);
            control.PointerPressed += (object sender, PointerPressedEventArgs args) => PlatformImpl?.BeginResizeDrag(edge);
        }

        T GetControl<T>(TemplateAppliedEventArgs e, string name) where T : Control
        {
            return e.NameScope.Find<T>(name);
        }
    }
}
