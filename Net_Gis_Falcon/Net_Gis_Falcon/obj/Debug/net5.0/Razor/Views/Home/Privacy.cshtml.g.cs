#pragma checksum "D:\FTC_ATOS_DAM\ProyectoGIS\.NetGISFalconMadrid\Net_GIS_Falcon\Net_Gis_Falcon\Views\Home\Privacy.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6f3d107a1e90b6720c511c2af3b9045b3c64389c"
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
#line 1 "D:\FTC_ATOS_DAM\ProyectoGIS\.NetGISFalconMadrid\Net_GIS_Falcon\Net_Gis_Falcon\Views\_ViewImports.cshtml"
using Net_Gis_Falcon;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\FTC_ATOS_DAM\ProyectoGIS\.NetGISFalconMadrid\Net_GIS_Falcon\Net_Gis_Falcon\Views\_ViewImports.cshtml"
using Net_Gis_Falcon.Views;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "D:\FTC_ATOS_DAM\ProyectoGIS\.NetGISFalconMadrid\Net_GIS_Falcon\Net_Gis_Falcon\Views\Home\Privacy.cshtml"
using Net_Gis_Falcon.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6f3d107a1e90b6720c511c2af3b9045b3c64389c", @"/Views/Home/Privacy.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5b33cdcbeaacdcc45d0601dee59d0c435a3b5bbd", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Privacy : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\FTC_ATOS_DAM\ProyectoGIS\.NetGISFalconMadrid\Net_GIS_Falcon\Net_Gis_Falcon\Views\Home\Privacy.cshtml"
  
    ViewData["Title"] = "Privacy Policy";

#line default
#line hidden
#nullable disable
            WriteLiteral("<h1>");
#nullable restore
#line 6 "D:\FTC_ATOS_DAM\ProyectoGIS\.NetGISFalconMadrid\Net_GIS_Falcon\Net_Gis_Falcon\Views\Home\Privacy.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n\r\n<p>Use this page to detail your site\'s privacy policy.</p>\r\n\r\n");
#nullable restore
#line 10 "D:\FTC_ATOS_DAM\ProyectoGIS\.NetGISFalconMadrid\Net_GIS_Falcon\Net_Gis_Falcon\Views\Home\Privacy.cshtml"
 if (User.Identity.IsAuthenticated)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <p>Hola Lo veo si estoy autenticado</p>\r\n");
#nullable restore
#line 13 "D:\FTC_ATOS_DAM\ProyectoGIS\.NetGISFalconMadrid\Net_GIS_Falcon\Net_Gis_Falcon\Views\Home\Privacy.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<h4>Usuarios</h4>\r\n<hr />\r\n<table cellpadding=\"0\" cellspacing=\"0\">\r\n    <tr>\r\n        <th>Usuario_id</th>\r\n        <th>Nombre</th>\r\n    </tr>\r\n\r\n</table>\r\n");
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
