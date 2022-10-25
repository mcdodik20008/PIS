using System.Data;
using System.Reflection;
using DGVWF;
using PISWF.infrasrtucture.filter;

namespace pis.infrasrtucture.dgvf;

public class DataGridViewWithFilter<TFilter> : DataGridView where TFilter : FilterModel
{
    private int _columnIndex;
    private readonly TFilter _filter;
    private readonly FilterMapper _filterMapper;
    private readonly List<FilterColumn> _filterColumns = new();

    #region formelements

    private readonly TextBox _textBoxCtrl = new();
    private readonly DateTimePicker _dateTimeCtrl = new();
    private readonly Button _saveFilterCtrl = new();
    private readonly ToolStripDropDown _popup = new();
    private readonly ComboBox _comboBox = new();

    #endregion
    
    public DataGridViewWithFilter(FilterFactory factory, FilterMapper filterMapper)
    {
        _filter = factory.Find<TFilter>();
        _filterMapper = filterMapper;
    }

    public Func<TObject, bool> GetFilter<TObject>()
    {
        var filter = _filter as FilterModel<TObject>;
        filter = FillFilter(filter, _filterColumns);
        var res = filter.FilterExpression;
        return res;
    }
    
    private FilterModel<T> FillFilter<T>(FilterModel<T> filter, List<FilterColumn> filterColumns)
    {
        var fields = filter.GetType().GetFields();
        MethodInfo updateFilterFieldMethod = null;
        foreach (var field in fields)
        {
            updateFilterFieldMethod = field.GetValue(filter).GetType().GetMethod("UpdateFilter");
            var filedValue = field.GetValue(filter);
            var filterColumn = filterColumns.FirstOrDefault(x => field.Name.Contains(x.Name));
            if (filterColumn != null && !filterColumn.Value.Equals(""))
            {
                updateFilterFieldMethod.Invoke(filedValue,
                    new object[] { _filterMapper, _textBoxCtrl.Text, _comboBox.Text });
            }
        }

        return filter;
    }
    
    public void ReloadFilter<TObject>()
    {
        (_filter as FilterModel<TObject>)?.Reset();
    }

    private void Header_FilterButtonClicked(object sender, ColumnFilterClickedEventArg e)
    {
        _columnIndex = e.ColumnIndex;
        InitializeCtrls(_columnIndex);
        var valueTextBox = GetControlHost(_textBoxCtrl);
        var actionBox = GetControlHost(_comboBox);
        var saveButton = GetControlHost(_saveFilterCtrl);
        var dateTimePicker = GetControlHost(_dateTimeCtrl);
        _popup.Items.Clear();
        _popup.AutoSize = true;
        _popup.Margin = Padding.Empty;
        _popup.Padding = Padding.Empty;
        var colType = Columns[_columnIndex].ValueType.ToString();
        FillCombobox(_comboBox, colType);
        switch (colType)
        {
            case "System.DateTime":
                _popup.Items.Add(actionBox);
                _popup.Items.Add(dateTimePicker);
                break;
            case "System.Int64":
            case "System.Int32":
            case "System.Double":
                _popup.Items.Add(actionBox);
                _popup.Items.Add(valueTextBox);
                break;
            default:
                _popup.Items.Add(valueTextBox);
                break;
        }

        _popup.Items.Add(saveButton);
        _popup.Show(this, e.ButtonRectangle.X, e.ButtonRectangle.Bottom);
    }

    private void InitializeCtrls(int colIndex)
    {
        var widthTool = Columns[colIndex].Width + 50;
        if (widthTool < 130) widthTool = 130;

        _comboBox.Text = _filterColumns[_columnIndex].ValueComboBox;
        _comboBox.Size = new Size(widthTool, 30);

        _textBoxCtrl.Text = _filterColumns[_columnIndex].Value;
        _textBoxCtrl.Size = new Size(widthTool, 30);

        _dateTimeCtrl.Size = new Size(widthTool, 30);
        _dateTimeCtrl.Format = DateTimePickerFormat.Custom;
        _dateTimeCtrl.CustomFormat = "dd.MM.yyyy";
        _dateTimeCtrl.TextChanged -= DatePicker_TextChanged!;
        _dateTimeCtrl.TextChanged += DatePicker_TextChanged!;
            
        _saveFilterCtrl.Text = "Save filter";
        _saveFilterCtrl.Size = new Size(widthTool, 30);
        _saveFilterCtrl.Click -= SaveFilter_Click!;
        _saveFilterCtrl.Click += SaveFilter_Click!;
    }

    private void DatePicker_TextChanged(object sender, EventArgs e)
    {
        _textBoxCtrl.Text = _dateTimeCtrl.Text;
        SaveFilter_Click(sender, e);
    }
    
    private void SaveFilter_Click(object sender, EventArgs e)
    {
        _filterColumns[_columnIndex].Value = _textBoxCtrl.Text;
        _filterColumns[_columnIndex].ValueComboBox = _comboBox.Text;
        _popup.Close();
    }

    public void FillDataGrid<T>(IEnumerable<T> sourse)
    {
        var dt = new DataTable();
        Columns.Clear();
        var propertys = typeof(T).GetProperties();
        foreach (var prop in propertys)
        {
            dt.Columns.Add(prop.Name, prop.PropertyType);
            _filterColumns.Add(new FilterColumn(prop.Name, prop.PropertyType, "", ""));
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

    private ToolStripControlHost GetControlHost(Control control)
    {
        var host = new ToolStripControlHost(control);
        host.Margin = Padding.Empty;
        host.Padding = Padding.Empty;
        host.AutoSize = false;
        host.Size = _dateTimeCtrl.Size;
        return host;
    }

    private void FillCombobox(ComboBox comboBox, string columnType)
    {
        var values = columnType switch
        {
            "System.DateTime" => new[] { "До", "После" },
            "System.Int32" or "System.Int64" or "System.Double"
                => new[] { "Меньше", "Больше", "Равно" }
        };
        _comboBox.Text = _filterColumns[_columnIndex].ValueComboBox;
        comboBox.Items.Clear();
        comboBox.Items.AddRange(values);
    }

    protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
    {
        var header = new DataGridFilterHeader();
        header.FilterButtonClicked += Header_FilterButtonClicked!;
        e.Column.HeaderCell = header;
        base.OnColumnAdded(e);
    }
}