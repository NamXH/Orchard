﻿using System;

namespace Orchard
{
    public class Step3VM : NPCBase
    {
        public Step3VM()
        {
            Common = new StepVMCommon("Step3Questions.txt", "Step3HelpTexts.txt", "Sprayer Information");
        }

        public StepVMCommon Common{ get; set; }

        int _operatingPressure;

        public int OperatingPressure
        {
            get { return _operatingPressure; }
            set { SetProperty(ref _operatingPressure, value); }
        }

        int _groundSpeed;

        public int GroundSpeed
        {
            get { return _groundSpeed; }
            set { SetProperty(ref _groundSpeed, value); }
        }

        int _activeNozzleNum;

        public int ActiveNozzleNum
        {
            get { return _activeNozzleNum; }
            set { SetProperty(ref _activeNozzleNum, value); }
        }

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

        NozzleUnit _currNozzleUnit;

        public NozzleUnit CurrNozzleUnit
        {
            get { return _currNozzleUnit; }
            set { SetProperty(ref _currNozzleUnit, value); }
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

        public enum NozzleUnit
        {
            mlpermin,
            abcperxyz
        }
    }
}

