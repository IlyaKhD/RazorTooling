// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Dnx.Runtime;
using Newtonsoft.Json.Linq;

namespace Microsoft.AspNet.Tooling.Razor.Application
{
    public class Program
    {
        private IAssemblyLoadContext _assemblyLoadContext;

        public Program(IAssemblyLoadContextAccessor assemblyLoadContextAccessor)
        {
            _assemblyLoadContext = assemblyLoadContextAccessor.Default;
        }

        public void Main(string[] messages)
        {
            var messageBroker = new ApplicationMessageBroker();
            var plugin = new RazorPlugin(messageBroker);

            foreach (var message in messages)
            {
                plugin.ProcessMessage(JObject.Parse(message), _assemblyLoadContext);
            }
        }
    }
}
