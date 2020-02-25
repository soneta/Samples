﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Samples.Example9.UI.Extender;
using Soneta.Business;
using Soneta.Business.UI;

[assembly: Worker(typeof(DynamicFormExtender))]

namespace Samples.Example9.UI.Extender
{
    class DynamicFormExtender
    {

        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public string Field3 { get; set; }

        public UIElement GetSource()
        {
            var stack = new StackContainer();
            var group = new GroupContainer { CaptionHtml = "Tytuł grupy", LabelHeight = "10" };
            var row = new RowContainer();
            var rowCmd = new RowContainer();

            var field1 = new FieldElement { CaptionHtml = "Pole 1", EditValue = "{Field1}", OuterWidth = "30" };
            var field2 = new FieldElement { CaptionHtml = "Pole 2", EditValue = "{Field2}", OuterWidth = "30" };
            var field3 = new FieldElement { CaptionHtml = "Pole 3", EditValue = "{Field3}", OuterWidth = "30" };
            var command = new CommandElement { CaptionHtml = "Pokaż wartości", MethodName = "ShowFieldValue", Width = "20" };

            row.Elements.Add(field1);
            row.Elements.Add(field2);
            row.Elements.Add(field3);
            rowCmd.Elements.Add(command);

            group.Elements.Add(row);
            group.Elements.Add(rowCmd);
            stack.Elements.Add(group);

            return stack;

        }

        public MessageBoxInformation ShowFieldValue()
        {
            return new MessageBoxInformation("Aktualne wartości")
            {
                Text = String.Format("Pole1 = {0}, Pole2 = {1}, Pole3 = {2}", Field1, Field2, Field3)
            };
        }
    }
}
