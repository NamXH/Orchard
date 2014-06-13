using System;

namespace Orchard
{
    public class Step2VM : ViewModelBase
    {
        public Step2VM()
        {
            Common = new StepVMCommon("Step2Questions.txt", null, "abc");
        }

        public StepVMCommon Common{ get; set; }
    }
}

