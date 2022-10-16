using System.Data;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace DGVWF
{
    public class DataGridViewWithFilter : DataGridView
    {
        List<FilterStatus> Filter = new List<FilterStatus>();
        TextBox textBoxCtrl = new TextBox();
        DateTimePicker DateTimeCtrl = new DateTimePicker();
        CheckedListBox CheckCtrl = new CheckedListBox();
        Button ApplyButtonCtrl = new Button();
        Button ClearFilterCtrl = new Button();
        ToolStripDropDown popup = new ToolStripDropDown();

        string StrFilter = "";
        string ButtonCtrlText = "Apply";
        string ClearFilterCtrlText = "Clear filters";
        string CheckCtrlAllText = "<All>";
        string SpaceText = "<Space>";

        // Текущий индекс ячейки
        private int columnIndex { get; set; }

        protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
            var header = new DataGridFilterHeader();
            header.FilterButtonClicked += new EventHandler<ColumnFilterClickedEventArg>(header_FilterButtonClicked);
            e.Column.HeaderCell = header;
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
        void header_FilterButtonClicked(object sender, ColumnFilterClickedEventArg e)
        {
            int widthTool = GetWhithColumn(e.ColumnIndex) + 50;
            if (widthTool < 130) widthTool = 130;

            columnIndex = e.ColumnIndex;

            textBoxCtrl.Clear();
            CheckCtrl.Items.Clear();

            //textBoxCtrl.Text = textBoxCtrlText;
            textBoxCtrl.Size = new Size(widthTool, 30);
            textBoxCtrl.TextChanged -= textBoxCtrl_TextChanged;
            textBoxCtrl.TextChanged += textBoxCtrl_TextChanged;

            DateTimeCtrl.Size = new Size(widthTool, 30);
            DateTimeCtrl.Format = DateTimePickerFormat.Custom;
            DateTimeCtrl.CustomFormat = "dd.MM.yyyy";
            DateTimeCtrl.TextChanged -= DateTimeCtrl_TextChanged;
            DateTimeCtrl.TextChanged += DateTimeCtrl_TextChanged;

            CheckCtrl.ItemCheck -= CheckCtrl_ItemCheck;
            CheckCtrl.ItemCheck += CheckCtrl_ItemCheck;
            CheckCtrl.CheckOnClick = true;

            GetChkFilter();

            CheckCtrl.MaximumSize = new Size(widthTool, GetHeightTable() - 120);
            CheckCtrl.Size = new Size(widthTool, (CheckCtrl.Items.Count + 1) * 18);

            ApplyButtonCtrl.Text = ButtonCtrlText;
            ApplyButtonCtrl.Size = new Size(widthTool, 30);
            ApplyButtonCtrl.Click -= ApplyButtonCtrl_Click;
            ApplyButtonCtrl.Click += ApplyButtonCtrl_Click;

            ClearFilterCtrl.Text = ClearFilterCtrlText;
            ClearFilterCtrl.Size = new Size(widthTool, 30);
            ClearFilterCtrl.Click -= ClearFilterCtrl_Click;
            ClearFilterCtrl.Click += ClearFilterCtrl_Click;

            popup.Items.Clear();
            popup.AutoSize = true;
            popup.Margin = Padding.Empty;
            popup.Padding = Padding.Empty;

            ToolStripControlHost host1 = new ToolStripControlHost(textBoxCtrl);
            host1.Margin = Padding.Empty;
            host1.Padding = Padding.Empty;
            host1.AutoSize = false;
            host1.Size = textBoxCtrl.Size;

            ToolStripControlHost host2 = new ToolStripControlHost(CheckCtrl);
            host2.Margin = Padding.Empty;
            host2.Padding = Padding.Empty;
            host2.AutoSize = false;
            host2.Size = CheckCtrl.Size;

            ToolStripControlHost host3 = new ToolStripControlHost(ApplyButtonCtrl);
            host3.Margin = Padding.Empty;
            host3.Padding = Padding.Empty;
            host3.AutoSize = false;
            host3.Size = ApplyButtonCtrl.Size;

            ToolStripControlHost host4 = new ToolStripControlHost(ClearFilterCtrl);
            host4.Margin = Padding.Empty;
            host4.Padding = Padding.Empty;
            host4.AutoSize = false;
            host4.Size = ClearFilterCtrl.Size;

            ToolStripControlHost host5 = new ToolStripControlHost(DateTimeCtrl);
            host5.Margin = Padding.Empty;
            host5.Padding = Padding.Empty;
            host5.AutoSize = false;
            host5.Size = DateTimeCtrl.Size;

            switch (Columns[columnIndex].ValueType.ToString())
            {
                case "System.DateTime":
                    popup.Items.Add(host5);
                    break;
                default:
                    popup.Items.Add(host1);
                    break;
            }

            popup.Items.Add(host2);
            popup.Items.Add(host3);
            popup.Items.Add(host4);

            popup.Show(this, e.ButtonRectangle.X, e.ButtonRectangle.Bottom);
            host2.Focus();
        }

        // Выбор всех
        void CheckCtrl_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {
                if (e.NewValue == CheckState.Checked)
                {
                    for (int i = 1; i < CheckCtrl.Items.Count; i++)
                        CheckCtrl.SetItemChecked(i, true);
                }
                else
                {
                    for (int i = 1; i < CheckCtrl.Items.Count; i++)
                        CheckCtrl.SetItemChecked(i, false);
                }
            }
        }

        // Очистить фильтры
        void ClearFilterCtrl_Click(object sender, EventArgs e)
        {
            Filter.Clear();
            StrFilter = "";
            ApllyFilter();
            popup.Close();
        }

        // Событие при изменении текста в TextBox
        void textBoxCtrl_TextChanged(object sender, EventArgs e)
        {
            //if (textBoxCtrl.Text != textBoxCtrlText)       
            (DataSource as DataTable).DefaultView.RowFilter = string.Format(
                "convert([" + Columns[columnIndex].Name + "], 'System.String') LIKE '%{0}%'", textBoxCtrl.Text);
        }

        void DateTimeCtrl_TextChanged(object sender, EventArgs e)
        {
            (DataSource as DataTable).DefaultView.RowFilter = string.Format(
                "convert([" + Columns[columnIndex].Name + "], 'System.String') LIKE '%{0}%'", DateTimeCtrl.Text);
        }

        // Событие кнопки применить
        void ApplyButtonCtrl_Click(object sender, EventArgs e)
        {
            StrFilter = "";
            SaveChkFilter();
            ApllyFilter();
            popup.Close();
        }

        // Получаем данные из выбранной колонки 
        private List<string> GetDataColumns(int e)
        {
            List<string> ValueCellList = new List<string>();
            string Value;

            // Посик данных в столбце, исключая повторения
            foreach (DataGridViewRow row in Rows)
            {
                Value = row.Cells[e].Value.ToString();
                if (Value == "") Value = SpaceText;

                if (!ValueCellList.Contains(Value))
                    ValueCellList.Add(Value);
            }

            return ValueCellList;
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
            string col = Columns[columnIndex].Name;
            string itemChk;
            bool statChk;

            Filter.RemoveAll(x => x.columnName == col);

            for (int i = 1; i < CheckCtrl.Items.Count; i++)
            {
                itemChk = CheckCtrl.Items[i].ToString();
                statChk = CheckCtrl.GetItemChecked(i);
                Filter.Add(new FilterStatus() { columnName = col, valueString = itemChk, check = statChk });
            }
        }

        // Загрузить чекбоксы
        private void GetChkFilter()
        {
            List<FilterStatus> CheckList = new List<FilterStatus>();
            List<FilterStatus> CheckListSort = new List<FilterStatus>();

            // Посик сохранённых данных
            foreach (FilterStatus val in Filter)
            {
                if (Columns[columnIndex].Name == val.columnName)
                {
                    if (val.valueString == "") val.valueString = SpaceText;
                    CheckList.Add(new FilterStatus()
                        { columnName = "", valueString = val.valueString, check = val.check });
                }
            }

            // Поиск данных в таблице
            foreach (string ValueCell in GetDataColumns(columnIndex))
            {
                int index = CheckList.FindIndex(item => item.valueString == ValueCell);
                if (index == -1)
                {
                    CheckList.Add(new FilterStatus { valueString = ValueCell, check = true });
                }
            }

            CheckCtrl.Items.Add(CheckCtrlAllText, CheckState.Indeterminate);
            // Сортировка
            switch (Columns[columnIndex].ValueType.ToString())
            {
                case "System.Int32":
                    CheckListSort = CheckList.OrderBy(x => Int32.Parse(x.valueString)).ToList();
                    foreach (FilterStatus val in CheckListSort)
                    {
                        if (val.check == true)
                            CheckCtrl.Items.Add(val.valueString, CheckState.Checked);
                        else
                            CheckCtrl.Items.Add(val.valueString, CheckState.Unchecked);
                    }

                    break;
                case "System.DateTime":
                    CheckListSort = CheckList.OrderBy(x => DateTime.Parse(x.valueString)).ToList();
                    foreach (FilterStatus val in CheckListSort)
                    {
                        if (val.check == true)
                            CheckCtrl.Items.Add(DateTime.Parse(val.valueString).ToString("dd.MM.yyyy"),
                                CheckState.Checked);
                        else
                            CheckCtrl.Items.Add(DateTime.Parse(val.valueString).ToString("dd.MM.yyyy"),
                                CheckState.Unchecked);
                    }

                    break;
                default:
                    /**
                     * TODO: тут поменял
                     */
                    CheckListSort = CheckList.OrderBy(x => x.valueString.ToString()).ToList();
                    foreach (FilterStatus val in CheckListSort)
                    {
                        if (val.check == true)
                            CheckCtrl.Items.Add(val.valueString, CheckState.Checked);
                        else
                            CheckCtrl.Items.Add(val.valueString, CheckState.Unchecked);
                    }

                    break;
            }
        }

        // Применить фильтр
        private void ApllyFilter()
        {
            foreach (FilterStatus val in Filter)
            {
                if (val.valueString == SpaceText)
                    val.valueString = "";

                if (val.check == false)
                {
                    // Исключение если bool              
                    string valueFilter = "'" + val.valueString + "' ";
                    if (valueFilter == "True")
                        valueFilter = "1";
                    if (valueFilter == "False")
                        valueFilter = "0";

                    if (StrFilter.Length == 0)
                        StrFilter = StrFilter + ("[" + val.columnName + "] <> " + valueFilter);
                    else
                        StrFilter = StrFilter + (" AND [" + val.columnName + "] <> " + valueFilter);
                }
            }

            (DataSource as DataTable).DefaultView.RowFilter = StrFilter;
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
            var DT = new DataTable();
            DT.Columns.Add("Number", typeof(int));
            DT.Columns.Add("Name");
            DT.Columns.Add("Ver");
            DT.Columns.Add("Date", typeof(DateTime));
            DT.Rows.Add("1", "Ubuntu", "11.10", "13.10.2011");
            DT.Rows.Add("2", "Ubuntu LTS", "12.04", "18.10.2012");
            DT.Rows.Add("3", "Ubuntu", "12.10", "18.10.2012");
            DT.Rows.Add("4", "Ubuntu", "13.04", "25.04.2012");
            DT.Rows.Add("5", "Ubuntu", "13.10", "17.10.2013");
            DT.Rows.Add("6", "Ubuntu LTS", "14.04", "23.04.2014");
            DT.Rows.Add("7", "Ubuntu", "14.10", "23.10.2014");
            DT.Rows.Add("8", "Ubuntu", "15.04", "23.04.2015");
            DT.Rows.Add("9", "Ubuntu", "15.04", "23.04.2015");
            DT.Rows.Add("11", "fbfghf", "11.10", "13.10.2011");
            DT.Rows.Add("12", "fbfghf LTS", "12.04", "18.10.2012");
            DT.Rows.Add("13", "fbfghf", "12.10", "18.10.2012");
            DT.Rows.Add("14", "fbfghf", "13.04", "25.04.2012");
            DT.Rows.Add("15", "fbfghf", "13.10", "17.10.2013");
            DT.Rows.Add("16", "fbfghf LTS", "14.04", "23.04.2014");
            DT.Rows.Add("17", "fbfghf", "14.10", "23.10.2014");
            DT.Rows.Add("18", "fbfghf", "15.04", "23.04.2015");
            DT.Rows.Add("19", "fbfghf", "15.04", "23.04.2015");
            DT.Rows.Add("21", "Ubuntu", "11.10", "13.10.2011");
            DT.Rows.Add("22", "Ubuntu LTS", "12.04", "18.10.2012");
            DT.Rows.Add("23", "Ubuntu", "12.10", "18.10.2012");
            DT.Rows.Add("24", "Ubuntu", "13.04", "25.04.2012");
            DT.Rows.Add("25", "Ubuntu", "13.10", "17.10.2013");
            DT.Rows.Add("26", "Ubuntu LTS", "14.04", "23.04.2014");
            DT.Rows.Add("27", "Ubuntu", "14.10", "23.10.2014");
            DT.Rows.Add("28", "Ubuntu", "15.04", "23.04.2015");
            DT.Rows.Add("29", "Ubuntu", "15.04", "23.04.2015");
            return DT;
        }
    }
}