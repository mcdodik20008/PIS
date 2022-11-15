using System.Drawing.Imaging;
using Microsoft.EntityFrameworkCore;
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

    private FileDocumentMapper FileDocumentMapper { get; }

    private FileDocumentRepository FileDocumentRepository { get; }

    private RegisterMcMapper RegisterMcMapper { get; }

    private RegisterMcRepository RegisterMcRepository { get; }
    
    private RegistermcValidator Validator { get; set; }

    public RegistermcService(
        FileDocumentMapper fileDocumentMapper,
        FileDocumentRepository fileDocumentRepository,
        RegisterMcMapper registerMcMapper,
        RegisterMcRepository registerMcRepository,
        ExcelExporter excelExporter, 
        RegistermcValidator validator)
    {
        FileDocumentMapper = fileDocumentMapper;
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
        return RegisterMcMapper.Map<RegisterMCLong>(RegisterMcRepository.Entity
            .Include(x => x.Organization)
            .Include(x => x.Municipality)
            .Include(x => x.Documents)
            .FirstOrDefault(x => x.Id.Equals(id))
        );
    }

    public RegisterMCLong Create(RegisterMCLong view)
    {
        var entity = RegisterMcMapper.Map<RegisterMC>(view);
        Validator.Validate(entity);
        RegisterMcRepository.AddAndSave(entity);
        return view;
    }

    // TODO: тестить
    public RegisterMCLong Update(long id, RegisterMCLong view)
    {
        view.Id = id;
        var register = RegisterMcRepository.Entity.Find(id);
        register = RegisterMcMapper.Map(view, register);
        Validator.Validate(register);
        RegisterMcRepository.AddAndSave(register);
        return view;
    }

    public RegisterMCShort Delete(RegisterMCShort view)
    {
        var entity = RegisterMcMapper.Map<RegisterMC>(view);
        RegisterMcRepository.Entity.Remove(entity);
        return view;
    }

    public void ExportToExcel(Func<RegisterMC, bool> filter, string path = "C:\\pisDoc\\reports\\")
    {
        var report = ExcelExporter.Generate(Read(filter));
        var dateOnly = DateOnly.FromDateTime(DateTime.Now);
        Directory.CreateDirectory(path.GetDirectory());
        path += $"{dateOnly}".Replace(new[] { '.' }, '-') + "-RegisterMCReport.xlsx";
        File.WriteAllBytes(path, report);
    }
    
    public void UpLoadFile(RegisterMC register, User user)
    {
        var date = DateTime.Now.ToString().Replace(new[] { ' ', ':', '.' }, '-');
        var name = user.Municipality is null ? user.Organization.Name : user.Municipality.Name;
        var doc = new FileDocument();
        doc.FilePath = $"C:\\pisDoc\\{name}\\{date}\\{doc.Name}";
        doc.FileType = "image";
        SaveFile(OpenFile(doc), doc);
        register.Documents.Add(doc);
        RegisterMcRepository.Entity.Update(register);
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
    
    // TODO: он вообще нужен?
    public void DownLoadFile(FileDocumentShort doc)
    {
        throw new NotImplementedException();
    }

    private void SaveFile(Image image, FileDocument fileDocument)
    {
        Directory.CreateDirectory(fileDocument.FilePath);
        image.Save(fileDocument.FilePath, ImageFormat.Png);
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
}