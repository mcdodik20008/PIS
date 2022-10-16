using System.Windows.Forms.VisualStyles;

namespace DGVWF
{
    public class DataGridFilterHeader : DataGridViewColumnHeaderCell
    {
        ComboBoxState currentState = ComboBoxState.Normal;
        
        Point cellLocation;
        
        Rectangle buttonRect;

        public event EventHandler<ColumnFilterClickedEventArg> FilterButtonClicked;

        protected override void OnDataGridViewChanged()
        {
            try
            {
                Padding dropDownPadding = new Padding(0, 0, 20, 0);
                Style.Padding = Padding.Add(InheritedStyle.Padding, dropDownPadding);
            }
            catch { }
            base.OnDataGridViewChanged();
        }

        protected override void Paint(Graphics graphics,
                                      Rectangle clipBounds,
                                      Rectangle cellBounds,
                                      int rowIndex,
                                      DataGridViewElementStates dataGridViewElementState,
                                      object value,
                                      object formattedValue,
                                      string errorText,
                                      DataGridViewCellStyle cellStyle,
                                      DataGridViewAdvancedBorderStyle advancedBorderStyle,
                                      DataGridViewPaintParts paintParts)
        {

            base.Paint(graphics, clipBounds,
                       cellBounds, rowIndex,
                       dataGridViewElementState, value,
                       formattedValue, errorText,
                       cellStyle, advancedBorderStyle, paintParts);

            int width = 20; // 20 px
            buttonRect = new Rectangle(cellBounds.X + cellBounds.Width - width, cellBounds.Y, width, cellBounds.Height);
            cellLocation = cellBounds.Location;
            ComboBoxRenderer.DrawDropDownButton(graphics, buttonRect, currentState);
        }
        protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
        {
            if (IsMouseOverButton(e.Location))
                currentState = ComboBoxState.Pressed;
            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(DataGridViewCellMouseEventArgs e)
        {
            if (IsMouseOverButton(e.Location))
            {
                currentState = ComboBoxState.Normal;
                OnFilterButtonClicked();
            }
            base.OnMouseUp(e);
        }
        private bool IsMouseOverButton(Point e)
        {
            Point p = new Point(e.X + cellLocation.X, e.Y + cellLocation.Y);
            if (p.X >= buttonRect.X && p.X <= buttonRect.X + buttonRect.Width &&
                p.Y >= buttonRect.Y && p.Y <= buttonRect.Y + buttonRect.Height)
            {
                return true;
            }
            return false;
        }
        protected virtual void OnFilterButtonClicked()
        {
            if (FilterButtonClicked != null)
            {
                FilterButtonClicked(this, new ColumnFilterClickedEventArg(ColumnIndex, buttonRect));
            }
        }
    }
}