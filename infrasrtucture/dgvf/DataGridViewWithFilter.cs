using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using DGVWF;
using pis.infrasrtucture.sort;
using PISWF.domain.registermc.service;
using PISWF.infrasrtucture.filter;

namespace pis.infrasrtucture.dgvf;

// TODO: refactor
public class DataGridViewWithFilter<TValue,TFilter> : DataGridView where TFilter : FilterModel
{
    private int order = 0;
    private int _columnIndex;
    private List<TValue> _values = new();
    private readonly TFilter _filter;
    private readonly FilterSorterMapper _filterSorterMapper;
    private readonly List<FilterSorterColumn> _filterColumns = new();

    #region formelements

    private readonly TextBox _textBoxCtrl = new();
    private readonly DateTimePicker _dateTimeCtrl = new();
    private readonly Button _saveFilterCtrl = new();
    private readonly Button _clearButtonCtrl = new();
    private readonly ToolStripDropDown _popup = new();
    private readonly ComboBox _comboBoxFilter = new();
    private readonly ComboBox _comboBoxSort = new();

    #endregion

    public DataGridViewWithFilter(IFilterFactory factory, FilterSorterMapper filterSorterMapper)
    {
        // рефлекшином через атрибут доставать филтр
        _filter = factory.Find<TFilter>();
        _filterSorterMapper = filterSorterMapper;
    }

    private void Header_FilterButtonClicked(object sender, ColumnFilterClickedEventArg e)
    {
        _columnIndex = e.ColumnIndex;
        InitializeCtrls(_columnIndex);
        var valueTextBox = GetControlHost(_textBoxCtrl);
        var actionBox = GetControlHost(_comboBoxFilter);
        var saveButton = GetControlHost(_saveFilterCtrl);
        var clearButton = GetControlHost(_clearButtonCtrl);
        var dateTimePicker = GetControlHost(_dateTimeCtrl);
        var sortBox = GetControlHost(_comboBoxSort);
        _popup.Items.Clear();
        _popup.AutoSize = true;
        _popup.Margin = Padding.Empty;
        _popup.Padding = Padding.Empty;
        var colType = Columns[_columnIndex].ValueType.ToString();
        FillFilterCombobox(_comboBoxFilter, colType);
        FillSortCombobox(_comboBoxSort);
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
            case "System.String":
                _popup.Items.Add(actionBox);
                _popup.Items.Add(valueTextBox);
                break;
            default:
                _popup.Items.Add(valueTextBox);
                break;
        }

        _popup.Items.Add(sortBox);
        _popup.Items.Add(saveButton);
        _popup.Items.Add(clearButton);
        _popup.Show(this, e.ButtonRectangle.X, e.ButtonRectangle.Bottom);
    }

    private void InitializeCtrls(int colIndex)
    {
        var widthTool = Columns[colIndex].Width + 50;
        if (widthTool < 130) widthTool = 130;
        
        _comboBoxSort.Text = _filterColumns[_columnIndex].ValueSorter;
        _comboBoxSort.Size = new Size(widthTool, 30);
        
        _comboBoxFilter.Text = _filterColumns[_columnIndex].ValueFilter;
        _comboBoxFilter.Size = new Size(widthTool, 30);

        _textBoxCtrl.Text = _filterColumns[_columnIndex].Value;
        _textBoxCtrl.Size = new Size(widthTool, 30);

        _dateTimeCtrl.Size = new Size(widthTool, 30);
        _dateTimeCtrl.Format = DateTimePickerFormat.Custom;
        _dateTimeCtrl.CustomFormat = "dd.MM.yyyy";
        _dateTimeCtrl.TextChanged -= DatePicker_TextChanged!;
        _dateTimeCtrl.TextChanged += DatePicker_TextChanged!;

        _saveFilterCtrl.Text = "Save";
        _saveFilterCtrl.Size = new Size(widthTool, 30);
        _saveFilterCtrl.Click -= SaveFilterSorter_Click!;
        _saveFilterCtrl.Click += SaveFilterSorter_Click!;

        _clearButtonCtrl.Text = "Clear";
        _clearButtonCtrl.Size = new Size(widthTool, 30);
        _clearButtonCtrl.Click -= ClearFilter_Click!;
        _clearButtonCtrl.Click += ClearFilter_Click!;
    }

    private void ClearFilter_Click(object? sender, EventArgs e)
    {
        _textBoxCtrl.Text = "";
        _comboBoxFilter.Text = "Без фильтрации";
        _comboBoxSort.Text = "Без соритровки";
        SaveFilterSorter_Click(sender, e);
    }

    private void DatePicker_TextChanged(object sender, EventArgs e)
    {
        _textBoxCtrl.Text = _dateTimeCtrl.Text;
        SaveFilterSorter_Click(sender, e);
    }

    private void SaveFilterSorter_Click(object sender, EventArgs e)
    {
        _filterColumns[_columnIndex].Value = _textBoxCtrl.Text;
        _filterColumns[_columnIndex].Order = order++;
        _filterColumns[_columnIndex].ValueFilter = _comboBoxFilter.Text;
        _filterColumns[_columnIndex].ValueSorter = _comboBoxSort.Text;
        _popup.Close();
    }

    public void FillDataGrid(List<TValue> sourse)
    {
        _values.Clear();
        _values.AddRange(sourse);
        var dt = new DataTable();
        Columns.Clear();
        var propertys = typeof(TValue).GetProperties();

        foreach (var prop in propertys)
        {
            if (prop.Name.ToLower().Equals("id"))
                continue;
            dt.Columns.Add(prop.Name, prop.PropertyType);
            if (_filterColumns.Count < propertys.Length)
            {
                _filterColumns.Add(new FilterSorterColumn(prop, prop.Name, prop.PropertyType));
            }
        }

        foreach (var entity in sourse)
        {
            var values = GetEntityValues(entity, propertys);
            dt.Rows.Add(values);
        }

        DataSource = dt;
    }

    private object[] GetEntityValues<T>(T entity, PropertyInfo[] propertys)
    {
        var values = new List<object>();
        var index = 0;
        foreach (var prop in propertys)
        {
            if (prop.Name.ToLower().Equals("id"))
                continue;
            values.Add(prop.GetValue(entity));
        }

        return values.ToArray();
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

    private void FillFilterCombobox(ComboBox comboBox, string columnType)
    {
        var values = columnType switch
        {
            "System.DateTime" => new[] { "Без фильтрации", "До", "После" },
            "System.Int32" or "System.Int64" or "System.Double"
                => new[] { "Без фильтрации", "Меньше", "Больше", "Равно" },
            _ => new[]{ "Содержит" }
        };
        var val = _filterColumns[_columnIndex].ValueFilter;
        comboBox.Text = val is null || val.Equals("") ? values[0] : val;
        comboBox.Items.Clear();
        comboBox.Items.AddRange(values);
    }

    private void FillSortCombobox(ComboBox comboBox)
    {
        var values = new[] { "Без соритровки", "По возрастанию", "По убыванию" };
        var val = _filterColumns[_columnIndex].ValueSorter;
        comboBox.Text = val is null || val.Equals("") ? values[0] : val;
        comboBox.Items.Clear();
        comboBox.Items.AddRange(values);
    }

    protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
    {
        var header = new DataGridFilterHeader();
        header.FilterButtonClicked += Header_FilterButtonClicked!;
        e.Column.HeaderCell = header;
        e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        base.OnColumnAdded(e);
    }
    
    public Expression<Func<TObject, bool>> GetFilter<TObject>()
    {
        var filter = _filter as FilterModel<TObject>;
        filter = FillFilter(filter, _filterColumns);
        return filter.GetExpression();
    }
    
    public TValue GetSelectedItem(int rowIndex)
    {
        return _values[rowIndex];
    }

    private FilterModel<T> FillFilter<T>(FilterModel<T> filter, List<FilterSorterColumn> filterColumns)
    {
        var properties = filter.GetType().GetProperties();
        foreach (var property in properties)
        {
            var value = property.GetValue(filter);
            MethodInfo updateFilterFieldMethod = value.GetType().GetMethod("UpdateFilter");
            var popName = property.GetCustomAttribute<SourseNameAttribute>()?.Name;
            var filterColumn = filterColumns.FirstOrDefault(x => x.Name.Equals(popName));
            var parameters = Equals(filterColumn, null) || Equals(filterColumn.Value, "")
                ? new object[] { _filterSorterMapper, "", "" }
                : new object[] { _filterSorterMapper, filterColumn.Value, filterColumn.ValueFilter };
            updateFilterFieldMethod.Invoke(value, parameters);
        }

        return filter;
    }
    
    public SortParameters GetSortParameters<T>()
    {
        var type = typeof(T);
        var parameters = new SortParameters();
        foreach (var column in _filterColumns.OrderBy(x => x.Order))
        {
            var prop = type.GetProperty(column.Property.Name);
            var val = _filterSorterMapper.Map<SortDirection>(column.ValueSorter);
            parameters.list.Add(new SortParameter(prop, val));
        }
        return parameters;
    }
}