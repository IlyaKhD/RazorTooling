using Microsoft.AspNet.Razor.TagHelpers;

namespace LocalFakeTagHelperWithSummary {

    [HtmlTargetElement("Stub1TagHelper", TagStructure = TagStructure.WithoutEndTag, ParentTag = "stub-parent")]
    public class Stub1TagHelper : TagHelper {

        /// <summary>summary goes here (local)</summary>
        public string Prop1 { get; set; }


    }
}