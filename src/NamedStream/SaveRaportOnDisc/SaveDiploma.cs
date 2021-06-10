using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SaveDiplomaOnDisc;
using Soneta.Business;
using Soneta.Kadry;

[assembly: Worker(typeof(SaveDiploma), typeof(Pracownicy))]
namespace SaveDiplomaOnDisc
{
    internal class SaveDiploma
    {
        [Context] public Pracownik[] Pracownicy { get; set; }

        [Action("Utwórz dane do drukarni",
            Target = ActionTarget.Menu,
            Mode = ActionMode.SingleSession,
            Priority = 0)]
        public NamedStream SaveData()
        {
            var memoryStream = new MemoryStream();
            var document = new Document(PageSize.A4, 10, 10, 10, 10);
            PdfWriter.GetInstance(document, memoryStream);
            document.Open();
            foreach (var pracownik in Pracownicy)
                CreatePage(document, pracownik);
            document.Close();
            var bytes = memoryStream.ToArray();
            memoryStream.Close();
            return new NamedStream("dyplomy.pdf", bytes);
        }

        private static void CreatePage(Document document, Pracownik pracownik)
        {
            document.NewPage();
            document.AddTitle("Dyplom");
            var headerParagraph = new Paragraph("DYPLOM \n\n") {Alignment = Element.ALIGN_CENTER};
            document.Add(headerParagraph);
            var para = new Paragraph("Nagroda dla pracownika: " + pracownik.Nazwa + "\n za szczególne osiągnięcia w pracy") { Alignment = Element.ALIGN_CENTER }; ;
            document.Add(para);
        }
    }
}