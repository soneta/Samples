using Soneta.Business.UI;

namespace DynamicForms.Step3.Extender {
    public partial class Step3Extender {
        private GroupContainer root;

        public UIElement GetStackElement() {
            checkBuffer();
            if (root == null) {
                root = new GroupContainer {
                    CaptionHtml = "Generowana lista spotkań",
                    Height = "*",
                    Width = "*"
                };

                root.Elements.Add(GetFilterElements());
                root.Elements.Add(new GapElement { Height = "1", Width = "1" });
                root.Elements.Add(GetGroupElements());
            }
            return root;
        }

        private UIElement GetFilterElements() {
            var flow = new FlowContainer(new FieldElement {
                EditValue = "{FilterParameters.Month}",
                CaptionHtml = "Spotkania w miesiącu",
                Width = "20"
            });
            return flow;
        }

        private UIElement GetGroupElements() {
            var flow = new FlowContainer();
            var tasks = Tasks;
            for (var i = 0; i < tasks.Length; i++) {
                flow.Elements.Add(getUIElement(i));
            }
            return flow;
        }

        private UIElement getUIElement(int i) {
            var env = new EnvironmentExtender();
            var row = new RowContainer();
            var group = new GroupContainer {
                DataContext = "{Tasks[" + i + "]}",
                CaptionHtml = "{Title}",
                Width = "350px",
                Height = "80px"
            };

            #region stackLeft

            var stackLeft = new StackContainer();
            var image = new FieldElement {
                CaptionHtml = "",
                EditValue = "{Zadanie.Przedstawiciel.DefaultImage}",
                Width = "110px",
                Height = "150px",
                Class = new UIClass[] { UIClass.ImageEdit }
            };

            stackLeft.Elements.Add(image);
            row.Elements.Add(stackLeft);

            #endregion stackLeft

            #region stackRight

            var stackRight = new StackContainer {
                LabelWidth = "20"
            };

            var labelContact = new LabelElement {
                CaptionHtml = "{Zadanie.Przedstawiciel.Nazwa}",
                Class = new UIClass[] { UIClass.BoldLabel, UIClass.GreenFont },
                Width = "20"
            };

            var labelPhone = new LabelElement {
                CaptionHtml = "{Zadanie.Przedstawiciel.Kontakt.TelefonKomorkowy}",
                Class = new UIClass[] { UIClass.BoldLabel, UIClass.GreenFont },
                Width = "16"
            };

            var labelCompany = new LabelElement {
                CaptionHtml = "{Zadanie.Kontrahent.Kod}",
                Width = "20"
            };

            var labelAddress1 = new LabelElement {
                CaptionHtml = "{Zadanie.Kontrahent.Adres.Linia1}",
                Width = "30"
            };

            var labelAddress2 = new LabelElement {
                CaptionHtml = "{Zadanie.Kontrahent.Adres.Linia2}",
                Width = "30"
            };

            var command = new CommandElement {
                MethodName = "ShowLocalization",
                CaptionHtml = "Zobacz dojazd",
                Width = "*"
            };

            stackRight.Elements.Add(labelContact);
            stackRight.Elements.Add(labelPhone);
            stackRight.Elements.Add(labelCompany);
            stackRight.Elements.Add(labelAddress1);
            stackRight.Elements.Add(labelAddress2);
            stackRight.Elements.Add(command);

            row.Elements.Add(stackRight);

            #endregion stackRight

            if (env.IsHtml) {
                stackLeft.Class = new[] { UIClass.Tight };
                stackRight.Class = new[] { UIClass.Tight };
            }

            group.Elements.Add(row);
            group.Elements.Add(new GapElement {
                Height = "1",
                Width = "0"
            });

            return group;
        }
    }
}
