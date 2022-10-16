using System.Data;
using System.Reflection;
using System.Text;
using DGVWF;

namespace pis.infrasrtucture.dgvf
{
    public class DataGridViewWithFilter : DataGridView
    {
        private readonly List<FilterStatus> _filter = new();
        private readonly TextBox _textBoxCtrl = new();
        private readonly List<string> _filterValues = new();
        private readonly DateTimePicker _dateTimeCtrl = new();
        private readonly CheckedListBox _checkCtrl = new();
        private readonly Button _applyButtonCtrl = new();
        private readonly Button _clearFilterCtrl = new();
        private readonly ToolStripDropDown _popup = new();

        private string _strFilter = "";
        private const string ButtonCtrlText = "Apply";
        private const string ClearFilterCtrlText = "Clear filters";
        private const string CheckCtrlAllText = "<All>";
        private const string SpaceText = "<Space>";

        // Текущий индекс ячейки
        private int ColumnIndex { get; set; }

        protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
            var header = new DataGridFilterHeader();
            header.FilterButtonClicked += header_FilterButtonClicked!;
            e.Column.HeaderCell = header;
            _filterValues.Add("");
            base.OnColumnAdded(e);
        }

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

            ColumnIndex = e.ColumnIndex;

            _textBoxCtrl.Text = _filterValues[ColumnIndex];
            _checkCtrl.Items.Clear();

            //textBoxCtrl.Text = textBoxCtrlText;
            _textBoxCtrl.Size = new Size(widthTool, 30);
            _textBoxCtrl.TextChanged -= textBoxCtrl_TextChanged!;
            _textBoxCtrl.TextChanged += textBoxCtrl_TextChanged!;

            _dateTimeCtrl.Size = new Size(widthTool, 30);
            _dateTimeCtrl.Format = DateTimePickerFormat.Custom;
            _dateTimeCtrl.CustomFormat = "dd.MM.yyyy";
            _dateTimeCtrl.TextChanged -= DateTimeCtrl_TextChanged!;
            _dateTimeCtrl.TextChanged += DateTimeCtrl_TextChanged!;

            _checkCtrl.ItemCheck -= CheckCtrl_ItemCheck!;
            _checkCtrl.ItemCheck += CheckCtrl_ItemCheck!;
            _checkCtrl.CheckOnClick = true;

            GetChkFilter();

            _checkCtrl.MaximumSize = new Size(widthTool, GetHeightTable() - 120);
            _checkCtrl.Size = new Size(widthTool, (_checkCtrl.Items.Count + 1) * 18);

            _applyButtonCtrl.Text = ButtonCtrlText;
            _applyButtonCtrl.Size = new Size(widthTool, 30);
            _applyButtonCtrl.Click -= ApplyButtonCtrl_Click!;
            _applyButtonCtrl.Click += ApplyButtonCtrl_Click!;

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

            ToolStripControlHost host2 = new ToolStripControlHost(_checkCtrl);
            host2.Margin = Padding.Empty;
            host2.Padding = Padding.Empty;
            host2.AutoSize = false;
            host2.Size = _checkCtrl.Size;

            ToolStripControlHost host3 = new ToolStripControlHost(_applyButtonCtrl);
            host3.Margin = Padding.Empty;
            host3.Padding = Padding.Empty;
            host3.AutoSize = false;
            host3.Size = _applyButtonCtrl.Size;

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

            switch (Columns[ColumnIndex].ValueType.ToString())
            {
                case "System.DateTime":
                    _popup.Items.Add(host5);
                    break;
                default:
                    _popup.Items.Add(host1);
                    break;
            }

            _popup.Items.Add(host2);
            _popup.Items.Add(host3);
            _popup.Items.Add(host4);

            _popup.Show(this, e.ButtonRectangle.X, e.ButtonRectangle.Bottom);
            host2.Focus();
        }

        // Выбор всех
        private void CheckCtrl_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {
                if (e.NewValue == CheckState.Checked)
                {
                    for (int i = 1; i < _checkCtrl.Items.Count; i++)
                        _checkCtrl.SetItemChecked(i, true);
                }
                else
                {
                    for (int i = 1; i < _checkCtrl.Items.Count; i++)
                        _checkCtrl.SetItemChecked(i, false);
                }
            }
        }

        // Очистить фильтры
        private void ClearFilterCtrl_Click(object sender, EventArgs e)
        {
            _filter.Clear();
            _strFilter = "";
            ApllyFilter();
            _popup.Close();
        }

        // Событие при изменении текста в TextBox
        private void textBoxCtrl_TextChanged(object sender, EventArgs e)
        {
            var table = DataSource as DataTable;
            table!.DefaultView.RowFilter = CreateFilter();
        }

        private string CreateFilter()
        {
            var builder = new StringBuilder();
            var index = 0;
            _filterValues[ColumnIndex] = _textBoxCtrl.Text;
            foreach (var value in _filterValues)
            {
                if (value != null && value.Length > 0)
                {
                    var strVal = Columns[index].ValueType.ToString() switch
                    {
                        "System.String" => $"convert([{Columns[index].Name}], '{Columns[index].ValueType}') LIKE '%{value}%'",
                        _ => throw new ArgumentException("Тип столбца не может быть филтрован")
                    };
                    builder.Append(strVal);
                    builder.Append(" AND ");
                }
                index++;
            }
            if (builder.Length > 4)
            {
                builder.Remove(builder.Length - 5, 4);
            }
            return builder.ToString();
        }

        private void DateTimeCtrl_TextChanged(object sender, EventArgs e)
        {
            (DataSource as DataTable).DefaultView.RowFilter = string.Format(
                "convert([" + Columns[ColumnIndex].Name + "], 'System.String') LIKE '%{0}%'", _dateTimeCtrl.Text);
        }

        // Событие кнопки применить
        private void ApplyButtonCtrl_Click(object sender, EventArgs e)
        {
            _strFilter = "";
            SaveChkFilter();
            ApllyFilter();
            _popup.Close();
        }

        // Получаем данные из выбранной колонки 
        private List<string> GetDataColumns(int e)
        {
            List<string> valueCellList = new List<string>();
            string value;

            // Посик данных в столбце, исключая повторения
            foreach (DataGridViewRow row in Rows)
            {
                value = row.Cells[e].Value.ToString();
                if (value == "") value = SpaceText;

                if (!valueCellList.Contains(value))
                    valueCellList.Add(value);
            }

            return valueCellList;
        }

        // Получаем высоту таблицы
        private int GetHeightTable()
        {
            return Height;
        }

        // Получаем ширину выбранной колонки
        private int GetWhithColumn(int e)
        {
            return Columns[e].Width;
        }

        // Запомнить чекбоксы фильтра
        private void SaveChkFilter()
        {
            string col = Columns[ColumnIndex].Name;
            string itemChk;
            bool statChk;

            _filter.RemoveAll(x => x.ColumnName == col);

            for (int i = 1; i < _checkCtrl.Items.Count; i++)
            {
                itemChk = _checkCtrl.Items[i].ToString();
                statChk = _checkCtrl.GetItemChecked(i);
                _filter.Add(new FilterStatus() { ColumnName = col, ValueString = itemChk, Check = statChk });
            }
        }

        // Загрузить чекбоксы
        private void GetChkFilter()
        {
            List<FilterStatus> checkList = new List<FilterStatus>();
            List<FilterStatus> checkListSort = new List<FilterStatus>();

            // Посик сохранённых данных
            foreach (FilterStatus val in _filter)
            {
                if (Columns[ColumnIndex].Name == val.ColumnName)
                {
                    if (val.ValueString == "") val.ValueString = SpaceText;
                    checkList.Add(new FilterStatus()
                        { ColumnName = "", ValueString = val.ValueString, Check = val.Check });
                }
            }

            // Поиск данных в таблице
            foreach (string valueCell in GetDataColumns(ColumnIndex))
            {
                int index = checkList.FindIndex(item => item.ValueString == valueCell);
                if (index == -1)
                {
                    checkList.Add(new FilterStatus { ValueString = valueCell, Check = true });
                }
            }

            _checkCtrl.Items.Add(CheckCtrlAllText, CheckState.Indeterminate);
            // Сортировка
            switch (Columns[ColumnIndex].ValueType.ToString())
            {
                case "System.Int32":
                    checkListSort = checkList.OrderBy(x => Int32.Parse(x.ValueString)).ToList();
                    foreach (FilterStatus val in checkListSort)
                    {
                        if (val.Check == true)
                            _checkCtrl.Items.Add(val.ValueString, CheckState.Checked);
                        else
                            _checkCtrl.Items.Add(val.ValueString, CheckState.Unchecked);
                    }

                    break;
                case "System.DateTime":
                    checkListSort = checkList.OrderBy(x => DateTime.Parse(x.ValueString)).ToList();
                    foreach (FilterStatus val in checkListSort)
                    {
                        if (val.Check == true)
                            _checkCtrl.Items.Add(DateTime.Parse(val.ValueString).ToString("dd.MM.yyyy"),
                                CheckState.Checked);
                        else
                            _checkCtrl.Items.Add(DateTime.Parse(val.ValueString).ToString("dd.MM.yyyy"),
                                CheckState.Unchecked);
                    }

                    break;
                default:
                    checkListSort = checkList.OrderBy(x => x.ValueString.ToString()).ToList();
                    foreach (FilterStatus val in checkListSort)
                    {
                        if (val.Check == true)
                            _checkCtrl.Items.Add(val.ValueString, CheckState.Checked);
                        else
                            _checkCtrl.Items.Add(val.ValueString, CheckState.Unchecked);
                    }

                    break;
            }
        }

        // Применить фильтр
        private void ApllyFilter()
        {
            foreach (FilterStatus val in _filter)
            {
                if (val.ValueString == SpaceText)
                    val.ValueString = "";

                if (val.Check == false)
                {
                    // Исключение если bool              
                    string valueFilter = "'" + val.ValueString + "' ";
                    if (valueFilter == "True")
                        valueFilter = "1";
                    if (valueFilter == "False")
                        valueFilter = "0";

                    if (_strFilter.Length == 0)
                        _strFilter = _strFilter + ("[" + val.ColumnName + "] <> " + valueFilter);
                    else
                        _strFilter = _strFilter + (" AND [" + val.ColumnName + "] <> " + valueFilter);
                }
            }

            (DataSource as DataTable).DefaultView.RowFilter = _strFilter;
        }

        public void FillDataGrid<T>(IEnumerable<T> sourse)
        {
            var dt = new DataTable();
            var propertys = typeof(T).GetProperties();
            foreach (var prop in propertys)
                dt.Columns.Add(prop.Name, prop.PropertyType);
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