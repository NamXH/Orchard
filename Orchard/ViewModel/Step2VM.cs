using System;

namespace Orchard
{
    public class Step2VM : ViewModelBase
    {
        public Step2VM()
        {
            Common = new StepVMCommon("Step2Questions.txt", "Step2HelpTexts.txt", null);
        }

        public StepVMCommon Common{ get; set; }
    }
}

