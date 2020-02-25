using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soneta.Business;
using Soneta.Business.Db;
using Soneta.CRM;
using Samples.Example2.UI.Extender;

[assembly: Worker(typeof(KontrahentNewOgolneExtender))]

namespace Samples.Example2.UI.Extender
{
    class KontrahentNewOgolneExtender
    {
        // Atrybut Context pozwala na wskazanie mechanizmowi tworzenia extenderów
        // jakie property powinny zostać ustawione automatycznie po utworzeniu obiektu
        // extendera. Jeśli w kontekście istnieje obiekt pasujący typem do opisanego 
        // atrybutem property, to po utworzeniu obiektu extendera wartość property zostanie
        // ustawiona wartością znajdującą sie w kontekście (obiekt Context).
        [Context]
        public Kontrahent Kontrahent { get; set; }

        public Image Logo
        {
            get
            {
                // Wyszukujemy w załącznikach pierwszy o typie obraz, który jest oznaczony jako domyślny
                foreach (Attachment attachemnt in Kontrahent.Attachments)
                    if (attachemnt.SubType == SubTypeType.Picture && attachemnt.IsDefault)
                        // i zwracamy go na zewnątrz
                        return attachemnt.DataAsImage;

                // lub zwracamy null
                return null;
            }
        }
    }
}
