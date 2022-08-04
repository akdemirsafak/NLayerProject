using System.Reflection;
using Autofac;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWorks;
using NLayer.Service.Mapping;
using NLayer.Service.Services;
using Module = Autofac.Module;

namespace NLayer.Web.Modules;

public class RepoServiceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>))
            .InstancePerLifetimeScope();
        builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();
        //Generic olduğu için typeof ile ekledik.

        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

        

        var apiAssembly = Assembly.GetExecutingAssembly();
        //Tip güvenli yapıyoruz.
        var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext)); //Repo katmanından herhangi biri
        var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile)); //service katmanından herhangi biri 


        builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly)
            .Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();
        builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly)
            .Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();

        //InstancePerLifetimeScope => DI Container'deki Scope a karşılık gelir.
        //InstancePerDependency =>transient e denk gelir.
    }
}