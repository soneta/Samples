using System;
using System.IO;
using System.Diagnostics;
using System.Reflection;

using Soneta.Business;
using Soneta.Business.UI;
using Soneta.Handel;
using Soneta.Business.Db;

using Microsoft.Extensions.DependencyInjection;

[assembly: Worker(typeof(Geeks.Printout), typeof(DokumentHandlowy), 
    MenuRootCaption = "Wydruki", MenuRootIconName = "lista")]

namespace Geeks
{
    class Printout
    {
        #region Worker0

        [Action("Wydruk0",
            Mode = ActionMode.SingleSession,
            Target = ActionTarget.Menu | ActionTarget.Toolbar)]
        public object Go0()
            => new ReportResult { ReportName = "Dokument/Dokument sprzedaży", Preview = true };

        #endregion

        #region Worker1

        [Action("Wydruk1",
            Mode = ActionMode.SingleSession,
            Target = ActionTarget.Menu | ActionTarget.Toolbar)]
        public object Go1(Context context)
            => new ReportResult
               {
                    Context = context,
                    Format = ReportResultFormat.PDF,
                    TemplateFileName = "Sprzedaz.repx",
                    DataType = typeof(DokumentHandlowy),
                    OutputHandler = (stream) => AsNamedStream(stream, "wydruk1.pdf")
               };

        #endregion

        #region Worker2

        [Action("Wydruk2",
            Mode = ActionMode.SingleSession,
            Target = ActionTarget.Menu | ActionTarget.Toolbar)]
        public object Go2(Context context)
        {
            var paramsType = Type.GetType("Soneta.Handel.Reports.SprzedazSnippet+MyParametryWydruku,Soneta.Handel.Reports");
            context[paramsType] = Activator.CreateInstance(paramsType, context);

            var result = new ReportResult {
                Context = context,
                Format = ReportResultFormat.PDF,
                TemplateFileName = "Sprzedaz.repx",
                DataType = typeof(DokumentHandlowy)
            };

            var service = context.Session.GetRequiredService<IReportService>();
            var stream = service.GenerateReport(result);
            return AsNamedStream(stream, "wydruk2.pdf");
        }

        #endregion

        #region Worker3

        [Action("Wydruk3",
            Mode = ActionMode.SingleSession,
            Target = ActionTarget.Menu | ActionTarget.Toolbar)]
        public object Go3(Context context)
        {
            var paramsType = Type.GetType("Soneta.Handel.Reports.SprzedazSnippet+MyParametryWydruku,Soneta.Handel.Reports");
            context[paramsType] = Activator.CreateInstance(paramsType, context);

            var sysFile = context.Session.GetBusiness()
                .SystemFiles.ByName[SystemFileTypes.DxSnippet, "CustomReportSnippet"];
            paramsType = context.Session.AssemblyCache.GetType(sysFile, "Geeks.CustomReportSnippet+CustomParams");
            context[paramsType] = Activator.CreateInstance(paramsType, context);

            var result = new ReportResult
            {
                Context = context,
                Format = ReportResultFormat.PDF,
                TemplateFileName = "Sprzedaz2.repx",
                DataType = typeof(DokumentHandlowy)
            };

            var service = context.Session.GetRequiredService<IReportService>();
            var stream = service.GenerateReport(result);
            return AsNamedStream(stream, "wydruk3.pdf");
        }

        #endregion

        #region Print

        private static NamedStream AsNamedStream(Stream stream, string fileName)
            => new NamedStream(fileName, () => stream);

        private static string AsLocalFile(Stream stream, string fileName)
        {
            var path = Path.Combine(OutputDirectory, fileName);
            using (stream)
                using (var output = File.Create(path))
                    stream.CopyTo(output);
            if (!File.Exists(path))
                throw new Exception("IO ERROR: Nie można zapisać pliku");
            OpenOutput(path);
            return path;
        }

        private static void OpenOutput(string path)
            => new Process()
            {
                StartInfo = new ProcessStartInfo() { FileName = path, UseShellExecute = true }
            }.Start();

        private static string OutputDirectory
        {
            get
            {
                var bin = Assembly.GetAssembly(typeof(Printout)).Location;
                var root = new DirectoryInfo(Path.GetDirectoryName(bin))
                    .Parent.Parent.Parent.FullName;
                var output = Path.Combine(root, "Output");
                if (!Directory.Exists(output))
                    Directory.CreateDirectory(output);
                return output;
            }
        }
        //= Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        #endregion
    }

}
