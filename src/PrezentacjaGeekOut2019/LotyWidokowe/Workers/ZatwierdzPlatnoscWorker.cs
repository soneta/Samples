using System;
using PrezentacjaGeekOut2019.LotyWidokowe;
using Soneta.Business;
using Soneta.Business.UI;
using ZatwierdzPlatnoscWorker = PrezentacjaGeekOut2019.LotyWidokowe.Workers.ZatwierdzPlatnoscWorker;

[assembly: Worker(typeof(ZatwierdzPlatnoscWorker), typeof(Rezerwacja))]
namespace PrezentacjaGeekOut2019.LotyWidokowe.Workers
{
    public class ZatwierdzPlatnoscWorker
    {
        [Context]
        public Rezerwacja Rezerwacja { get; set; }

        [Context]
        public Context Context { get; set; }

        
        [Action("Zatwierdź płatnośc", Mode = ActionMode.SingleSession | ActionMode.ConfirmSave | ActionMode.Progress, Icon = ActionIcon.Accept, Target = ActionTarget.Toolbar)]
        public void ZatwierdzPlatnosc()
        {
            using (ITransaction t = Context.Session.Logout(true))
            {
                Rezerwacja.CzyOplacona = CzyOplacone.Oplacone;
                t.Commit();
            }



        }
    }


}
