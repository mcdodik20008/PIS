using System.Drawing.Imaging;
using Microsoft.EntityFrameworkCore;
using PISWF.domain.model.validator;
using PISWF.domain.registermc.model.entity;
using PISWF.domain.registermc.model.mapper;
using PISWF.domain.registermc.model.view;
using PISWF.infrasrtucture;
using PISWF.infrasrtucture.auth.model.entity;
using PISWF.infrasrtucture.extentions;
using PISWF.infrasrtucture.muni_org.service;
using PISWF.infrasrtucture.page;

namespace PISWF.domain.registermc.service;

public class RegistermcService
{
    private ExcelExporter ExcelExporter { get; }

    private RegisterMcMapper RegisterMcMapper { get; }


    private RegistermcValidator Validator { get; set; }

    private OrganizationService OrganizationService { get; set; }

    private MunicipalityService MunicipalityService { get; set; }

    public RegistermcService(
        RegisterMcMapper registerMcMapper,
        ExcelExporter excelExporter,
        RegistermcValidator validator,
        OrganizationService organizationService,
        MunicipalityService municipalityService
    )
    {
        RegisterMcMapper = registerMcMapper;
        ExcelExporter = excelExporter;
        Validator = validator;
        OrganizationService = organizationService;
        MunicipalityService = municipalityService;
    }

    public List<RegisterMCShort> Read(Page page)
    {
        using var context = new AppDbContext();
        return RegisterMcMapper.Map<List<RegisterMCShort>>(context.Register
            .Include(x => x.Organization)
            .Include(x => x.Municipality)
            .Skip(page.Size * page.Number)
            .Take(page.Size)
        );
    }

    public List<RegisterMCShort> Read(Page page, Func<RegisterMC, bool> filter, SortParameters sortParameters)
    {
        var comparer = new UltimateComparer<RegisterMC>(sortParameters);
        using var context = new AppDbContext();
        return RegisterMcMapper.Map<List<RegisterMCShort>>(context.Register
            .Where(filter)
            .OrderBy(x => x, comparer)
            .Skip(page.Size * page.Number)
            .Take(page.Size)
        );
    }

    public RegisterMCLong Read(long id)
    {
        using var context = new AppDbContext();
        var entity = context.Register
            .Include(x => x.Organization)
            .Include(x => x.Municipality)
            .Include(x => x.Documents)
            .FirstOrDefault(x => x.Id == id);
        return RegisterMcMapper.Map<RegisterMCLong>(entity);
    }

    public RegisterMCLong Create(RegisterMCLong view)
    {
        var entity = RegisterMcMapper.Map<RegisterMC>(view);
        Validator.Validate(entity);
        using var context = new AppDbContext();
        var xx = context.Register.Update(entity).Entity;
        context.SaveChanges();
        return RegisterMcMapper.Map<RegisterMCLong>(xx);
    }

    public RegisterMCLong Update(long id, RegisterMCLong view)
    {
        view.Id = id;
        using var context = new AppDbContext();
        var docIds = view.Documents.Select(x => x.Id);
        var docs = context.Documents.Where(x => docIds.Contains(x.Id)).ToList();
        var register = context.Register.Find(id);
        register = RegisterMcMapper.Map(view, register);
        register.Documents = docs;
        Validator.Validate(register);
        context.Register.Update(register);
        context.SaveChanges();
        return view;
    }

    public void Delete(long id)
    {
        using var context = new AppDbContext();
        var entity = context.Register.Find(id);
        context.Register.Remove(entity);
        context.SaveChanges();
    }

    public void ExportToExcel(Func<RegisterMC, bool> filter, string path = "C:\\pisDoc\\reports\\")
    {
        var report = ExcelExporter.Generate(Read(filter));
        var dateOnly = DateOnly.FromDateTime(DateTime.Now);
        Directory.CreateDirectory(path.GetDirectory());
        path += $"{dateOnly}".Replace(new[] { '.' }, '-') + "-RegisterMCReport.xlsx";
        File.WriteAllBytes(path, report);
    }

    public void UpLoadFile(RegisterMCLong register, User user)
    {
        using var context = new AppDbContext();
        var entity = context.Register.Find(register.Id);
        var name = user.Municipality is null ? user.Organization.Name : user.Municipality.Name;
        var doc = new FileDocument();
        doc.FilePath = $"C:\\pisDoc\\{name}\\";
        doc.FileType = "image";
        var image = OpenFile(doc);
        if (image is null)
            return;
        SaveFile(image, doc);
        // TODO: нифига как можно
        entity.Documents ??= new();
        entity.Documents.Add(doc);
        context.Register.Update(entity);
        context.SaveChanges();
    }

    private Image OpenFile(FileDocument doc)
    {
        var sfd = new OpenFileDialog();
        sfd.Filter = "Файлы изображений|*.bmp;*.png;*.jpg";
        if (sfd.ShowDialog() != DialogResult.OK)
            return null;
        try
        {
            var date = DateTime.Now.ToString().Replace(new[] { ' ', ':', '.' }, '-');
            doc.Name = $"{date}-" + sfd.FileName.Split('\\').Last();
            return Image.FromFile(sfd.FileName);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка чтения скана образа документа");
        }

        return null;
    }

    private void SaveFile(Image image, FileDocument fileDocument)
    {
        if (image == null)
            return;
        Directory.CreateDirectory(fileDocument.FilePath);
        image.Save(fileDocument.FilePath + fileDocument.Name, ImageFormat.Png);
    }

    private List<RegisterMC> Read(Func<RegisterMC, bool> filter)
    {
        using var context = new AppDbContext();
        return context.Register
            .Include(x => x.Organization)
            .Include(x => x.Municipality)
            .Include(x => x.Documents)
            .Where(filter)
            .ToList();
    }

    public void DeleteFile(long id)
    {
        using var context = new AppDbContext();
        var entity = context.Documents.Find(id);
        context.Documents.Remove(entity);
        context.SaveChanges();
    }
}