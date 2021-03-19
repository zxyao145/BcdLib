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
            Draggable = true;
            Title = "BcdForm1";
            BodyStyle = "max-height:400px;";
        }
    }
}
