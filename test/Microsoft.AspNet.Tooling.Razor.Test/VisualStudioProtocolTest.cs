﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Razor.Compilation.TagHelpers;
using Microsoft.AspNet.Razor.TagHelpers;
using Newtonsoft.Json;
using Xunit;

namespace Microsoft.AspNet.Tooling.Razor
{
    public class VisualStudioProtocolTest
    {
        [Fact]
        public void TagHelperDescrictorsAreCompatibleWithPinnedVisualStudioVersion()
        {
            // Arrange
            var runtimeDescriptor = new TagHelperDescriptor
            {
                AllowedChildren = new[] { "tr", "td" },
                AssemblyName = "CustomAssembly",
                Prefix = "th:",
                RequiredAttributes = new[] { "runat" },
                RequiredParent = "body",
                TagName = "custom-table",
                TagStructure = TagStructure.NormalOrSelfClosing,
                TypeName = "Custom.Type.TableTagHelper",
                DesignTimeDescriptor = new TagHelperDesignTimeDescriptor
                {
                    OutputElementHint = "table",
                    Remarks = "Some tag level remarks.",
                    Summary = "Some tag level summary."
                },
                Attributes = new[]
                {
                    new TagHelperAttributeDescriptor
                    {
                        IsIndexer = false,
                        IsStringProperty = true,
                        Name = "bind",
                        PropertyName = "Bind",
                        TypeName = "System.String",
                        DesignTimeDescriptor = new TagHelperAttributeDesignTimeDescriptor
                        {
                            Remarks = "Some attribute level remarks.",
                            Summary = "Some attribute level summary."
                        }
                    },
                    new TagHelperAttributeDescriptor
                    {
                        IsIndexer = false,
                        IsEnum = true,
                        IsStringProperty = false,
                        Name = "bind-enum",
                        PropertyName = "BindEnum",
                        TypeName = "MyEnumNamespace",
                        DesignTimeDescriptor = new TagHelperAttributeDesignTimeDescriptor
                        {
                            Remarks = "Some enum attribute level remarks.",
                            Summary = "Some enum attribute level summary."
                        }
                    }
                }
            };
            var expectedVSDescriptor = new VisualStudioTagHelperDescriptor
            {
                AllowedChildren = new[] { "tr", "td" },
                AssemblyName = "CustomAssembly",
                Prefix = "th:",
                RequiredAttributes = new[] { "runat" },
                RequiredParent = "body",
                TagName = "custom-table",
                TagStructure = VisualStudioTagStructure.NormalOrSelfClosing,
                TypeName = "Custom.Type.TableTagHelper",
                DesignTimeDescriptor = new VisualStudioTagHelperDesignTimeDescriptor
                {
                    OutputElementHint = "table",
                    Remarks = "Some tag level remarks.",
                    Summary = "Some tag level summary."
                },
                Attributes = new[]
                {
                    new VisualStudioTagHelperAttributeDescriptor
                    {
                        IsIndexer = false,
                        IsStringProperty = true,
                        Name = "bind",
                        PropertyName = "Bind",
                        TypeName = "System.String",
                        DesignTimeDescriptor = new VisualStudioTagHelperAttributeDesignTimeDescriptor
                        {
                            Remarks = "Some attribute level remarks.",
                            Summary = "Some attribute level summary."
                        }
                    },
                    new VisualStudioTagHelperAttributeDescriptor
                    {
                        IsIndexer = false,
                        IsEnum = true,
                        IsStringProperty = false,
                        Name = "bind-enum",
                        PropertyName = "BindEnum",
                        TypeName = "MyEnumNamespace",
                        DesignTimeDescriptor = new VisualStudioTagHelperAttributeDesignTimeDescriptor
                        {
                            Remarks = "Some enum attribute level remarks.",
                            Summary = "Some enum attribute level summary."
                        }
                    }
                }
            };
            var serializedRuntimeDescriptor = JsonConvert.SerializeObject(runtimeDescriptor);

            // Act
            var vsDescriptor =
                JsonConvert.DeserializeObject<VisualStudioTagHelperDescriptor>(serializedRuntimeDescriptor);

            // Assert
            Assert.Equal(expectedVSDescriptor.AllowedChildren, vsDescriptor.AllowedChildren, StringComparer.Ordinal);
            Assert.Equal(expectedVSDescriptor.AssemblyName, vsDescriptor.AssemblyName, StringComparer.Ordinal);
            Assert.Equal(expectedVSDescriptor.Prefix, vsDescriptor.Prefix, StringComparer.Ordinal);
            Assert.Equal(expectedVSDescriptor.RequiredAttributes, vsDescriptor.RequiredAttributes, StringComparer.Ordinal);
            Assert.Equal(expectedVSDescriptor.RequiredParent, vsDescriptor.RequiredParent, StringComparer.Ordinal);
            Assert.Equal(expectedVSDescriptor.TagName, vsDescriptor.TagName, StringComparer.Ordinal);
            Assert.Equal(expectedVSDescriptor.TagStructure, vsDescriptor.TagStructure);
            Assert.Equal(expectedVSDescriptor.TypeName, vsDescriptor.TypeName, StringComparer.Ordinal);

            var dtDescriptor = vsDescriptor.DesignTimeDescriptor;
            var expectedDTDescriptor = expectedVSDescriptor.DesignTimeDescriptor;
            Assert.Equal(expectedDTDescriptor.OutputElementHint, dtDescriptor.OutputElementHint, StringComparer.Ordinal);
            Assert.Equal(expectedDTDescriptor.Remarks, dtDescriptor.Remarks, StringComparer.Ordinal);
            Assert.Equal(expectedDTDescriptor.Summary, dtDescriptor.Summary, StringComparer.Ordinal);

            var attributes = vsDescriptor.Attributes.OrderBy(attr => attr.Name).ToArray();
            var expectedAttributes = expectedVSDescriptor.Attributes.OrderBy(attr => attr.Name).ToArray();
            Assert.Equal(attributes.Length, expectedAttributes.Length);

            for (var i = 0; i < attributes.Length; i++)
            {
                var attribute = attributes[i];
                var expectedAttribute = expectedAttributes[i];
                Assert.Equal(attribute.IsIndexer, expectedAttribute.IsIndexer);
                Assert.Equal(attribute.IsEnum, expectedAttribute.IsEnum);
                Assert.Equal(attribute.IsStringProperty, expectedAttribute.IsStringProperty);
                Assert.Equal(attribute.Name, expectedAttribute.Name, StringComparer.Ordinal);
                Assert.Equal(attribute.PropertyName, expectedAttribute.PropertyName, StringComparer.Ordinal);
                Assert.Equal(attribute.TypeName, expectedAttribute.TypeName, StringComparer.Ordinal);

                var dtAttribute = attribute.DesignTimeDescriptor;
                var expectedDTAttribute = expectedAttribute.DesignTimeDescriptor;
                Assert.Equal(dtAttribute.Remarks, expectedDTAttribute.Remarks, StringComparer.Ordinal);
                Assert.Equal(dtAttribute.Summary, expectedDTAttribute.Summary, StringComparer.Ordinal);
            }
        }

        #region PinnedVisualStudioTagHelperDescriptors
        // RC2 Razor TagHelperDescriptor Snapshot
        public class VisualStudioTagHelperAttributeDesignTimeDescriptor
        {
            public string Summary { get; set; }
            public string Remarks { get; set; }
        }

        private class VisualStudioTagHelperDesignTimeDescriptor
        {
            public string Summary { get; set; }
            public string Remarks { get; set; }
            public string OutputElementHint { get; set; }
        }

        private class VisualStudioTagHelperAttributeDescriptor
        {
            private string _typeName;
            private string _name;
            private string _propertyName;

            public bool IsIndexer { get; set; }
            public bool IsEnum { get; set; }
            public bool IsStringProperty { get; set; }
            public string Name
            {
                get
                {
                    return _name;
                }
                set
                {
                    if (value == null)
                    {
                        throw new ArgumentNullException(nameof(value));
                    }

                    _name = value;
                }
            }
            public string PropertyName
            {
                get
                {
                    return _propertyName;
                }
                set
                {
                    if (value == null)
                    {
                        throw new ArgumentNullException(nameof(value));
                    }

                    _propertyName = value;
                }
            }
            public string TypeName
            {
                get
                {
                    return _typeName;
                }
                set
                {
                    if (value == null)
                    {
                        throw new ArgumentNullException(nameof(value));
                    }

                    _typeName = value;
                    IsStringProperty = string.Equals(TypeName, typeof(string).FullName, StringComparison.Ordinal);
                }
            }
            public VisualStudioTagHelperAttributeDesignTimeDescriptor DesignTimeDescriptor { get; set; }
            public bool IsNameMatch(string name)
            {
                if (IsIndexer)
                {
                    return name.StartsWith(Name, StringComparison.OrdinalIgnoreCase);
                }
                else
                {
                    return string.Equals(name, Name, StringComparison.OrdinalIgnoreCase);
                }
            }
        }

        private class VisualStudioTagHelperDescriptor
        {
            private string _prefix = string.Empty;
            private string _tagName;
            private string _typeName;
            private string _assemblyName;
            private IEnumerable<VisualStudioTagHelperAttributeDescriptor> _attributes =
                Enumerable.Empty<VisualStudioTagHelperAttributeDescriptor>();
            private IEnumerable<string> _requiredAttributes = Enumerable.Empty<string>();

            public string Prefix
            {
                get
                {
                    return _prefix;
                }
                set
                {
                    if (value == null)
                    {
                        throw new ArgumentNullException(nameof(value));
                    }

                    _prefix = value;
                }
            }
            public string TagName
            {
                get
                {
                    return _tagName;
                }
                set
                {
                    if (value == null)
                    {
                        throw new ArgumentNullException(nameof(value));
                    }

                    _tagName = value;
                }
            }
            public string FullTagName
            {
                get
                {
                    return Prefix + TagName;
                }
            }
            public string TypeName
            {
                get
                {
                    return _typeName;
                }
                set
                {
                    if (value == null)
                    {
                        throw new ArgumentNullException(nameof(value));
                    }

                    _typeName = value;
                }
            }
            public string AssemblyName
            {
                get
                {
                    return _assemblyName;
                }
                set
                {
                    if (value == null)
                    {
                        throw new ArgumentNullException(nameof(value));
                    }

                    _assemblyName = value;
                }
            }
            public IEnumerable<VisualStudioTagHelperAttributeDescriptor> Attributes
            {
                get
                {
                    return _attributes;
                }
                set
                {
                    if (value == null)
                    {
                        throw new ArgumentNullException(nameof(value));
                    }

                    _attributes = value;
                }
            }
            public IEnumerable<string> RequiredAttributes
            {
                get
                {
                    return _requiredAttributes;
                }
                set
                {
                    if (value == null)
                    {
                        throw new ArgumentNullException(nameof(value));
                    }

                    _requiredAttributes = value;
                }
            }
            public IEnumerable<string> AllowedChildren { get; set; }
            public string RequiredParent { get; set; }
            public VisualStudioTagStructure TagStructure { get; set; }
            public VisualStudioTagHelperDesignTimeDescriptor DesignTimeDescriptor { get; set; }
        }
        public enum VisualStudioTagStructure
        {
            Unspecified,
            NormalOrSelfClosing,
            WithoutEndTag
        }
        #endregion
    }
}
