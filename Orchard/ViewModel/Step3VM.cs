using System;

namespace Orchard
{
    public class Step3VM : NPCBase
    {
        public Step3VM()
        {
            Common = new StepVMCommon("Step3Questions.txt", "Step3HelpTexts.txt", "Sprayer Information");
        }

        public StepVMCommon Common{ get; set; }

        PressureUnit _currPressureUnit;

        public PressureUnit CurrPressureUnit
        {
            get { return _currPressureUnit; }
            set { SetProperty(ref _currPressureUnit, value); }
        }

        SpeedUnit _currSpeedUnit;

        public SpeedUnit CurrSpeedUnit
        {
            get { return _currSpeedUnit; }
            set { SetProperty(ref _currSpeedUnit, value); }
        }

        public enum PressureUnit
        {
            Bar,
            Psi
        }

        public enum SpeedUnit
        {
            kmh,
            mph
        }
    }
}

