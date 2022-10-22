﻿using System.Data;
using System.Reflection;
using DGVWF;

namespace pis.infrasrtucture.dgvf
{
    public class DataGridViewWithFilter : DataGridView
    {
        private readonly List<FilterStatus> _filter = new();
        private readonly List<FilterColumn> _filterColumns = new();
        private readonly TextBox _textBoxCtrl = new();
        private readonly DateTimePicker _dateTimeCtrl = new();
        private readonly Button _clearFilterCtrl = new();
        private readonly ToolStripDropDown _popup = new();
        private readonly CheckedListBox CheckCtrl = new();
        private const string ClearFilterCtrlText = "Clear filters";

        // Индекс ячейки в котором открыто окно
        private int _columnIndex;

        protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
            var header = new DataGridFilterHeader();
            header.FilterButtonClicked += header_FilterButtonClicked!;
            e.Column.HeaderCell = header;
            base.OnColumnAdded(e);
        }

        // TODO: Выпилить?
        // Скролл после сортировки
        public override void Sort(DataGridViewColumn dataGridViewColumn,
            System.ComponentModel.ListSortDirection direction)
        {
            int scrl = HorizontalScrollBar.Value;
            int scrlOffset = HorizontalScrollingOffset;
            base.Sort(dataGridViewColumn, direction);
            HorizontalScrollBar.Value = scrl;
            HorizontalScrollingOffset = scrlOffset;
        }

        // Событие кнопки фильтрации
        private void header_FilterButtonClicked(object sender, ColumnFilterClickedEventArg e)
        {
            int widthTool = GetWhithColumn(e.ColumnIndex) + 50;
            if (widthTool < 130) widthTool = 130;

            _columnIndex = e.ColumnIndex;

            _textBoxCtrl.Text = _filterColumns[_columnIndex].value;
            _textBoxCtrl.Size = new Size(widthTool, 30);
            _textBoxCtrl.TextChanged -= textBoxCtrl_TextChanged!;
            _textBoxCtrl.TextChanged += textBoxCtrl_TextChanged!;

            _dateTimeCtrl.Size = new Size(widthTool, 30);
            _dateTimeCtrl.Format = DateTimePickerFormat.Custom;
            _dateTimeCtrl.CustomFormat = "dd.MM.yyyy";
            _dateTimeCtrl.TextChanged -= DateTimeCtrl_TextChanged!;
            _dateTimeCtrl.TextChanged += DateTimeCtrl_TextChanged!;
            
            _clearFilterCtrl.Text = ClearFilterCtrlText;
            _clearFilterCtrl.Size = new Size(widthTool, 30);
            _clearFilterCtrl.Click -= ClearFilterCtrl_Click!;
            _clearFilterCtrl.Click += ClearFilterCtrl_Click!;

            _popup.Items.Clear();
            _popup.AutoSize = true;
            _popup.Margin = Padding.Empty;
            _popup.Padding = Padding.Empty;

            ToolStripControlHost host1 = new ToolStripControlHost(_textBoxCtrl);
            host1.Margin = Padding.Empty;
            host1.Padding = Padding.Empty;
            host1.AutoSize = false;
            host1.Size = _textBoxCtrl.Size;
            
            ToolStripControlHost host2 = new ToolStripControlHost(CheckCtrl);
            host2.Margin = Padding.Empty;
            host2.Padding = Padding.Empty;
            host2.AutoSize = false;
            host2.Size = CheckCtrl.Size;

            ToolStripControlHost host4 = new ToolStripControlHost(_clearFilterCtrl);
            host4.Margin = Padding.Empty;
            host4.Padding = Padding.Empty;
            host4.AutoSize = false;
            host4.Size = _clearFilterCtrl.Size;

            ToolStripControlHost host5 = new ToolStripControlHost(_dateTimeCtrl);
            host5.Margin = Padding.Empty;
            host5.Padding = Padding.Empty;
            host5.AutoSize = false;
            host5.Size = _dateTimeCtrl.Size;

            switch (Columns[_columnIndex].ValueType.ToString())
            {
                case "System.DateTime":
                    _popup.Items.Add(host5);
                    break;
                default:
                    _popup.Items.Add(host1);
                    break;
            }
            
            _popup.Items.Add(host4);

            _popup.Show(this, e.ButtonRectangle.X, e.ButtonRectangle.Bottom);
        }

        // Очистить фильтры
        private void ClearFilterCtrl_Click(object sender, EventArgs e)
        {
            _filter.Clear();
            _filterColumns[_columnIndex].value = "";
            _textBoxCtrl.Text = "";
            _popup.Close();
            updateTableWithFilter();
        }

        // Событие при изменении текста в TextBox
        private void textBoxCtrl_TextChanged(object sender, EventArgs e)
        {
            updateTableWithFilter();
        }

        private void updateTableWithFilter()
        {
            var table = DataSource as DataTable;
            _filterColumns[_columnIndex].value = _textBoxCtrl.Text;
           // table!.DefaultView.RowFilter = _filterColumns.AsString(); 

        }

        // TODO: переписать
        private void DateTimeCtrl_TextChanged(object sender, EventArgs e)
        {
            var filter = string.Format(
                "convert([" + Columns[_columnIndex].Name + "], 'System.String') LIKE '%{0}%'", _dateTimeCtrl.Text);
            _filterColumns[_columnIndex].value = filter;
        }

        // Получаем ширину выбранной колонки
        private int GetWhithColumn(int e) => Columns[e].Width;

        public void FillDataGrid<T>(IEnumerable<T> sourse)
        {
            var dt = new DataTable();
            Columns.Clear();
            var propertys = typeof(T).GetProperties();
            foreach (var prop in propertys)
            {
                dt.Columns.Add(prop.Name, prop.PropertyType);
               _filterColumns.Add(new FilterColumn(prop.Name, prop.PropertyType, ""));
            }
            foreach (var entity in sourse)
            {
                var values = GetEntityValues(entity, propertys);
                dt.Rows.Add(values.ToArray());
            }
            DataSource = dt;
        }

        private object[] GetEntityValues<T>(T entity, PropertyInfo[] propertys)
        {
            var values = new object[propertys.Length];
            var index = 0;
            foreach (var prop in propertys)
                values[index++] = prop.GetValue(entity);
            return values;
        }

        private DataTable GetFullTable()
        {
            var dt = new DataTable();
            dt.Columns.Add("Number", typeof(int));
            dt.Columns.Add("Name");
            dt.Columns.Add("Ver");
            dt.Columns.Add("Date", typeof(DateTime));
            dt.Rows.Add("1", "Ubuntu", "11.10", "13.10.2011");
            dt.Rows.Add("2", "Ubuntu LTS", "12.04", "18.10.2012");
            dt.Rows.Add("3", "Ubuntu", "12.10", "18.10.2012");
            dt.Rows.Add("4", "Ubuntu", "13.04", "25.04.2012");
            dt.Rows.Add("5", "Ubuntu", "13.10", "17.10.2013");
            dt.Rows.Add("6", "Ubuntu LTS", "14.04", "23.04.2014");
            dt.Rows.Add("7", "Ubuntu", "14.10", "23.10.2014");
            dt.Rows.Add("8", "Ubuntu", "15.04", "23.04.2015");
            dt.Rows.Add("9", "Ubuntu", "15.04", "23.04.2015");
            dt.Rows.Add("11", "fbfghf", "11.10", "13.10.2011");
            dt.Rows.Add("12", "fbfghf LTS", "12.04", "18.10.2012");
            dt.Rows.Add("13", "fbfghf", "12.10", "18.10.2012");
            dt.Rows.Add("14", "fbfghf", "13.04", "25.04.2012");
            dt.Rows.Add("15", "fbfghf", "13.10", "17.10.2013");
            dt.Rows.Add("16", "fbfghf LTS", "14.04", "23.04.2014");
            dt.Rows.Add("17", "fbfghf", "14.10", "23.10.2014");
            dt.Rows.Add("18", "fbfghf", "15.04", "23.04.2015");
            dt.Rows.Add("19", "fbfghf", "15.04", "23.04.2015");
            dt.Rows.Add("21", "Ubuntu", "11.10", "13.10.2011");
            dt.Rows.Add("22", "Ubuntu LTS", "12.04", "18.10.2012");
            dt.Rows.Add("23", "Ubuntu", "12.10", "18.10.2012");
            dt.Rows.Add("24", "Ubuntu", "13.04", "25.04.2012");
            dt.Rows.Add("25", "Ubuntu", "13.10", "17.10.2013");
            dt.Rows.Add("26", "Ubuntu LTS", "14.04", "23.04.2014");
            dt.Rows.Add("27", "Ubuntu", "14.10", "23.10.2014");
            dt.Rows.Add("28", "Ubuntu", "15.04", "23.04.2015");
            dt.Rows.Add("29", "Ubuntu", "15.04", "23.04.2015");
            return dt;
        }
    }
}