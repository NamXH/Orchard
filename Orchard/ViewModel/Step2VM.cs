using System;

namespace Orchard
{
    public class Step2VM : NPCBase
    {
        public Step2VM()
        {
            Common = new StepVMCommon("Step2Questions.txt", "Step2HelpTexts.txt", "Tree Shape and Density");
        }

        public StepVMCommon Common{ get; set; }

        GrowthStage _currGrowStage;

        public GrowthStage CurrGrowStage
        {
            get { return _currGrowStage; }
            set { SetProperty(ref _currGrowStage, value); }
        }

        public enum GrowthStage
        {
            PrePetaFall,
            PostPetaFall
        }
    }
}

