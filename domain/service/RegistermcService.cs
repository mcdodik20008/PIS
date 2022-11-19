using System.Drawing.Imaging;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using PISWF.domain.model.validator;
using PISWF.domain.registermc.context.repository;
using PISWF.domain.registermc.model.entity;
using PISWF.domain.registermc.model.mapper;
using PISWF.domain.registermc.model.view;
using PISWF.infrasrtucture;
using PISWF.infrasrtucture.auth.model.entity;
using PISWF.infrasrtucture.extentions;
using PISWF.infrasrtucture.page;

namespace PISWF.domain.registermc.service;

public class RegistermcService
{
    private ExcelExporter ExcelExporter { get; }
    
    private FileDocumentRepository FileDocumentRepository { get; }

    private RegisterMcMapper RegisterMcMapper { get; }

    private RegisterMcRepository RegisterMcRepository { get; }

    private RegistermcValidator Validator { get; set; }

    public RegistermcService(
        FileDocumentRepository fileDocumentRepository,
        RegisterMcMapper registerMcMapper,
        RegisterMcRepository registerMcRepository,
        ExcelExporter excelExporter,
        RegistermcValidator validator)
    {
        FileDocumentRepository = fileDocumentRepository;
        RegisterMcMapper = registerMcMapper;
        RegisterMcRepository = registerMcRepository;
        ExcelExporter = excelExporter;
        Validator = validator;
    }

    public List<RegisterMCShort> Read(Page page)
    {
        return RegisterMcMapper.Map<List<RegisterMCShort>>(RegisterMcRepository.Entity
            .Include(x => x.Organization)
            .Include(x => x.Municipality)
            .Skip(page.Size * page.Number)
            .Take(page.Size)
        );
    }

    public List<RegisterMCShort> Read(Page page, Func<RegisterMC, bool> filter, SortParameters sortParameters)
    {
        var comperer = new UltimateComparer<RegisterMC>(sortParameters);
        return RegisterMcMapper.Map<List<RegisterMCShort>>(RegisterMcRepository.Entity
            .Where(filter)
            .OrderBy(x => x, comperer)
            .Skip(page.Size * page.Number)
            .Take(page.Size)
        );
    }

    public RegisterMCLong Read(long id)
    {
        var entity = RegisterMcRepository.Entity
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
        RegisterMcRepository.Entity.Add(entity);
        RegisterMcRepository.Save();
        return view;
    }

    public RegisterMCLong Update(long id, RegisterMCLong view)
    {
        view.Id = id;
        var register = RegisterMcRepository.Entity.Find(id);
        register = RegisterMcMapper.Map(view, register);
        Validator.Validate(register);
        RegisterMcRepository.Entity.Update(register);
        return view;
    }

    public void Delete(long id)
    {
        var entity = RegisterMcRepository.Entity.Find(id);
        RegisterMcRepository.Entity.Remove(entity);
        RegisterMcRepository.SaveChanges();
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
        var entity = RegisterMcRepository.Entity.Find(register.Id);
        var date = DateTime.Now.ToString().Replace(new[] { ' ', ':', '.' }, '-');
        var name = user.Municipality is null ? user.Organization.Name : user.Municipality.Name;
        var doc = new FileDocument();
        doc.FilePath = $"C:\\pisDoc\\{name}\\{date}\\{doc.Name}";
        doc.FileType = "image";
        var image = OpenFile(doc);
        if (image is null)
            return;
        SaveFile(image, doc);
        entity.Documents.Add(doc);
        RegisterMcRepository.Entity.Update(entity);
        RegisterMcRepository.SaveChanges();
    }

    private Image OpenFile(FileDocument doc)
    {
        var sfd = new OpenFileDialog();
        sfd.Filter = "Файлы изображений|*.bmp;*.png;*.jpg";
        if (sfd.ShowDialog() != DialogResult.OK)
            return null;
        try
        {
            doc.Name = sfd.FileName.Split('\\').Last();
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
        return RegisterMcRepository.Entity
            .Include(x => x.Organization)
            .Include(x => x.Municipality)
            .Include(x => x.Documents)
            .Where(filter)
            .ToList();
    }

    public void DeleteFile(long id)
    {
        var entity = FileDocumentRepository.Entity.Find(id);
        FileDocumentRepository.Entity.Remove(entity);
    }
}