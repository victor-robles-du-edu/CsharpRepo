#pragma checksum "C:\Users\User\Desktop\ICT 4351\Projects\Store\WebAppStore\Views\Home\Tablets.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "654850353b20654a2bd7b53252b93e9041e589b6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Tablets), @"mvc.1.0.view", @"/Views/Home/Tablets.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\User\Desktop\ICT 4351\Projects\Store\WebAppStore\Views\_ViewImports.cshtml"
using WebAppStore;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\User\Desktop\ICT 4351\Projects\Store\WebAppStore\Views\_ViewImports.cshtml"
using WebAppStore.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"654850353b20654a2bd7b53252b93e9041e589b6", @"/Views/Home/Tablets.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"332c571eabe3b8a2d2faf06e0880579c058c82eb", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Tablets : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\User\Desktop\ICT 4351\Projects\Store\WebAppStore\Views\Home\Tablets.cshtml"
  
    ViewData["Title"] = "Tablets Prodcuts";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"text-center\">\r\n    <h1>");
#nullable restore
#line 6 "C:\Users\User\Desktop\ICT 4351\Projects\Store\WebAppStore\Views\Home\Tablets.cshtml"
   Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h1>
    <h3>
        <p>
        <font size=""5"" face=""Courier New"">
            <table border=""1"" width=""100%"">
                <tr>
                    <th>Name</th>
                    <th>Description</th>
                    <th>Brand</th>
                    <th>Price</th>
                </tr>
");
#nullable restore
#line 17 "C:\Users\User\Desktop\ICT 4351\Projects\Store\WebAppStore\Views\Home\Tablets.cshtml"
                 foreach (var p in Program.Products.Where(x => x.Type == "Tablet").ToList())
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td>");
#nullable restore
#line 20 "C:\Users\User\Desktop\ICT 4351\Projects\Store\WebAppStore\Views\Home\Tablets.cshtml"
                       Write(p.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 21 "C:\Users\User\Desktop\ICT 4351\Projects\Store\WebAppStore\Views\Home\Tablets.cshtml"
                       Write(p.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 22 "C:\Users\User\Desktop\ICT 4351\Projects\Store\WebAppStore\Views\Home\Tablets.cshtml"
                       Write(p.Brand);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td style=\"text-align:right\">");
#nullable restore
#line 23 "C:\Users\User\Desktop\ICT 4351\Projects\Store\WebAppStore\Views\Home\Tablets.cshtml"
                                                Write(p.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    </tr>\r\n");
#nullable restore
#line 25 "C:\Users\User\Desktop\ICT 4351\Projects\Store\WebAppStore\Views\Home\Tablets.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </table>\r\n        </font>\r\n        </p>\r\n    </h3>\r\n\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
