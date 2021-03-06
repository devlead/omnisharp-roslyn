using System.Composition;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;
using OmniSharp.DotNetTest.Helpers.DotNetTestManager;
using OmniSharp.DotNetTest.Models;
using OmniSharp.Mef;
using OmniSharp.Services;

namespace OmniSharp.DotNetTest.Services
{
    [OmniSharpHandler(OmnisharpEndpoints.GetTestStartInfo, LanguageNames.CSharp)]
    public class GetTestStartInfoService : RequestHandler<GetTestStartInfoRequest, GetTestStartInfoResponse>
    {
        private readonly OmniSharpWorkspace _workspace;
        private readonly DotNetCliService _dotNetCli;
        private readonly ILoggerFactory _loggerFactory;

        [ImportingConstructor]
        public GetTestStartInfoService(OmniSharpWorkspace workspace, DotNetCliService dotNetCli, ILoggerFactory loggerFactory)
        {
            _workspace = workspace;
            _dotNetCli = dotNetCli;
            _loggerFactory = loggerFactory;
        }

        public Task<GetTestStartInfoResponse> Handle(GetTestStartInfoRequest request)
        {
            var document = _workspace.GetDocument(request.FileName);
            var projectFolder = Path.GetDirectoryName(document.Project.FilePath);

            using (var dtm = DotNetTestManager.Start(projectFolder, _dotNetCli, _loggerFactory))
            {
                var response = dtm.GetTestStartInfo(request.MethodName, request.TestFrameworkName);
                return Task.FromResult(response);
            }
        }
    }
}
