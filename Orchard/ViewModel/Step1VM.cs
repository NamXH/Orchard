using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace Orchard
{
    public class Step1VM : NPCBase
    {
        public Step1VM()
        {
            Common = new StepVMCommon("Step1Questions.txt", "Step1HelpTexts.txt", "Basic Information");

//            Q2SelectedIdx = -1;
//            Q3AppTime = DateTime.Now;
        }

        public StepVMCommon Common
        {
            get;
            set;
        }

        OptimizeMode _currOptimizeMode;

        public OptimizeMode CurrOptimizeMode
        {
            get { return _currOptimizeMode; }
            set { SetProperty(ref _currOptimizeMode, value); }
        }

        RowSprayingMode _currRowSprayingMode;

        public RowSprayingMode CurrRowSprayingMode
        {
            get { return _currRowSprayingMode; }
            set { SetProperty(ref _currRowSprayingMode, value); }
        }

        Sprayer _chosenSprayer;

        public Sprayer ChosenSprayer
        {
            get { return _chosenSprayer; }
            set { SetProperty(ref _chosenSprayer, value); }
        }

        OrchardBlock _chosenOrchardBlock;

        public OrchardBlock OrchardBlock
        {
            get { return _chosenOrchardBlock; }
            set { SetProperty(ref _chosenOrchardBlock, value); }
        }

        Operator _chosenOperator;

        public Operator ChosenOperator
        {
            get { return _chosenOperator; }
            set { SetProperty(ref _chosenOperator, value); }
        }

        public enum OptimizeMode
        {
            OptimizedRate,
            LabelRate
        }

        public enum RowSprayingMode
        {
            EveryRow,
            AlternateRow
        }
    }
}
