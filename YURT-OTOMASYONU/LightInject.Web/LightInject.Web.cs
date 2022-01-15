/*****************************************************************************
    The MIT License (MIT)

    Copyright (c) 2016 bernhard.richter@gmail.com

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
******************************************************************************
    LightInject.Web version 2.0.0
    http://seesharper.github.io/LightInject/
    http://twitter.com/bernhardrichter
******************************************************************************/
[assembly: System.Web.PreApplicationStartMethod(typeof(LightInject.Web.LightInjectHttpModuleInitializer), "Initialize")]
[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed")]
[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:PrefixLocalCallsWithThis", Justification = "No inheritance")]
[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Single source file deployment.")]
[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1403:FileMayOnlyContainASingleNamespace", Justification = "Extension methods must be visible")]
[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1633:FileMustHaveHeader", Justification = "Custom header.")]
[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "All public members are documented.")]
[module: System.Diagnostics.CodeAnalysis.SuppressMessage("MaintainabilityRules", "SA1403", Justification = "One source file")]
[module: System.Diagnostics.CodeAnalysis.SuppressMessage("DocumentationRules", "SA1649", Justification = "One source file")]

namespace YURT_OTOMASYONU.LightInject.Web
{
    using Web;

    /// <summary>
    /// Extends the <see cref="IServiceContainer"/> interface with a method
    /// to enable services that are scoped per web request.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal static class WebContainerExtensions
    {
        /// <summary>
        /// Ensures that services registered with the <see cref="PerScopeLifetime"/> is properly
        /// disposed when the web request ends.
        /// </summary>
        /// <param name="serviceContainer">The target <see cref="IServiceContainer"/>.</param>
        public static void EnablePerWebRequestScope(this IServiceContainer serviceContainer)
        {
            serviceContainer.ScopeManagerProvider = new PerWebRequestScopeManagerProvider();
        }
    }
}

namespace YURT_OTOMASYONU.LightInject.Web
{
    using System.Web;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    /// <summary>
    /// Registers the <see cref="LightInjectHttpModule"/> with the current <see cref="HttpApplication"/>.
    /// </summary>
    public static class LightInjectHttpModuleInitializer
    {
        private static bool isInitialized;

        /// <summary>
        /// Executed before the <see cref="HttpApplication"/> is started and registers
        /// the <see cref="LightInjectHttpModule"/> with the current <see cref="HttpApplication"/>.
        /// </summary>
        public static void Initialize()
        {
            if (!isInitialized)
            {
                isInitialized = true;
                DynamicModuleUtility.RegisterModule(typeof(LightInjectHttpModule));                                
            }
        }
    }

    /// <summary>
    /// A <see cref="IHttpModule"/> that ensures that services registered
    /// with the <see cref="PerScopeLifetime"/> lifetime is scoped per web request.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal class LightInjectHttpModule : IHttpModule
    {
        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="HttpApplication"/> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application </param>
        public void Init(HttpApplication context)
        {
            context.EndRequest += (a, e) => PerWebRequestScopeManager.EndContextScope();
        }

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"/>.
        /// </summary>
        public void Dispose()
        {
        }          
    }

    /// <summary>
    /// A <see cref="ScopeManager"/> that manages scope per web request.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal class PerWebRequestScopeManager : ScopeManager
    {
        private const string Key = "LightInject.Scope";

        /// <summary>
        /// Initializes a new instance of the <see cref="PerWebRequestScopeManager"/> class.        
        /// </summary>
        /// <param name="serviceFactory">The <see cref="IServiceFactory"/> to be associated with this <see cref="ScopeManager"/>.</param>
        public PerWebRequestScopeManager(IServiceFactory serviceFactory) : base(serviceFactory)
        {           
        }

        /// <summary>
        /// Gets or sets the current <see cref="T:LightInject.Scope"/>.
        /// </summary>
        public override Scope CurrentScope
        {
            get { return GetOrAddScope(); }
            set { HttpContext.Current.Items[Key] = value; }
        }

        /// <summary>
        /// Ends the scope associated with the current <see cref="HttpContext"/>.
        /// </summary>
        public static void EndContextScope()
        {
            var scope = (Scope)HttpContext.Current.Items[Key];
            if (scope != null)
            {
                scope.Dispose();
                HttpContext.Current.Items[Key] = null;                
            }
        }

        private Scope GetOrAddScope()
        {            
            var scope = (Scope)HttpContext.Current.Items[Key];
            if (scope == null)
            {
                scope = new Scope(this, null);
                CurrentScope = scope;
            }

            return scope;
        }
    }

    /// <summary>
    /// An <see cref="IScopeManagerProvider"/> that provides the <see cref="ScopeManager"/>
    /// used by the current <see cref="HttpRequest"/>.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal class PerWebRequestScopeManagerProvider : ScopeManagerProvider
    {
        /// <summary>
        /// Creates a new <see cref="T:LightInject.IScopeManager"/> instance.
        /// </summary>
        /// <param name="serviceFactory">The <see cref="T:LightInject.IServiceFactory"/> to be associated with the <see cref="T:LightInject.IScopeManager"/>.</param>
        /// <returns>
        /// <see cref="T:LightInject.IScopeManager"/>.
        /// </returns>
        protected override IScopeManager CreateScopeManager(IServiceFactory serviceFactory)
        {
            return new PerWebRequestScopeManager(serviceFactory);
        }
    }
}
