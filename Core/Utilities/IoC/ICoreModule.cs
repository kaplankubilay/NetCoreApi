using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.IoC
{
    public interface ICoreModule
    {
        /// <summary>
        /// .NetCore Service collection u kullanarak cache leme işlemini gerçekleştirecek.
        /// </summary>
        /// <param name="collection"></param>
        void Load(IServiceCollection collection);
    }
}
