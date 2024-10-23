using Autofac;
using ETicaretAPI.Application.Services;
using ETicaretAPI.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure
{
    public class InfrastructureAutofactModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FileService>().As<IFileService>();
        }
    }
}
