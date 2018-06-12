using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Pure.NetCoreExtensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection self, out IConfiguration config, string fileName = "appsettings", bool reloadOnChange= true)
        {
            var services = self.BuildServiceProvider();
            var env = services.GetRequiredService<IHostingEnvironment>();

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"{fileName}.json")
                .AddJsonFile($"{fileName}.{env.EnvironmentName}.json", optional: true, reloadOnChange: reloadOnChange);
            var configuration = builder.Build();
            self.AddSingleton<IConfiguration>(configuration);
            config = configuration;

            ConfigurationManager.RegisterConfiguration(config);
            return self;
        }

        public static IServiceCollection AddConfiguration(this IServiceCollection self, string fileName = "appsettings", bool reloadOnChange = true)
        {
            var services = self.BuildServiceProvider();
            var env = services.GetRequiredService<IHostingEnvironment>();

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"{fileName}.json")
                .AddJsonFile($"{fileName}.{env.EnvironmentName}.json", optional: true, reloadOnChange: reloadOnChange);
            var configuration = builder.Build();
            self.AddSingleton<IConfiguration>(configuration);

            ConfigurationManager.RegisterConfiguration(configuration);

            return self;
        }


        public static IServiceCollection AddConfiguration(this IServiceCollection self, Action<IConfigurationBuilder> action)
        {
            var services = self.BuildServiceProvider();
            var env = services.GetRequiredService<IHostingEnvironment>();

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath);

            if (action != null)
            {
                action(builder);
            }
               
            var configuration = builder.Build();
            self.AddSingleton<IConfiguration>(configuration);

            ConfigurationManager.RegisterConfiguration(configuration);

            return self;
        }



        private const string SettingsFolder = "Settings";

        private const string JsonExtension = "json";
        private const string XmlExtension = "xml";

        /// <summary>
        ///  按文件夹增加配置文件
        /// </summary>
        /// <param name="builder">The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" /> to add to.</param>
        /// <param name="path">
        ///     Path relative to the base path stored in
        ///     <see cref="P:Microsoft.Extensions.Configuration.IConfigurationBuilder.Properties" /> of <paramref name="builder" />
        ///     .
        /// </param>
        /// <param name="optional">Whether the file is optional.</param>
        /// <param name="reloadOnChange">Whether the configuration should be reloaded if the file changes.</param>
        /// <returns>The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" />.</returns>
        public static IConfigurationBuilder AddSettingsFolder(
            this IConfigurationBuilder builder,
            string path = SettingsFolder,
            bool optional = false, bool reloadOnChange = false)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException(nameof(path));

            builder.AddJsonFiles(path, optional, reloadOnChange);
           // builder.AddXmlFiles(path, optional, reloadOnChange);

            return builder;
        }

        #region Providers

        private static void AddJsonFiles(this IConfigurationBuilder builder,
            string path, bool optional, bool reloadOnChange)
        {

            var files = Directory
                .GetFiles($"{Directory.GetCurrentDirectory()}\\{path}")
                .GetFileNames()
                .WhereJson(JsonExtension);

            foreach (var jsonFile in files)
                builder.AddJsonFile($"{path}\\{jsonFile}", optional, reloadOnChange);
        }

        //private static void AddXmlFiles(this IConfigurationBuilder builder,
        //    string path, bool optional, bool reloadOnChange)
        //{
        //    var files = Directory
        //        .GetFiles($"{Directory.GetCurrentDirectory()}\\{path}")
        //        .GetFileNames()
        //        .WhereJson(XmlExtension);

        //    foreach (var jsonFile in files)
        //        builder.AddXmlFile($"{path}\\{jsonFile}", optional, reloadOnChange);
        //}


        /// <summary>
        /// 根据条件新增配置
        /// </summary>
        /// <param name="configurationBuilder">The configuration builder.</param>
        /// <param name="condition">If set to <c>true</c> the action is executed.</param>
        /// <param name="action">The action used to add to the request execution pipeline.</param>
        /// <returns>The same configuration builder.</returns>
        public static IConfigurationBuilder AddIf(
            this IConfigurationBuilder configurationBuilder,
            bool condition,
            Func<IConfigurationBuilder, IConfigurationBuilder> action)
        {
            if (condition)
            {
                configurationBuilder = action(configurationBuilder);
            }

            return configurationBuilder;
        }

        /// <summary>
        /// 根据条件新增配置
        /// </summary>
        /// <param name="configurationBuilder">The configuration builder.</param>
        /// <param name="condition">If set to <c>true</c> the <paramref name="ifAction"/> is executed, otherwise the
        /// <paramref name="elseAction"/> is executed.</param>
        /// <param name="ifAction">The action used to add to the configuration pipeline if the condition is
        /// <c>true</c>.</param>
        /// <param name="elseAction">The action used to add to the configuration pipeline if the condition is
        /// <c>false</c>.</param>
        /// <returns>The same configuration builder.</returns>
        public static IConfigurationBuilder AddIfElse(
            this IConfigurationBuilder configurationBuilder,
            bool condition,
            Func<IConfigurationBuilder, IConfigurationBuilder> ifAction,
            Func<IConfigurationBuilder, IConfigurationBuilder> elseAction)
        {
            if (condition)
            {
                configurationBuilder = ifAction(configurationBuilder);
            }
            else
            {
                configurationBuilder = elseAction(configurationBuilder);
            }

            return configurationBuilder;
        }

        #endregion

        #region Helpers

        private static IEnumerable<string> GetFileNames(this IEnumerable<string> files)
        {
            return files.Select(f => f.Split('\\').LastOrDefault());
        }

        private static IEnumerable<string> WhereJson(this IEnumerable<string> fileNames, string extension)
        {
            return fileNames.Where(f => f.HasExtension(extension));
        }

        private static bool HasExtension(this string fileName, string extension)
        {
            var fileExtension = fileName.Split('.').Last();
            return fileExtension.Equals(extension, StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion

        #region 获取


        /// <summary>
        ///   获取连接字符串，默认节点名称 Default
        /// </summary>
        /// <param name="configuration">
        ///     <see cref="T:Microsoft.Extensions.Configuration.IConfiguration" /> to get the connection
        ///     string from.
        /// </param>
        /// <returns>The "Default" connection string.</returns>
        public static string GetConnectionString(this IConfiguration configuration, string connKey = "DefaultConnection")
        {
            return configuration.GetValue<string>("ConnectionStrings:"+ connKey);
        }

        #endregion
    }
}
