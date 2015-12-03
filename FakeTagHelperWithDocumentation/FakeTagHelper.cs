using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Razor.TagHelpers;

namespace FakeTagHelperWithDocumentation {

    /// <summary>
    /// Class summary goes here
    /// </summary>
    [HtmlTargetElement("FakeTagHelper", TagStructure = TagStructure.WithoutEndTag, ParentTag = "stub-parent")]
    public class FakeTagHelper : TagHelper {

        /// <summary>
        /// Prop summary goes here
        /// </summary>
        public string Prop1 { get; set; }
    }

}
