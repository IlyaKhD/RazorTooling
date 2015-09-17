// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Dnx.DesignTimeHost;
using Newtonsoft.Json;

namespace Microsoft.AspNet.Tooling.Razor.Application
{
    public class ApplicationMessageBroker : IPluginMessageBroker
    {
        public void SendMessage(object data)
        {
            var stringData = JsonConvert.SerializeObject(data);

            Console.WriteLine(stringData);
        }
    }
}
