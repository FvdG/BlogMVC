//===================================================================================
// Microsoft patterns & practices
//===================================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===================================================================================

using Blog.Data;
using Blog.Data.Repositories;
using Blog.Domain.Interfaces;
using Blog.Web.UnityExtensions;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Blog.Web
{
    public class UnityContainerFactory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability",
            "CA2000:Dispose objects before losing scope", Justification = "Container has the scope of the application.")
        ]
        public IUnityContainer CreateConfiguredContainer()
        {
            var container = new UnityContainer();
            //LoadConfigurationOverrides(container);
            container
                .RegisterType<ICategoryRepository, CategoryRepository>()
                .RegisterType<IPostRepository, PostRepository>()
                .RegisterType<ITagRepository, TagRepository>()
                .RegisterType<IUserRepository, UserRepository>()
                .RegisterType<IRoleRepository, RoleRepository>()
                .RegisterType<IUnitOfWork, BlogContext>(new UnityHttpContextPerRequestLifetimeManager());
            return container;
        }

        private static void LoadConfigurationOverrides(IUnityContainer container)
        {
            container.LoadConfiguration("container");
        }
    }
}