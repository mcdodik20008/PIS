using System.Drawing.Imaging;
using Microsoft.EntityFrameworkCore;
using PISWF.domain.registermc.context.repository;
using PISWF.domain.registermc.model.entity;
using PISWF.domain.registermc.model.mapper;
using PISWF.domain.registermc.model.view;
using PISWF.infrasrtucture.auth.controller;
using PISWF.infrasrtucture.auth.model.entity;
using PISWF.infrasrtucture.page;

namespace PISWF.domain.registermc.service;

// TODO: Не забыть только апдатать, сохранение на отдельную кнопку

public class RegistermcService
{
    private FileDocumentMapper FileDocumentMapper { get; }

    private FileDocumentRepository FileDocumentRepository { get; }

    private RegisterMcMapper RegisterMcMapper { get; }

    private RegisterMcRepository RegisterMcRepository { get; }

    public RegistermcService(
        FileDocumentMapper fileDocumentMapper,
        FileDocumentRepository fileDocumentRepository,
        RegisterMcMapper registerMcMapper,
        RegisterMcRepository registerMcRepository)
    {
        FileDocumentMapper = fileDocumentMapper;
        FileDocumentRepository = fileDocumentRepository;
        RegisterMcMapper = registerMcMapper;
        RegisterMcRepository = registerMcRepository;
    }

    public List<RegisterMCLong> Read()
    {
        return RegisterMcMapper.Map<List<RegisterMCLong>>(RegisterMcRepository.Entity);
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
        return RegisterMcMapper.Map<List<RegisterMCShort>>(RegisterMcRepository.Entity
            .Where(filter)
            .OrderBy(x => x, new UltimateComparer<RegisterMC>(sortParameters))
            .Skip(page.Size * page.Number)
            .Take(page.Size)
        );
    }

    public RegisterMC Read(long id)
    {
        return RegisterMcMapper.Map<RegisterMC>(RegisterMcRepository.Entity
            .Include(x => x.Organization)
            .Include(x => x.Municipality)
            .Include(x => x.Documents)
            .FirstOrDefault(x => x.Id.Equals(id))
        );
    }

    public RegisterMCShort Create(RegisterMCShort view)
    {
        var entity = RegisterMcMapper.Map<RegisterMC>(view);
        RegisterMcRepository.AddAndSave(entity);
        return view;
    }

    // TODO: тестить
    public RegisterMCLong Update(long id, RegisterMCLong view)
    {
        view.Id = id;
        var register = RegisterMcRepository.Entity.Find(id);
        register = RegisterMcMapper.Map(view, register);
        RegisterMcRepository.AddAndSave(register);
        return view;
    }

    public RegisterMCShort Delete(RegisterMCShort view)
    {
        var entity = RegisterMcMapper.Map<RegisterMC>(view);
        RegisterMcRepository.Entity.Remove(entity);
        return view;
    }

    // TODO: Рефакторить
    public void UpLoadFile(RegisterMC register, User user)
    {
        var date = DateTime.Now.ToString()
            .Replace(' ', '-')
            .Replace(':', '-')
            .Replace('.', '-');
        var name = user.Municipality is null ? user.Organization.Name : user.Municipality.Name;
        var doc = new FileDocument();
        var image = OpenFile(doc);
        doc.FilePath = $"C:\\pisDoc\\{name}\\{date}\\{doc.Name}";
        doc.FileType = "image";
        SaveFile(image, doc);
        register.Documents.Add(doc);
        RegisterMcRepository.Entity.Update(register);
        RegisterMcRepository.SaveChanges();
    }

    public void DownLoadFile(FileDocumentShort doc)
    {
        throw new NotImplementedException();
    }

    private Image OpenFile(FileDocument doc)
    {
        Image image;
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
        var directory = fileDocument.FilePath.Split("\\");
        directory = directory.Take(directory.Length - 1).ToArray();
        var path = String.Join("\\", directory);
        Directory.CreateDirectory(path);
        image.Save(fileDocument.FilePath, ImageFormat.Png);
    }
}