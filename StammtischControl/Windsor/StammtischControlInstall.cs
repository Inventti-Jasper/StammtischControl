using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using StammtischControl.Controllers;
using StammtischControl.Models.Entidades;
using StammtischControl.Models.Entidades.CadastroGeral;
using StammtischControl.Models.Persistencia;

namespace StammtischControl.Windsor
{
    public class StammtischControlInstall: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IRepositorio<Participante>>().ImplementedBy<Repositorio<Participante>>().LifestylePerWebRequest());
            //container.Register(Component.For<HomeController>().LifestyleSingleton());
            container.Register(Classes.FromAssemblyContaining<HomeController>().Pick().LifestylePerWebRequest());
        }
    }
}