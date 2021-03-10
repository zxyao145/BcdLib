using BcdLib;
using Microsoft.AspNetCore.Components;

namespace BcdLibSample.BcdForms
{
    public partial class BcdForm1: BcdForm
    {
        public BcdForm1(): base()
        {
            
        }

        protected override void InitComponent()
        {
            MaximizeBox = false;
            Draggable = true;
            Title = "BcdForm1";
        }
    }
}
