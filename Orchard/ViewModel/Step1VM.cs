using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace Orchard
{
    public class Step1VM : NPCBase
    {
        public Step1VM()
        {
            Common = new StepVMCommon("Step1Questions.txt", "Step1HelpTexts.txt", "Basic Information");
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

        Sprayer _currSprayer;

        public Sprayer CurrSprayer
        {
            get { return _currSprayer; }
            set { SetProperty(ref _currSprayer, value); }
        }

        OrchardBlock _currOrchardBlock;

        public OrchardBlock CurrOrchardBlock
        {
            get { return _currOrchardBlock; }
            set { SetProperty(ref _currOrchardBlock, value); }
        }

        Operator _currOperator;

        public Operator CurrOperator
        {
            get { return _currOperator; }
            set { SetProperty(ref _currOperator, value); }
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
