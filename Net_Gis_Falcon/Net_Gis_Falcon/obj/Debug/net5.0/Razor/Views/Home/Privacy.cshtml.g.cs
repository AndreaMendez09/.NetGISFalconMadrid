#pragma checksum "D:\Atos\RepositorioGIT\.NetGISFalconMadrid\Net_GIS_Falcon\Net_Gis_Falcon\Views\Home\Privacy.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "224bcf1553c5fa29fdc7c4da857d4ce754c3dc66"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Privacy), @"mvc.1.0.view", @"/Views/Home/Privacy.cshtml")]
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
#line 1 "D:\Atos\RepositorioGIT\.NetGISFalconMadrid\Net_GIS_Falcon\Net_Gis_Falcon\Views\_ViewImports.cshtml"
using Net_Gis_Falcon;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "D:\Atos\RepositorioGIT\.NetGISFalconMadrid\Net_GIS_Falcon\Net_Gis_Falcon\Views\Home\Privacy.cshtml"
using Net_Gis_Falcon.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"224bcf1553c5fa29fdc7c4da857d4ce754c3dc66", @"/Views/Home/Privacy.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"85c566102185e311abfadcddba8629d4b791d1b2", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Privacy : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Persona>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "D:\Atos\RepositorioGIT\.NetGISFalconMadrid\Net_GIS_Falcon\Net_Gis_Falcon\Views\Home\Privacy.cshtml"
  
    ViewData["Title"] = "Privacy Policy";

#line default
#line hidden
#nullable disable
            WriteLiteral("<h1>");
#nullable restore
#line 7 "D:\Atos\RepositorioGIT\.NetGISFalconMadrid\Net_GIS_Falcon\Net_Gis_Falcon\Views\Home\Privacy.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n\r\n<p>Use this page to detail your site\'s privacy policy.</p>\r\n\r\n");
#nullable restore
#line 11 "D:\Atos\RepositorioGIT\.NetGISFalconMadrid\Net_GIS_Falcon\Net_Gis_Falcon\Views\Home\Privacy.cshtml"
 if (User.Identity.IsAuthenticated)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <p>Hola Lo veo si estoy autenticado</p>\r\n");
#nullable restore
#line 14 "D:\Atos\RepositorioGIT\.NetGISFalconMadrid\Net_GIS_Falcon\Net_Gis_Falcon\Views\Home\Privacy.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<h4>Usuarios</h4>\r\n<hr />\r\n<table cellpadding=\"0\" cellspacing=\"0\">\r\n    <tr>\r\n        <th>Usuario_id</th>\r\n        <th>Nombre</th>\r\n    </tr>\r\n");
#nullable restore
#line 24 "D:\Atos\RepositorioGIT\.NetGISFalconMadrid\Net_GIS_Falcon\Net_Gis_Falcon\Views\Home\Privacy.cshtml"
     foreach (Persona customer in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>");
#nullable restore
#line 27 "D:\Atos\RepositorioGIT\.NetGISFalconMadrid\Net_GIS_Falcon\Net_Gis_Falcon\Views\Home\Privacy.cshtml"
           Write(customer.IdPersona);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 28 "D:\Atos\RepositorioGIT\.NetGISFalconMadrid\Net_GIS_Falcon\Net_Gis_Falcon\Views\Home\Privacy.cshtml"
           Write(customer.Nombre);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n");
#nullable restore
#line 30 "D:\Atos\RepositorioGIT\.NetGISFalconMadrid\Net_GIS_Falcon\Net_Gis_Falcon\Views\Home\Privacy.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</table>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Persona>> Html { get; private set; }
    }
}
#pragma warning restore 1591
