using LightInject;
using pis.infrasrtucture.filter.impl;
using PISWF.infrasrtucture.auth.controller;
using PISWF.infrasrtucture.auth.model.entity;
using PISWF.infrasrtucture.auth.model.view;
using PISWF.infrasrtucture.auth.service;
using PISWF.infrasrtucture.filter;
using PISWF.infrasrtucture.logger.controller;
using PISWF.infrasrtucture.page;
using PISWF.view;

namespace PISWF;

public static class Program
{
    // Добавить миграцию - dotnet ef migrations add (название)
    // Залить на базу - dotnet ef database update
    // НАстройка контейнера
    // docker run --name pis -p 5432:5432 -e POSTGRES_PASSWORD=1234 -d postgres
    
    [STAThread]
    public static void Main(string[] args)
    {
        var container = new AppContainer();
           Application.EnableVisualStyles();
           Application.SetCompatibleTextRenderingDefault(false);
           Application.Run(container.GetInstance<DgvFilter>());
    }
    
}