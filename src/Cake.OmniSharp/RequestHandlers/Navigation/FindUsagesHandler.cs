using System.Composition;
using OmniSharp.Mef;
using OmniSharp.Models;
using OmniSharp;
using OmniSharp.Roslyn.CSharp.Services.Navigation;

namespace Cake.OmniSharp.RequestHandlers.Navigation
{
    [OmniSharpHandler(OmnisharpEndpoints.FindUsages, Constants.LanguageNames.Cake)]
    public class FindUsagesHandler : CakeRequestHandler<FindUsagesService, FindUsagesRequest, QuickFixResponse>
    {
        [ImportingConstructor]
        public FindUsagesHandler(
            OmniSharpWorkspace workspace)
            : base(workspace, new FindUsagesService(workspace))
        {
        }
    }
}